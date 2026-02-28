using System.Security.Claims;
using F1Trackr.Core;
using F1Trackr.Core.Domain.Management;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace F1Trackr.Server;

internal static class HttpContextExtensions
{
    public static async Task SignInUserAsync(this HttpContext httpContext, User user)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id.Value.ToString()),
            new(ClaimTypes.Name, user.Name),
            new(ClaimTypes.Role, user.IsAdmin ? UserRoles.Admin : UserRoles.User),
        };

        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

        await httpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(identity),
            new AuthenticationProperties
            {
                AllowRefresh = true,
                IsPersistent = true,
                ExpiresUtc = DateTimeOffset.UtcNow.AddDays(30),
            });
    }
}
