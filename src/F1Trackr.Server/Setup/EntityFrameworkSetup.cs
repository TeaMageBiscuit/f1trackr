using F1Trackr.Core.Infrastructure.EntityFramework;

namespace F1Trackr.Server.Setup;

internal static class EntityFrameworkSetup
{
    public static void SetupEntityFramework(this WebApplicationBuilder builder)
    {
        builder.AddNpgsqlDbContext<TrackrDbContext>("database");
    }
}
