using F1Trackr.Core.Infrastructure.EntityFramework;

namespace F1Trackr.MigrationService;

internal sealed class Program
{
    public static void Main(string[] args)
    {
        var builder = Host.CreateApplicationBuilder(args);

        builder.AddServiceDefaults();

        builder.Services.AddHostedService<Worker>();

        builder.Services
            .AddOpenTelemetry()
            .WithTracing(tracing => tracing.AddSource(Worker.ActivitySourceName));

        builder.AddNpgsqlDbContext<TrackrDbContext>("database");

        var host = builder.Build();
        host.Run();
    }
}
