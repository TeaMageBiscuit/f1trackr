using System.Security.Claims;
using F1Trackr.Core.Domain.Management;
using F1Trackr.Core.Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace F1Trackr.Core.Application.Groups;

public sealed class ViewGroup
{
    public sealed record Query(GroupId GroupId) : IQuery<GroupOverview?>;

    public sealed class QueryHandler : IQueryHandler<Query, GroupOverview?>
    {
        private readonly TrackrDbContext _dbContext;
        private readonly ClaimsPrincipal _currentUser;

        public QueryHandler(TrackrDbContext dbContext, ClaimsPrincipal currentUser)
        {
            _dbContext = dbContext;
            _currentUser = currentUser;
        }

        public async Task<GroupOverview?> HandleAsync(
            Query query,
            CancellationToken cancellationToken)
        {
            var group = await _dbContext.Groups
                .AsNoTracking()
                .Where(g => g.Id == query.GroupId)
                .Select(g => new GroupOverview(
                    g.Id,
                    g.Name,
                    g.Season,
                    g.Members
                        .OrderByDescending(m => m.CurrentScore)
                        .ThenBy(m => m.User.Name)
                        .Select(m => new GroupMemberSummary(
                            m.GroupId,
                            m.UserId,
                            m.User.Name,
                            m.CurrentScore))
                        .ToList()))
                .SingleOrDefaultAsync(cancellationToken);

            if (group is null)
            {
                return null;
            }

            var containsUser = group.Members.Any(m => m.UserId == _currentUser.UserId);
            if (!containsUser && !_currentUser.IsAdmin)
            {
                return null;
            }

            return group;
        }
    }
}
