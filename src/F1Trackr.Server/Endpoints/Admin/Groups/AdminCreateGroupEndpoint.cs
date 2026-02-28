using F1Trackr.Core.Application.Groups;
using F1Trackr.Core.Domain.Management;
using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace F1Trackr.Server.Endpoints.Admin.Groups;

internal sealed class AdminCreateGroupEndpoint : IEndpoint
{
    private sealed record Request(string Name, int Season);

    private sealed class RequestValidator : AbstractValidator<Request>
    {
        public RequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.Season)
                .GreaterThanOrEqualTo(2025);
        }
    }

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost(
            "/api/admin/groups",
            async Task<Results<Created<GroupId>, NotFound, ForbidHttpResult, BadRequest<IDictionary<string, string[]>>>> (
                [FromBody] Request request,
                [FromServices] RequestValidator validator,
                [FromServices] CreateGroup.CommandHandler handler,
                CancellationToken ct) =>
            {
                var validationResult = await validator.ValidateAsync(request, ct);
                if (!validationResult.IsValid)
                {
                    return TypedResults.BadRequest(validationResult.ToDictionary());
                }

                var command = new CreateGroup.Command(
                    request.Name,
                    request.Season.ToString());

                var result = await handler.HandleAsync(command, ct);

                return result.ToApiResult(groupId => TypedResults.Created(
                    $"/api/admin/groups/{groupId.Value}",
                    groupId));
            })
            .RequireAuthorization(AuthorizationPolicies.Admin);
    }
}
