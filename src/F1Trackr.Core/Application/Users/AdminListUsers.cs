using F1Trackr.Core.Domain.Management;
using F1Trackr.Core.Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace F1Trackr.Core.Application.Users;

public sealed class AdminListUsers
{
    public sealed record Query : IQuery<IReadOnlyCollection<User>>;

    public sealed class QueryHandler : IQueryHandler<Query, IReadOnlyCollection<User>>
    {
        private readonly TrackrDbContext _dbContext;

        public QueryHandler(TrackrDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IReadOnlyCollection<User>> HandleAsync(Query query, CancellationToken cancellationToken)
        {
            var users = await _dbContext.Users
                .AsNoTracking()
                .OrderBy(u => u.Name)
                .ToListAsync(cancellationToken);

            return users;
        }
    }
}
