using F1Trackr.Core.Domain;
using F1Trackr.Core.Infrastructure.EntityFramework;
using F1Trackr.Core.Infrastructure.Jolpica;
using Microsoft.EntityFrameworkCore;

namespace F1Trackr.Core.Application.FormulaOne;

public sealed class ImportRaces
{
    public sealed record Command(string Season) : ICommand;

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
            var races = await _dbContext.Races
                .Where(x => x.Id.Season == command.Season)
                .ToListAsync(cancellationToken);

            var response = await _jolpicaService.GetRacesAsync(
                int.Parse(command.Season),
                null,
                50,
                cancellationToken);

            var imports = response?.GetRaces() ?? [];

            var existingByRound = races.ToDictionary(c => c.Id.Round);
            var imported = new HashSet<int>();

            foreach (var import in imports)
            {
                var round = int.Parse(import.Round!);

                imported.Add(round);

                if (!existingByRound.TryGetValue(round, out var race))
                {
                    race = new Race
                    {
                        Id = new RaceId(import.Season!, round),
                        Name = string.Empty,
                        Circuit = string.Empty,
                        Location = string.Empty,
                        Country = string.Empty,
                        GrandPrixTime = DateTimeOffset.MinValue,
                    };

                    _dbContext.Races.Add(race);

                    existingByRound.Add(round, race);
                }

                race.Name = import.RaceName ?? string.Empty;
                race.Circuit = import.Circuit?.CircuitName ?? string.Empty;
                race.Location = import.Circuit?.Location?.Locality ?? string.Empty;
                race.Country = import.Circuit?.Location?.Country ?? string.Empty;

                race.GrandPrixTime = ParseDate(import.Date, import.Time) ?? DateTimeOffset.MinValue;
                race.FirstPracticeTime = ParseDate(import.FirstPractice?.Date, import.FirstPractice?.Time);
                race.SecondPracticeTime = ParseDate(import.SecondPractice?.Date, import.SecondPractice?.Time);
                race.ThirdPracticeTime = ParseDate(import.ThirdPractice?.Date, import.ThirdPractice?.Time);
                race.QualifyingTime = ParseDate(import.Qualifying?.Date, import.Qualifying?.Time);
                race.SprintQualifyingTime = ParseDate(import.SprintQualifying?.Date, import.SprintQualifying?.Time);
                race.SprintTime = ParseDate(import.Sprint?.Date, import.Sprint?.Time);
            }

            foreach (var race in races.Where(c => !imported.Contains(c.Id.Round)))
            {
                _dbContext.Races.Remove(race);
            }

            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        private static DateTimeOffset? ParseDate(string? date, string? time)
        {
            return DateTimeOffset.TryParse($"{date} {time}", out var result) ? result : null;
        }
    }
}
