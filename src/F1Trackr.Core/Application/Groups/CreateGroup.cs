using F1Trackr.Core.Domain.Management;
using F1Trackr.Core.Infrastructure.EntityFramework;
using F1Trackr.Core.Results;
using FluentResults;
using Microsoft.EntityFrameworkCore;

namespace F1Trackr.Core.Application.Groups;

public sealed class CreateGroup
{
    public sealed record Command(string Name, string Season) : ICommand<Result<GroupId>>;

    public sealed class CommandHandler : ICommandHandler<Command, Result<GroupId>>
    {
        private readonly TrackrDbContext _dbContext;

        public CommandHandler(TrackrDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<GroupId>> HandleAsync(Command command, CancellationToken cancellationToken)
        {
            var group = new Group
            {
                Id = new GroupId(Guid.CreateVersion7()),
                Name = command.Name,
                Season = command.Season,
            };

            var existing = await _dbContext.Groups
                .Where(g => g.Name == group.Name && g.Season == group.Season)
                .SingleOrDefaultAsync(cancellationToken);

            if (existing is not null)
            {
                return new ValidationError(nameof(command.Name), "Group with the same name already exists");
            }

            _dbContext.Groups.Add(group);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return group.Id;
        }
    }
}
