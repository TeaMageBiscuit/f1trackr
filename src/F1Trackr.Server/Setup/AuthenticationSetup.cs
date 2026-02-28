using Microsoft.AspNetCore.Authentication.Cookies;

namespace F1Trackr.Server.Setup;

public static class AuthenticationSetup
{
    public static void SetupAuthentication(this WebApplicationBuilder builder)
    {
        builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.Cookie.Name = "F1TrackrAuth";
                options.Cookie.SameSite = SameSiteMode.Strict;
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                options.Cookie.IsEssential = true;
                options.Cookie.HttpOnly = true;
                options.LoginPath = "/login";
                options.SlidingExpiration = true;
                options.ExpireTimeSpan = TimeSpan.FromDays(30);
            });
    }
}
