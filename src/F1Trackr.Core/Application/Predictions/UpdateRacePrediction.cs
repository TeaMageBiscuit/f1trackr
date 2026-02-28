using System.Security.Claims;
using F1Trackr.Core.Domain;
using F1Trackr.Core.Domain.Management;
using F1Trackr.Core.Domain.Predictions;
using F1Trackr.Core.Infrastructure.EntityFramework;
using F1Trackr.Core.Results;
using FluentResults;
using Microsoft.EntityFrameworkCore;

namespace F1Trackr.Core.Application.Predictions;

public static class UpdateRacePrediction
{
    public sealed record Command(
        GroupId GroupId,
        UserId UserId,
        int Round,
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

            var race = await _dbContext.Races
                .Where(r => r.Id.Season == group.Season && r.Id.Round == command.Round)
                .SingleOrDefaultAsync(cancellationToken);

            if (race is null)
            {
                return new NotFoundError($"Race with round {command.Round} of season {group.Season} not found.");
            }

            if (!_currentUser.IsAdmin && _currentUser.UserId != command.UserId)
            {
                return new AccessDeniedError("You cannot alter another user's predictions.");
            }

            if (!_currentUser.IsAdmin)
            {
                var now = DateTimeOffset.UtcNow;
                var firstPractice = race.FirstPracticeTime ?? now;
                if (firstPractice < now)
                {
                    return new ValidationError("Cannot alter predictions after the first practice of the season has started.");
                }
            }

            var racePrediction = member.DriverRacePredictions.SingleOrDefault(x => x.RaceId == race.Id);
            if (racePrediction is null)
            {
                racePrediction = new DriverRacePrediction(race.Id, []);
                member.DriverRacePredictions.Add(racePrediction);
            }

            racePrediction.Drivers.Clear();

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

                racePrediction.Drivers.Add(new DriverPrediction(driver, position));
            }

            if (result.IsFailed)
            {
                return result;
            }

            _dbContext.Entry(member).State = EntityState.Modified;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Result.Ok();
        }
    }
}
