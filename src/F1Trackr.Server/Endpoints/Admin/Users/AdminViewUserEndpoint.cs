using F1Trackr.Core.Application.Users;
using F1Trackr.Core.Domain.Management;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace F1Trackr.Server.Endpoints.Admin.Users;

internal sealed class AdminViewUserEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet(
            "/api/admin/users/{userId:guid}",
            async Task<Results<NotFound, Ok<User>>> (
                [FromRoute] Guid userId,
                [FromServices] AdminViewUser.QueryHandler handler,
                CancellationToken ct) =>
            {
                var query = new AdminViewUser.Query(new UserId(userId));
                var user = await handler.HandleAsync(query, ct);
                if (user is null)
                {
                    return TypedResults.NotFound();
                }

                return TypedResults.Ok(user);
            })
            .RequireAuthorization(AuthorizationPolicies.Admin);
    }
}
