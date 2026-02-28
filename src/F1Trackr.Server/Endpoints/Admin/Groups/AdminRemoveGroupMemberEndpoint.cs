using F1Trackr.Core.Application.Groups;
using F1Trackr.Core.Domain.Management;
using Microsoft.AspNetCore.Mvc;

namespace F1Trackr.Server.Endpoints.Admin.Groups;

public sealed class AdminRemoveGroupMemberEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete(
            "/api/admin/groups/{groupId:guid}/members/{userId:guid}",
            async (
                [FromRoute] Guid groupId,
                [FromRoute] Guid userId,
                [FromServices] RemoveGroupMember.CommandHandler handler,
                CancellationToken ct) =>
            {
                var command = new RemoveGroupMember.Command(
                    new GroupId(groupId),
                    new UserId(userId));

                var result = await handler.HandleAsync(command, ct);

                return result.ToApiResult();
            })
            .RequireAuthorization(AuthorizationPolicies.Admin);
    }
}
