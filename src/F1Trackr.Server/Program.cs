using F1Trackr.Server.Setup;
using F1Trackr.Core;

namespace F1Trackr.Server;

public static class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.AddServiceDefaults();

        builder.Services.AddDistributedMemoryCache();

        builder.SetupAuthentication();
        builder.SetupAuthorization();
        builder.SetupEndpoints();
        builder.SetupEntityFramework();
        builder.SetupExternalServices();
        builder.SetupValidators();

        builder.Services.SetupCore();

        var app = builder.Build();

        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseDefaultFiles();
        app.UseStaticFiles();
        app.MapEndpoints();

        if (!app.Environment.IsDevelopment())
        {
            app.MapFallbackToFile("index.html");
        }

        await app.RunAsync();
    }
}
