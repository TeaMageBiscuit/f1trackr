using F1Trackr.Core.Application.FormulaOne;
using Microsoft.AspNetCore.Mvc;

namespace F1Trackr.Server.Endpoints.Admin.FormulaOne;

internal sealed class AdminImportRaceResultsEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut(
            "/api/admin/results/{season}/{round:int}",
            async (
                [FromRoute] string season,
                [FromRoute] int round,
                [FromServices] ImportRaceResults.CommandHandler handler,
                CancellationToken ct) =>
            {
                var command = new ImportRaceResults.Command(season, round);
                await handler.HandleAsync(command, ct);

                return TypedResults.NoContent();
            })
            .RequireAuthorization(AuthorizationPolicies.Admin);
    }
}
