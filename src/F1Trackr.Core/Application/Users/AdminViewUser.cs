using F1Trackr.Core.Domain.Management;
using F1Trackr.Core.Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace F1Trackr.Core.Application.Users;

public sealed class AdminViewUser
{
    public sealed record Query(UserId UserId) : IQuery<User?>;

    public sealed class QueryHandler : IQueryHandler<Query, User?>
    {
        private readonly TrackrDbContext _dbContext;

        public QueryHandler(TrackrDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User?> HandleAsync(Query query, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users
                .AsNoTracking()
                .Where(u => u.Id == query.UserId)
                .SingleOrDefaultAsync(cancellationToken);

            return user;
        }
    }
}
