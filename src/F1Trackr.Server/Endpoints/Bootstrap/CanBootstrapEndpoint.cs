using F1Trackr.Core.Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace F1Trackr.Server.Endpoints.Bootstrap;

internal sealed class CanBootstrapEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet(
            "/api/bootstrap",
            async (TrackrDbContext dbContext, CancellationToken ct) =>
            {
                var userCount = await dbContext.Users.CountAsync(ct);

                return TypedResults.Ok(
                    new
                    {
                        canBootstrap = userCount == 0,
                    });
            });
    }
}
