using F1Trackr.Core.Domain.Management;
using F1Trackr.Core.Infrastructure.EntityFramework;
using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace F1Trackr.Server.Endpoints.Account;

internal sealed class LoginEndpoint : IEndpoint
{
    private sealed record LoginRequest(string UserName, string AccessCode);

    private sealed class LoginRequestValidator : AbstractValidator<LoginRequest>
    {
        public LoginRequestValidator()
        {
            RuleFor(x => x.UserName)
                .NotEmpty();

            RuleFor(x => x.AccessCode)
                .NotEmpty();
        }
    }

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost(
            "/api/account/login",
            async Task<Results<NoContent, BadRequest<IDictionary<string, string[]>>>> (
                HttpContext httpContext,
                [FromBody] LoginRequest request,
                [FromServices] LoginRequestValidator validator,
                [FromServices] TrackrDbContext dbContext,
                [FromServices] IPasswordHasher<User> passwordHasher) =>
        {
            var validationResult = await validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                return TypedResults.BadRequest(validationResult.ToDictionary());
            }

            var user = await dbContext.Users.SingleOrDefaultAsync(u => u.Name == request.UserName);
            if (user is null)
            {
                return TypedResults.BadRequest<IDictionary<string, string[]>>(
                    new Dictionary<string, string[]>
                    {
                        [string.Empty] = ["Invalid user name or access code"],
                    });
            }

            var accessCodeResult = passwordHasher.VerifyHashedPassword(user, user.AccessCode, request.AccessCode);
            if (accessCodeResult == PasswordVerificationResult.Failed)
            {
                return TypedResults.BadRequest<IDictionary<string, string[]>>(
                    new Dictionary<string, string[]>
                    {
                        [string.Empty] = ["Invalid user name or access code"],
                    });
            }

            if (accessCodeResult == PasswordVerificationResult.SuccessRehashNeeded)
            {
                user.AccessCode = passwordHasher.HashPassword(user, request.AccessCode);
                await dbContext.SaveChangesAsync();
            }

            await httpContext.SignInUserAsync(user);

            return TypedResults.NoContent();
        });
    }
}
