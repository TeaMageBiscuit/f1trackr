using F1Trackr.Core.Application.Users;
using F1Trackr.Core.Domain.Management;
using Microsoft.AspNetCore.Mvc;

namespace F1Trackr.Server.Endpoints.Admin.Users;

internal sealed class AdminDeleteUserEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete(
            "/api/admin/users/{userId:guid}",
            async (
                [FromRoute] Guid userId,
                [FromServices] AdminDeleteUser.CommandHandler handler,
                CancellationToken ct) =>
            {
                var command = new AdminDeleteUser.Command(new UserId(userId));
                var result = await handler.HandleAsync(command, ct);

                return result.ToApiResult();
            })
            .RequireAuthorization(AuthorizationPolicies.Admin);
    }
}
