namespace F1Trackr.Server.Endpoints.Seasons;

internal sealed class SeasonsEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet(
            "/api/seasons",
            () =>
            {
                const int startYear = 2025;
                var endYear = DateTime.UtcNow.Year;

                return TypedResults.Ok(Enumerable.Range(startYear, endYear - startYear + 1).Reverse());
            });
    }
}
