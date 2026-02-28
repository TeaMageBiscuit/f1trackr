using F1Trackr.Core.Application.Groups;
using Microsoft.AspNetCore.Mvc;

namespace F1Trackr.Server.Endpoints.Admin.Groups;

internal sealed class AdminListGroupsEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet(
            "/api/admin/groups",
            async (
                [FromServices] ListGroups.QueryHandler handler,
                CancellationToken ct) =>
            {
                var query = new ListGroups.Query(UserId: null);
                var groups = await handler.HandleAsync(query, ct);

                return TypedResults.Ok(groups);
            })
            .RequireAuthorization(AuthorizationPolicies.Admin);
    }
}
