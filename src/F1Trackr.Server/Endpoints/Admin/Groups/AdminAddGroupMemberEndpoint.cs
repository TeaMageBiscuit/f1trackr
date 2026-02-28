using F1Trackr.Core.Application.Groups;
using F1Trackr.Core.Domain.Management;
using Microsoft.AspNetCore.Mvc;

namespace F1Trackr.Server.Endpoints.Admin.Groups;

internal sealed class AdminAddGroupMemberEndpoint : IEndpoint
{
    private sealed record Request(Guid UserId);

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost(
            "/api/admin/groups/{groupId:guid}/members",
            async (
                [FromRoute] Guid groupId,
                [FromBody] Request request,
                [FromServices] AddGroupMember.CommandHandler handler,
                CancellationToken ct) =>
            {
                var command = new AddGroupMember.Command(
                    new GroupId(groupId),
                    new UserId(request.UserId));

                var result = await handler.HandleAsync(command, ct);

                return result.ToApiResult();
            })
            .RequireAuthorization(AuthorizationPolicies.Admin);
    }
}
