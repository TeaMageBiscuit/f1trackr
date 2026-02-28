using F1Trackr.Core.Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace F1Trackr.Core.Application.FormulaOne;

public sealed class ListConstructors
{
    public sealed record Query(string Season) : IQuery<IReadOnlyCollection<ConstructorSummary>>;

    public sealed class QueryHandler : IQueryHandler<Query, IReadOnlyCollection<ConstructorSummary>>
    {
        private readonly TrackrDbContext _dbContext;

        public QueryHandler(TrackrDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IReadOnlyCollection<ConstructorSummary>> HandleAsync(
            Query query,
            CancellationToken cancellationToken)
        {
            var constructors = await _dbContext.Constructors
                .AsNoTracking()
                .Where(c => c.Season == query.Season)
                .OrderBy(c => c.Name)
                .Select(c => new ConstructorSummary(c.Id, c.Name, c.Nationality, c.LogoAddress))
                .ToListAsync(cancellationToken);

            return constructors;
        }
    }
}
