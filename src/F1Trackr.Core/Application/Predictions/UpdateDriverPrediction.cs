using System.Security.Claims;
using F1Trackr.Core.Domain;
using F1Trackr.Core.Domain.Management;
using F1Trackr.Core.Domain.Predictions;
using F1Trackr.Core.Infrastructure.EntityFramework;
using F1Trackr.Core.Results;
using FluentResults;
using Microsoft.EntityFrameworkCore;

namespace F1Trackr.Core.Application.Predictions;

public sealed class UpdateDriverPrediction
{
    public sealed record Command(
        GroupId GroupId,
        UserId UserId,
        IDictionary<DriverId, int> Positions) : ICommand<Result>;

    public sealed class CommandHandler : ICommandHandler<Command, Result>
    {
        private readonly ClaimsPrincipal _currentUser;
        private readonly TrackrDbContext _dbContext;

        public CommandHandler(ClaimsPrincipal currentUser, TrackrDbContext dbContext)
        {
            _currentUser = currentUser;
            _dbContext = dbContext;
        }

        public async Task<Result> HandleAsync(Command command, CancellationToken cancellationToken)
        {
            var group = await _dbContext.Groups
                .Include(g => g.Members)
                .Where(g => g.Id == command.GroupId)
                .SingleOrDefaultAsync(cancellationToken);

            if (group is null)
            {
                return new NotFoundError($"Group with ID {command.GroupId} not found.");
            }

            var member = group.Members.SingleOrDefault(m => m.UserId == command.UserId);
            if (member is null)
            {
                return new NotFoundError($"User with ID {command.UserId} not a member of group {command.GroupId}.");
            }

            if (!_currentUser.IsAdmin && _currentUser.UserId != command.UserId)
            {
                return new AccessDeniedError("You cannot alter another user's predictions.");
            }

            if (!_currentUser.IsAdmin)
            {
                var race1 = await _dbContext.Races
                    .Where(r => r.Season == group.Season && r.Round == 1)
                    .SingleOrDefaultAsync(cancellationToken);

                var now = DateTimeOffset.UtcNow;
                var firstPractice = race1?.FirstPracticeTime ?? now;
                if (firstPractice < now)
                {
                    return new ValidationError("Cannot alter predictions after the first practice of the season has started.");
                }
            }

            member.DriverPredictions.Clear();

            var drivers = await _dbContext.Drivers
                .Where(c => c.Season == group.Season)
                .ToDictionaryAsync(c => c.Id, cancellationToken);

            var result = new Result();
            foreach (var (driverId, position) in command.Positions)
            {
                if (!drivers.TryGetValue(driverId, out var driver))
                {
                    result.WithError(new ValidationError($"Driver with ID {driverId} not found."));
                    continue;
                }

                member.DriverPredictions.Add(new DriverPrediction(driver, position));
            }

            if (result.IsFailed)
            {
                return result;
            }

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Result.Ok();
        }
    }
}
