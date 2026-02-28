using F1Trackr.Core.Domain.Management;
using F1Trackr.Core.Infrastructure.EntityFramework;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace F1Trackr.Server.Endpoints.Bootstrap;

internal sealed class BootstrapEndpoint : IEndpoint
{
    private sealed record BootstrapRequest(string UserName, string AccessCode);

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost(
            "/api/bootstrap",
            async Task<Results<Ok<UserId>, NotFound>> (
                HttpContext httpContext,
                TrackrDbContext dbContext,
                IPasswordHasher<User> passwordHasher,
                [FromBody] BootstrapRequest request,
                CancellationToken ct) =>
            {
                var userCount = await dbContext.Users.CountAsync(ct);
                if (userCount > 0)
                {
                    return TypedResults.NotFound();
                }

                var user = new User
                {
                    Id = new UserId(Guid.CreateVersion7()),
                    Name = request.UserName,
                    AccessCode = string.Empty,
                    IsAdmin = true,
                };

                user.AccessCode = passwordHasher.HashPassword(user, request.AccessCode);

                dbContext.Users.Add(user);

                await dbContext.SaveChangesAsync(ct);

                await httpContext.SignInUserAsync(user);

                return TypedResults.Ok(user.Id);
            });
    }
}
