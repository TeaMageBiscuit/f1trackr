using F1Trackr.Core.Application.FormulaOne;
using Microsoft.AspNetCore.Mvc;

namespace F1Trackr.Server.Endpoints.Admin.FormulaOne;

internal sealed class AdminImportRacesEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut(
            "/api/admin/races/{season}",
            async (
                [FromRoute] string season,
                [FromServices] ImportRaces.CommandHandler handler,
                CancellationToken ct) =>
            {
                var command = new ImportRaces.Command(season);
                await handler.HandleAsync(command, ct);

                return TypedResults.NoContent();
            })
            .RequireAuthorization(AuthorizationPolicies.Admin);
    }
}
