using F1Trackr.Core.Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace F1Trackr.Core.Application.FormulaOne;

public sealed class ListDrivers
{
    public sealed record Query(string Season) : IQuery<IReadOnlyCollection<DriverSummary>>;

    public sealed class QueryHandler : IQueryHandler<Query, IReadOnlyCollection<DriverSummary>>
    {
        private readonly TrackrDbContext _dbContext;

        public QueryHandler(TrackrDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IReadOnlyCollection<DriverSummary>> HandleAsync(
            Query query,
            CancellationToken cancellationToken)
        {
            var drivers = await _dbContext.Drivers
                .AsNoTracking()
                .Where(d => d.Season == query.Season)
                .OrderBy(d => d.GivenName)
                .ThenBy(d => d.FamilyName)
                .Select(d => new DriverSummary(
                    d.Id,
                    d.GivenName,
                    d.FamilyName,
                    d.Nationality,
                    d.Code,
                    d.DriverNumber,
                    d.ImageAddress))
                .ToListAsync(cancellationToken);

            return drivers;
        }
    }
}
