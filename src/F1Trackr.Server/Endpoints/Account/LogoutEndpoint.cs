using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace F1Trackr.Server.Endpoints.Account;

internal sealed class LogoutEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost(
            "/api/account/logout",
            async (HttpContext httpContext) =>
            {
                await httpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

                return TypedResults.NoContent();
            })
            .RequireAuthorization();
    }
}
