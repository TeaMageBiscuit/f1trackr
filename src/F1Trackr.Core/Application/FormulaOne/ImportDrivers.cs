using F1Trackr.Core.Domain;
using F1Trackr.Core.Infrastructure.EntityFramework;
using F1Trackr.Core.Infrastructure.Jolpica;
using Microsoft.EntityFrameworkCore;

namespace F1Trackr.Core.Application.FormulaOne;

public sealed class ImportDrivers
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
            var drivers = await _dbContext.Drivers
                .Where(x => x.Season == command.Season)
                .ToListAsync(cancellationToken);

            var response = await _jolpicaService.GetDriversAsync(
                int.Parse(command.Season),
                null,
                50,
                cancellationToken);

            var imports = response?.GetDrivers() ?? [];

            var existingById = drivers.ToDictionary(c => c.Id.Value);
            var imported = new HashSet<string>();

            foreach (var import in imports)
            {
                imported.Add(import.DriverId);

                if (!existingById.TryGetValue(import.DriverId, out var driver))
                {
                    driver = new Driver
                    {
                        Id = new DriverId(import.DriverId),
                        Season = command.Season,
                        GivenName = string.Empty,
                        FamilyName = string.Empty,
                    };

                    _dbContext.Drivers.Add(driver);

                    existingById.Add(import.DriverId, driver);
                }

                driver.GivenName = import.GivenName;
                driver.FamilyName = import.FamilyName;
                driver.Nationality = import.Nationality;
                driver.Code = import.Code;
                driver.DriverNumber = import.PermanentNumber;
            }

            foreach (var driver in drivers.Where(c => !imported.Contains(c.Id.Value)))
            {
                _dbContext.Drivers.Remove(driver);
            }

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
