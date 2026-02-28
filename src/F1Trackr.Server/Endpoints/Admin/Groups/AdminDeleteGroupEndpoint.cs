using F1Trackr.Core.Application.Groups;
using F1Trackr.Core.Domain.Management;
using Microsoft.AspNetCore.Mvc;

namespace F1Trackr.Server.Endpoints.Admin.Groups;

public sealed class AdminDeleteGroupEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete(
            "/api/admin/groups/{groupId:guid}",
            async (
                [FromRoute] Guid groupId,
                [FromServices] DeleteGroup.CommandHandler handler,
                CancellationToken ct) =>
            {
                var command = new DeleteGroup.Command(new GroupId(groupId));
                var result = await handler.HandleAsync(command, ct);

                return result.ToApiResult();
            })
            .RequireAuthorization(AuthorizationPolicies.Admin);
    }
}
