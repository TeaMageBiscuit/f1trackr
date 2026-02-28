using F1Trackr.Core.Application.FormulaOne;
using Microsoft.AspNetCore.Mvc;

namespace F1Trackr.Server.Endpoints.Admin.FormulaOne;

internal sealed class AdminImportConstructorsEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut(
            "/api/admin/constructors/{season}",
            async (
                [FromRoute] string season,
                [FromServices] ImportConstructors.CommandHandler handler,
                CancellationToken ct) =>
            {
                var command = new ImportConstructors.Command(season);
                await handler.HandleAsync(command, ct);

                return TypedResults.NoContent();
            })
            .RequireAuthorization(AuthorizationPolicies.Admin);
    }
}
