using F1Trackr.Core.Domain.Management;
using F1Trackr.Core.Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace F1Trackr.Core.Application.Groups;

public sealed class ListGroups
{
    public sealed record Query(UserId? UserId) : IQuery<IReadOnlyCollection<GroupSummary>>;

    public sealed class QueryHandler : IQueryHandler<Query, IReadOnlyCollection<GroupSummary>>
    {
        private readonly TrackrDbContext _dbContext;

        public QueryHandler(TrackrDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IReadOnlyCollection<GroupSummary>> HandleAsync(
            Query query,
            CancellationToken cancellationToken)
        {
            var queryable = _dbContext.Groups.Include(g => g.Members).AsNoTracking();

            if (query.UserId is not null)
            {
                queryable = queryable.Where(g => g.Members.Any(m => m.UserId == query.UserId));
            }

            var groups = await queryable
                .OrderBy(g => g.Name)
                .Select(g => new GroupSummary(g.Id, g.Name, g.Season))
                .ToListAsync(cancellationToken);

            return groups;
        }
    }
}
