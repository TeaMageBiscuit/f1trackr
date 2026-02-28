using System.Security.Claims;
using F1Trackr.Core;
using F1Trackr.Core.Application.Groups;
using Microsoft.AspNetCore.Mvc;

namespace F1Trackr.Server.Endpoints.Groups;

internal sealed class ListGroupsEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet(
            "/api/groups",
            async (
                [FromServices] ListGroups.QueryHandler handler,
                ClaimsPrincipal currentUser,
                CancellationToken ct) =>
            {
                var query = new ListGroups.Query(currentUser.UserId);
                var groups = await handler.HandleAsync(query, ct);

                return TypedResults.Ok(groups);
            })
            .RequireAuthorization();
    }
}
