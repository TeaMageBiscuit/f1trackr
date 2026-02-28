using F1Trackr.Core.Application.FormulaOne;
using Microsoft.AspNetCore.Mvc;

namespace F1Trackr.Server.Endpoints.Admin.FormulaOne;

internal sealed class AdminImportDriversEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut(
            "/api/admin/drivers/{season}",
            async (
                [FromRoute] string season,
                [FromServices] ImportDrivers.CommandHandler handler,
                CancellationToken ct) =>
            {
                var command = new ImportDrivers.Command(season);
                await handler.HandleAsync(command, ct);

                return TypedResults.NoContent();
            })
            .RequireAuthorization(AuthorizationPolicies.Admin);
    }
}
