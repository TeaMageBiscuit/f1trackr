using F1Trackr.Core.Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace F1Trackr.Core.Application.FormulaOne;

public sealed class ListRaces
{
    public sealed record Query(string Season) : IQuery<IReadOnlyCollection<RaceSummary>>;

    public sealed class QueryHandler : IQueryHandler<Query, IReadOnlyCollection<RaceSummary>>
    {
        private readonly TrackrDbContext _dbContext;

        public QueryHandler(TrackrDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IReadOnlyCollection<RaceSummary>> HandleAsync(
            Query query,
            CancellationToken cancellationToken)
        {
            var races = await _dbContext.Races
                .AsNoTracking()
                .Where(r => r.Id.Season == query.Season)
                .OrderBy(r => r.Id.Round)
                .Select(r => new RaceSummary(
                    r.Id,
                    r.Name,
                    r.Circuit,
                    r.Location,
                    r.Country,
                    r.GrandPrixTime,
                    r.FirstPracticeTime))
                .ToListAsync(cancellationToken);

            return races;
        }
    }
}
