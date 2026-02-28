using F1Trackr.Core.Application.FormulaOne;
using Microsoft.AspNetCore.Mvc;

namespace F1Trackr.Server.Endpoints.FormulaOne;

internal sealed class ListRacesEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet(
            "/api/seasons/{season}/races",
            async (
                [FromRoute] string season,
                [FromServices] ListRaces.QueryHandler handler,
                CancellationToken ct) =>
            {
                var query = new ListRaces.Query(season);
                var races = await handler.HandleAsync(query, ct);

                return TypedResults.Ok(races);
            });
    }
}
