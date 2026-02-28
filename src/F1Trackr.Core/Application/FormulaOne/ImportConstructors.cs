using F1Trackr.Core.Domain;
using F1Trackr.Core.Infrastructure.EntityFramework;
using F1Trackr.Core.Infrastructure.Jolpica;
using Microsoft.EntityFrameworkCore;

namespace F1Trackr.Core.Application.FormulaOne;

public sealed class ImportConstructors
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
            var constructors = await _dbContext.Constructors
                .Where(x => x.Season == command.Season)
                .ToListAsync(cancellationToken);

            var response = await _jolpicaService.GetConstructorsAsync(
                int.Parse(command.Season),
                null,
                null,
                cancellationToken);

            var imports = response?.GetConstructors() ?? [];

            var existingById = constructors.ToDictionary(c => c.Id.Value);
            var imported = new HashSet<string>();

            foreach (var import in imports)
            {
                imported.Add(import.ConstructorId);

                if (!existingById.TryGetValue(import.ConstructorId, out var constructor))
                {
                    constructor = new Constructor
                    {
                        Id = new ConstructorId(import.ConstructorId),
                        Season = command.Season,
                        Name = string.Empty,
                    };

                    _dbContext.Constructors.Add(constructor);

                    existingById.Add(import.ConstructorId, constructor);
                }

                constructor.Name = import.Name;
                constructor.Nationality = import.Nationality;
            }

            foreach (var constructor in constructors.Where(c => !imported.Contains(c.Id.Value)))
            {
                _dbContext.Constructors.Remove(constructor);
            }

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
