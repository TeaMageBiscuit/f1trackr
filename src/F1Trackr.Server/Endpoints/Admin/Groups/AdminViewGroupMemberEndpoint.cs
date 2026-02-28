using F1Trackr.Core.Application.Groups;
using F1Trackr.Core.Domain.Management;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace F1Trackr.Server.Endpoints.Admin.Groups;

public sealed class AdminViewGroupMemberEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet(
            "/api/admin/groups/{groupId:guid}/members/{userId:guid}",
            async Task<Results<NotFound, Ok<GroupMember>>> (
                [FromRoute] Guid groupId,
                [FromRoute] Guid userId,
                [FromServices] ViewGroupMember.QueryHandler handler,
                CancellationToken ct) =>
            {
                var query = new ViewGroupMember.Query(
                    new GroupId(groupId),
                    new UserId(userId));

                var groupMember = await handler.HandleAsync(query, ct);
                if (groupMember is null)
                {
                    return TypedResults.NotFound();
                }

                return TypedResults.Ok(groupMember);
            })
            .RequireAuthorization(AuthorizationPolicies.Admin);
    }
}
