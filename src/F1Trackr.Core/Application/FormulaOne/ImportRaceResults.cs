using F1Trackr.Core.Domain;
using F1Trackr.Core.Infrastructure.EntityFramework;
using F1Trackr.Core.Infrastructure.Jolpica;
using Microsoft.EntityFrameworkCore;

namespace F1Trackr.Core.Application.FormulaOne;

public sealed class ImportRaceResults
{
    public sealed record Command(string Season, int Round) : ICommand;

    public sealed class CommandHandler : ICommandHandler<Command>
    {
        private readonly TrackrDbContext _dbContext;
        private readonly JolpicaService _jolpicaService;

        public CommandHandler(TrackrDbContext dbContext, JolpicaService jolpicaService)
        {
            _dbContext = dbContext;
            _jolpicaService = jolpicaService;
        }

        public async Task HandleAsync(Command command, CancellationToken cancellationToken)
        {
            var season = int.Parse(command.Season);

            var race = await _dbContext.Races
                .Where(x => x.Id.Season == command.Season && x.Id.Round == command.Round)
                .SingleOrDefaultAsync(cancellationToken);

            if (race is null)
            {
                return;
            }

            var raceResult = await _dbContext.RaceResults
                .Where(x => x.RaceId.Season == command.Season && x.RaceId.Round == command.Round)
                .SingleOrDefaultAsync(cancellationToken);

            if (raceResult is null)
            {
                raceResult = new RaceResult
                {
                    RaceId = race.Id,
                    UpdatedAt = DateTimeOffset.UtcNow,
                };

                _dbContext.RaceResults.Add(raceResult);
            }

            raceResult.UpdatedAt = DateTimeOffset.UtcNow;
            raceResult.SprintDriverResults.Clear();
            raceResult.QualifyingResults.Clear();
            raceResult.DriverResults.Clear();
            raceResult.ConstructorStandingsSnapshot.Clear();
            raceResult.DriverStandingsSnapshot.Clear();

            if (race.SprintTime.HasValue)
            {
                var sprintResult = await _jolpicaService.GetSprintResultAsync(
                    season,
                    race.Id.Round,
                    cancellationToken);

                var sprintResultImport = sprintResult?.GetResults().FirstOrDefault();
                foreach (var result in sprintResultImport?.SprintResults ?? [])
                {
                    raceResult.SprintDriverResults.Add(
                        new DriverPosition(
                            new DriverId(result.Driver!.DriverId),
                            new ConstructorId(result.Constructor!.ConstructorId),
                            int.Parse(result.Position ?? "0"),
                            int.Parse(result.Points ?? "0"),
                            int.Parse(result.Grid ?? "0"),
                            int.Parse(result.Laps ?? "0"),
                            result.Status ?? string.Empty,
                            result.Time?.Time,
                            result.FastestLap?.Time?.Time));
                }
            }

            var qualifyingResponse = await _jolpicaService.GetQualifyingResultAsync(
                season,
                race.Id.Round,
                cancellationToken);

            var qualifyingResultImport = qualifyingResponse?.GetResults().FirstOrDefault();
            foreach (var result in qualifyingResultImport?.Results ?? [])
            {
                raceResult.QualifyingResults.Add(
                    new QualifyingPosition(
                        new DriverId(result.Driver!.DriverId),
                        new ConstructorId(result.Constructor!.ConstructorId),
                        int.Parse(result.Position ?? "0"),
                        result.Q1,
                        result.Q2,
                        result.Q3));
            }

            var raceResultResponse = await _jolpicaService.GetRaceResultAsync(
                season,
                race.Id.Round,
                cancellationToken);

            var raceResultImport = raceResultResponse?.GetResults().FirstOrDefault();
            foreach (var result in raceResultImport?.Results ?? [])
            {
                raceResult.DriverResults.Add(
                    new DriverPosition(
                        new DriverId(result.Driver!.DriverId),
                        new ConstructorId(result.Constructor!.ConstructorId),
                        int.Parse(result.Position ?? "0"),
                        int.Parse(result.Points ?? "0"),
                        int.Parse(result.Grid ?? "0"),
                        int.Parse(result.Laps ?? "0"),
                        result.Status ?? string.Empty,
                        result.Time?.Time,
                        result.FastestLap?.Time?.Time));
            }

            var constructorStandingsResponse = await _jolpicaService.GetConstructorStandingsAsync(
                season,
                $"{race.Id.Round}",
                cancellationToken);

            var constructorStandings = constructorStandingsResponse?.GetStandings() ?? [];
            foreach (var standing in constructorStandings)
            {
                raceResult.ConstructorStandingsSnapshot.Add(
                    new ConstructorStanding(
                        new ConstructorId(standing.Constructor!.ConstructorId),
                        int.Parse(standing.Position ?? "0"),
                        int.Parse(standing.Points ?? "0"),
                        int.Parse(standing.Wins ?? "0")));
            }

            var driverStandingsResponse = await _jolpicaService.GetDriverStandingsAsync(
                season,
                $"{race.Id.Round}",
                cancellationToken);

            var driverStandings = driverStandingsResponse?.GetStandings() ?? [];
            foreach (var standing in driverStandings)
            {
                raceResult.DriverStandingsSnapshot.Add(
                    new DriverStanding(
                        new DriverId(standing.Driver!.DriverId),
                        int.Parse(standing.Position ?? "0"),
                        int.Parse(standing.Points ?? "0"),
                        int.Parse(standing.Wins ?? "0")));
            }

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
