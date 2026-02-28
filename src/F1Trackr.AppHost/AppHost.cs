using System.Diagnostics.CodeAnalysis;
using Aspire.Hosting.Docker.Resources.ComposeNodes;
using Microsoft.Extensions.Hosting;

namespace F1Trackr.AppHost;

public static class Program
{
    [Experimental("ASPIRECOMPUTE003")]
    public static void Main(string[] args)
    {
        var builder = DistributedApplication.CreateBuilder(args);

        if (builder.ExecutionContext.IsPublishMode)
        {
        }

        var database = AddDatabase(builder);

        var migrations = builder.AddProject<Projects.F1Trackr_MigrationService>("migrations")
            .WithReference(database)
            .WaitFor(database);

        var server = builder.AddProject<Projects.F1Trackr_Server>("server")
            .WithReference(database)
            .WithReference(migrations)
            .WaitFor(database)
            .WaitForCompletion(migrations);

        if (builder.ExecutionContext.IsPublishMode)
        {
            var hostname = builder.AddParameter("traefik-host");
            var registryEndpoint = builder.AddParameter("registry-endpoint");
            var registryRepository = builder.AddParameter("registry-repository");
            var registry = builder.AddContainerRegistry("container-registry", registryEndpoint, registryRepository);

            builder.AddDockerComposeEnvironment("compose")
                .WithProperties(props =>
                {
                    props.DefaultNetworkName = "f1-trackr";
                    props.DashboardEnabled = builder.Environment.IsDevelopment();
                    props.RequiresImageBuild();
                })
                .ConfigureComposeFile(compose => compose
                    .AddNetwork(new Network
                    {
                        Name = "dokploy",
                        External = true,
                    }))
                .WithContainerRegistry(registry);

            server.PublishAsDockerComposeService((compose, service) =>
            {
                service.Networks.Add("dokploy");
                service.Labels.Add("traefik.enable", "true");
                service.Labels.Add("traefik.http.routers.f1-trackr.rule", $"Host(`{hostname.AsEnvironmentPlaceholder(compose)}`)");
                service.Labels.Add("traefik.http.routers.f1-trackr.entrypoints", "websecure");
                service.Labels.Add("traefik.http.routers.f1-trackr.tls.certResolver", "letsencrypt");
                service.Labels.Add("traefik.http.services.f1-trackr.loadbalancer.server.port", service.Ports[0]);
            });
        }

        if (builder.Environment.IsProduction())
        {
            server.WithExternalHttpEndpoints();
        }

        if (builder.Environment.IsDevelopment())
        {
            builder
                .AddViteApp("frontend", Path.Combine("..", "F1Trackr.Frontend"))
                .WithEndpoint("http", annotation => annotation.Port = 5111)
                .WithNpmPackageInstallation()
                .WithExternalHttpEndpoints()
                .WithEnvironment("BACKEND_URL", server.GetEndpoint("http"))
                .WaitFor(server);
        }

        var host = builder.Build();

        host.Run();
    }

    private static IResourceBuilder<IResourceWithConnectionString> AddDatabase(IDistributedApplicationBuilder builder)
    {
        if (builder.Environment.IsDevelopment())
        {
            return builder
                .AddPostgres("postgres")
                .WithLifetime(ContainerLifetime.Persistent)
                .WithDataVolume()
                .WithPgAdmin(admin => admin.WithHostPort(5112))
                .AddDatabase("database", "F1Trackr");
        }

        return builder.AddConnectionString("database");
    }
}
