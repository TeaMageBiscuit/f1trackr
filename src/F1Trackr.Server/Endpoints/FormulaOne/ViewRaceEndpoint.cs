using F1Trackr.Core.Application.FormulaOne;
using F1Trackr.Core.Domain;
using Microsoft.AspNetCore.Mvc;

namespace F1Trackr.Server.Endpoints.FormulaOne;

public sealed class ViewRaceEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet(
            "/api/seasons/{season}/races/{round:int}/",
            async (
                [FromRoute] string season,
                [FromRoute] int round,
                [FromServices] ViewRace.QueryHandler handler,
                CancellationToken ct) =>
            {
                var query = new ViewRace.Query(new RaceId(season, round));
                var race = await handler.HandleAsync(query, ct);

                return TypedResults.Ok(race);
            });
    }
}
