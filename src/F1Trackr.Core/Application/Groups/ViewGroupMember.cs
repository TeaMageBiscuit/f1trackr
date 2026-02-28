using System.Security.Claims;
using F1Trackr.Core.Domain.Management;
using F1Trackr.Core.Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace F1Trackr.Core.Application.Groups;

public sealed class ViewGroupMember
{
    public sealed record Query(GroupId GroupId, UserId UserId) : IQuery<GroupMember?>;

    public sealed class QueryHandler : IQueryHandler<Query, GroupMember?>
    {
        private readonly TrackrDbContext _dbContext;
        private readonly ClaimsPrincipal _currentUser;

        public QueryHandler(TrackrDbContext dbContext, ClaimsPrincipal currentUser)
        {
            _dbContext = dbContext;
            _currentUser = currentUser;
        }

        public async Task<GroupMember?> HandleAsync(
            Query query,
            CancellationToken cancellationToken)
        {
            var group = await _dbContext.Groups
                .AsNoTracking()
                .Include(g => g.Members)
                .ThenInclude(m => m.User)
                .Where(g => g.Id == query.GroupId)
                .SingleOrDefaultAsync(cancellationToken);

            var groupMember = group?.Members.SingleOrDefault(m => m.UserId == query.UserId);
            if (groupMember is null)
            {
                return null;
            }

            var containsUser = group!.Members.Any(m => m.UserId == _currentUser.UserId);
            if (!containsUser && !_currentUser.IsAdmin)
            {
                return null;
            }

            return groupMember;
        }
    }
}
