using F1Trackr.Core.Application.FormulaOne;
using Microsoft.AspNetCore.Mvc;

namespace F1Trackr.Server.Endpoints.FormulaOne;

internal sealed class ListDriversEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet(
            "/api/seasons/{season}/drivers",
            async (
                [FromRoute] string season,
                [FromServices] ListDrivers.QueryHandler handler,
                CancellationToken ct) =>
            {
                var query = new ListDrivers.Query(season);
                var drivers = await handler.HandleAsync(query, ct);

                return TypedResults.Ok(drivers);
            });
    }
}
