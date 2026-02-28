using F1Trackr.Core.Domain;
using F1Trackr.Core.Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace F1Trackr.Core.Application.FormulaOne;

public sealed class ViewRace
{
    public sealed record Query(RaceId RaceId) : IQuery<RaceOverview?>;

    public sealed class QueryHandler : IQueryHandler<Query, RaceOverview?>
    {
        private readonly TrackrDbContext _dbContext;

        public QueryHandler(TrackrDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<RaceOverview?> HandleAsync(Query query, CancellationToken cancellationToken)
        {
            var race = await _dbContext.Races
                .Where(r => r.Id == query.RaceId)
                .SingleOrDefaultAsync(cancellationToken);

            if (race is null)
            {
                return null;
            }

            var raceResult = await _dbContext.RaceResults
                .Where(r => r.RaceId == query.RaceId)
                .SingleOrDefaultAsync(cancellationToken);

            return new RaceOverview(
                race.Id,
                race.Name,
                race.Circuit,
                race.Location,
                race.Country,
                race.GrandPrixTime,
                race.FirstPracticeTime,
                race.SecondPracticeTime,
                race.ThirdPracticeTime,
                race.QualifyingTime,
                race.SprintQualifyingTime,
                race.SprintTime,
                raceResult?.SprintDriverResults.ToList().AsReadOnly(),
                raceResult?.QualifyingResults.ToList().AsReadOnly(),
                raceResult?.DriverResults.ToList().AsReadOnly(),
                raceResult?.ConstructorStandingsSnapshot.ToList().AsReadOnly(),
                raceResult?.DriverStandingsSnapshot.ToList().AsReadOnly());
        }
    }
}
