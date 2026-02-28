using F1Trackr.Core.Domain;
using F1Trackr.Core.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace F1Trackr.Server.Endpoints.Admin;

internal sealed class AdminRecalculateScoreEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut(
            "/api/admin/scores/{season}/{round:int}",
            async (
                [FromRoute] string season,
                [FromRoute] int round,
                [FromServices] ScoreCalculationService scoreCalculationService,
                CancellationToken ct) =>
            {
                await scoreCalculationService.HandleAsync(new RaceId(season, round), ct);

                return TypedResults.NoContent();
            })
            .RequireAuthorization(AuthorizationPolicies.Admin);
    }
}
