using F1Trackr.Core.Domain.Management;
using F1Trackr.Core.Infrastructure.EntityFramework;
using F1Trackr.Core.Results;
using FluentResults;
using Microsoft.EntityFrameworkCore;

namespace F1Trackr.Core.Application.Groups;

public sealed class RenameGroup
{
    public sealed record Command(GroupId GroupId, string Name) : ICommand<Result>;

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
                .Where(g => g.Id == command.GroupId)
                .SingleOrDefaultAsync(cancellationToken);

            if (group is null)
            {
                return new NotFoundError($"Group with ID {command.GroupId} not found.");
            }

            var existing = await _dbContext.Groups
                .Where(g => g.Name == group.Name && g.Season == group.Season && g.Id != command.GroupId)
                .SingleOrDefaultAsync(cancellationToken);

            if (existing is not null)
            {
                return new ValidationError(nameof(command.Name), "Group with the same name already exists");
            }

            group.Name = command.Name;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Result.Ok();
        }
    }
}
