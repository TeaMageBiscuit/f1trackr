using F1Trackr.Core.Application.Users;
using F1Trackr.Core.Domain.Management;
using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace F1Trackr.Server.Endpoints.Admin.Users;

internal sealed class AdminCreateUserEndpoint : IEndpoint
{
    private sealed record Request(string Name, string AccessCode, bool IsAdmin);

    private sealed class RequestValidator : AbstractValidator<Request>
    {
        public RequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.AccessCode)
                .NotEmpty()
                .MaximumLength(200);
        }
    }

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost(
            "/api/admin/users",
            async Task<Results<Created<UserId>, NotFound, ForbidHttpResult, BadRequest<IDictionary<string, string[]>>>> (
                [FromBody] Request request,
                [FromServices] RequestValidator validator,
                [FromServices] AdminCreateUser.CommandHandler handler,
                CancellationToken ct) =>
            {
                var validationResult = await validator.ValidateAsync(request, ct);
                if (!validationResult.IsValid)
                {
                    return TypedResults.BadRequest(validationResult.ToDictionary());
                }

                var command = new AdminCreateUser.Command(
                    request.Name,
                    request.AccessCode,
                    request.IsAdmin);

                var result = await handler.HandleAsync(command, ct);

                return result.ToApiResult(userId => TypedResults.Created(
                    $"/api/admin/users/{userId.Value}",
                    userId));
            })
            .RequireAuthorization(AuthorizationPolicies.Admin);
    }
}
