using F1Trackr.Core.Application.Users;
using Microsoft.AspNetCore.Mvc;

namespace F1Trackr.Server.Endpoints.Admin.Users;

internal sealed class AdminListUsersEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet(
            "/api/admin/users",
            async (
                [FromServices] AdminListUsers.QueryHandler handler,
                CancellationToken ct) =>
            {
                var query = new AdminListUsers.Query();
                var users = await handler.HandleAsync(query, ct);

                return TypedResults.Ok(users);
            })
            .RequireAuthorization(AuthorizationPolicies.Admin);
    }
}
