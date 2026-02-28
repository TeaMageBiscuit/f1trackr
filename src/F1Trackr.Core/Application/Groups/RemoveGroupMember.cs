using F1Trackr.Core.Domain.Management;
using F1Trackr.Core.Infrastructure.EntityFramework;
using F1Trackr.Core.Results;
using FluentResults;
using Microsoft.EntityFrameworkCore;

namespace F1Trackr.Core.Application.Groups;

public sealed class RemoveGroupMember
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

            var member = group.Members.SingleOrDefault(m => m.UserId == command.UserId);

            if (member is null)
            {
                return new ValidationError("User is not a member of the group.");
            }

            group.Members.Remove(member);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Result.Ok();
        }
    }
}
