using System.Security.Claims;
using F1Trackr.Core.Application;
using F1Trackr.Core.Domain.Management;
using F1Trackr.Core.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace F1Trackr.Core;

public static class CoreSetup
{
    public static void SetupCore(this IServiceCollection services)
    {
        services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

        services.AddHttpContextAccessor();
        services.AddScoped<ClaimsPrincipal>(sp =>
        {
            var httpContextAccessor = sp.GetRequiredService<IHttpContextAccessor>();
            var httpContext = httpContextAccessor.HttpContext;

            return httpContext?.User ?? new ClaimsPrincipal();
        });

        services.AddScoped<ScoreCalculationService>();

        services.Scan(scan => scan
            .FromAssemblies(CoreAssemblyReference.Assembly)
            .AddClasses(classes => classes.AssignableTo(typeof(IQueryHandler<,>)))
            .AsSelfWithInterfaces()
            .WithScopedLifetime());

        services.Scan(scan => scan
            .FromAssemblies(CoreAssemblyReference.Assembly)
            .AddClasses(classes => classes.AssignableTo(typeof(ICommandHandler<>)))
            .AsSelfWithInterfaces()
            .WithScopedLifetime());

        services.Scan(scan => scan
            .FromAssemblies(CoreAssemblyReference.Assembly)
            .AddClasses(classes => classes.AssignableTo(typeof(ICommandHandler<,>)))
            .AsSelfWithInterfaces()
            .WithScopedLifetime());
    }
}
