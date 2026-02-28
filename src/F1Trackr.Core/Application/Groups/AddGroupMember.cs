using F1Trackr.Core.Domain;
using F1Trackr.Core.Domain.Management;
using F1Trackr.Core.Infrastructure.EntityFramework;
using F1Trackr.Core.Results;
using FluentResults;
using Microsoft.EntityFrameworkCore;

namespace F1Trackr.Core.Application.Groups;

public sealed class AddGroupMember
{
    public sealed record Command(GroupId GroupId, UserId UserId) : ICommand<Result>;

    public sealed class CommandHandler : ICommandHandler<Command, Result>
    {
        private readonly TrackrDbContext _dbContext;

        public CommandHandler(TrackrDbContext dbContext)
        {
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

            var user = await _dbContext.Users
                .Where(u => u.Id == command.UserId)
                .SingleOrDefaultAsync(cancellationToken);

            if (user is null)
            {
                return new NotFoundError($"User with ID {command.UserId} not found.");
            }

            if (group.Members.Any(m => m.UserId == user.Id))
            {
                return new ValidationError("User is already a member of the group.");
            }

            group.Members.Add(
                new GroupMember
                {
                    GroupId = group.Id,
                    UserId = user.Id,
                    User = user,
                    CurrentScore = Scoring.BaseScore,
                    ScoreSnapshots = [],
                    ConstructorPredictions = [],
                    DriverPredictions = [],
                    DriverRacePredictions = [],
                });

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Result.Ok();
        }
    }
}
