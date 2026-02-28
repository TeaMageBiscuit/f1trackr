using F1Trackr.Core.Application.Users;
using F1Trackr.Core.Domain.Management;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace F1Trackr.Server.Endpoints.Admin.Users;

internal sealed class AdminUpdateUserEndpoint : IEndpoint
{
    private sealed record Request(string Name, string? AccessCode, bool IsAdmin);

    private sealed class RequestValidator : AbstractValidator<Request>
    {
        public RequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.AccessCode)
                .MaximumLength(200)
                .When(x => !string.IsNullOrWhiteSpace(x.AccessCode));
        }
    }

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut(
            "/api/admin/users/{userId:guid}",
            async (
                [FromRoute] Guid userId,
                [FromBody] Request request,
                [FromServices] RequestValidator validator,
                [FromServices] AdminUpdateUser.CommandHandler handler,
                CancellationToken ct) =>
            {
                var validationResult = await validator.ValidateAsync(request, ct);
                if (!validationResult.IsValid)
                {
                    return Results.BadRequest(validationResult.ToDictionary());
                }

                var command = new AdminUpdateUser.Command(
                    new UserId(userId),
                    request.Name,
                    request.AccessCode,
                    request.IsAdmin);

                var result = await handler.HandleAsync(command, ct);

                return result.ToApiResult();
            })
            .RequireAuthorization(AuthorizationPolicies.Admin);
    }
}
