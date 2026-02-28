using System.Security.Claims;

namespace F1Trackr.Server.Endpoints.Account;

internal sealed class AccountEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet(
            "/api/account/me",
            (HttpContext httpContext) => TypedResults.Ok(
                new
                {
                    isAuthenticated = httpContext.User.Identity?.IsAuthenticated ?? false,
                    userId = httpContext.User.FindFirstValue(ClaimTypes.NameIdentifier),
                    userName = httpContext.User.Identity?.Name,
                    isAdmin = httpContext.User.IsInRole("Admin"),
                }));
    }
}
