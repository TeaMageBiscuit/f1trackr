using F1Trackr.Core.Application.FormulaOne;
using Microsoft.AspNetCore.Mvc;

namespace F1Trackr.Server.Endpoints.FormulaOne;

internal sealed class ListConstructorsEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet(
            "/api/seasons/{season}/constructors",
            async (
                [FromRoute] string season,
                [FromServices] ListConstructors.QueryHandler handler,
                CancellationToken ct) =>
            {
                var query = new ListConstructors.Query(season);
                var constructors = await handler.HandleAsync(query, ct);

                return TypedResults.Ok(constructors);
            });
    }
}
