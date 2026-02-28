namespace F1Trackr.Server.Setup;

public static class AuthorizationSetup
{
    public static void SetupAuthorization(this WebApplicationBuilder builder)
    {
        builder.Services.AddAuthorizationBuilder()
            .AddPolicy(AuthorizationPolicies.Admin, policy => policy.RequireRole("Admin"));
    }
}
