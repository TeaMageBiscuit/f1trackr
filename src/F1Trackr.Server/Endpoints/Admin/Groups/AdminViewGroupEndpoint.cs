using F1Trackr.Core.Application.Groups;
using F1Trackr.Core.Domain.Management;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace F1Trackr.Server.Endpoints.Admin.Groups;

public sealed class AdminViewGroupEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet(
            "/api/admin/groups/{groupId:guid}",
            async Task<Results<NotFound, Ok<GroupOverview>>> (
                [FromRoute] Guid groupId,
                [FromServices] ViewGroup.QueryHandler handler,
                CancellationToken ct) =>
            {
                var query = new ViewGroup.Query(new GroupId(groupId));
                var group = await handler.HandleAsync(query, ct);
                if (group is null)
                {
                    return TypedResults.NotFound();
                }

                return TypedResults.Ok(group);
            })
            .RequireAuthorization(AuthorizationPolicies.Admin);
    }
}
