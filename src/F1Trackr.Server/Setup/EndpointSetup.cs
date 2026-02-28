using F1Trackr.Server.Endpoints;
using Scalar.AspNetCore;

namespace F1Trackr.Server.Setup;

internal static class EndpointSetup
{
    public static void SetupEndpoints(this WebApplicationBuilder builder)
    {
        if (builder.Environment.IsDevelopment())
        {
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddOpenApi("internal");
        }

        builder.Services.Scan(scan => scan
            .FromAssemblies(BackendAssemblyReference.Assembly)
            .AddClasses(classes => classes.AssignableTo<IEndpoint>(), false)
            .AsImplementedInterfaces()
            .WithSingletonLifetime());
    }

    public static void MapEndpoints(this WebApplication app)
    {
        var endpointMappers = app.Services.GetServices<IEndpoint>();

        foreach (var mapper in endpointMappers)
        {
            mapper.MapEndpoint(app);
        }

        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.MapScalarApiReference(options =>
            {
                options.AddDocument("internal");
            });
        }

        // app.MapAdminEndpoints();
        //
        // app.MapAccountEndpoint();
        // app.MapBootstrapEndpoint();
        // app.MapLoginEndpoint();
        // app.MapSeasonEndpoints();
    }
}
