using F1Trackr.Core.Infrastructure.Jolpica;

namespace F1Trackr.Server.Setup;

internal static class ExternalServiceSetup
{
    public static void SetupExternalServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<JolpicaService>()
            .AddHttpClient<JolpicaService>(client =>
            {
                client.BaseAddress = new Uri("https://api.jolpi.ca", UriKind.Absolute);
            })
            .AddStandardResilienceHandler();
    }
}
