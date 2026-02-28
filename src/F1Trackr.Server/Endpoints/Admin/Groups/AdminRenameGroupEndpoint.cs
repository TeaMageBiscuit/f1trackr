using F1Trackr.Core.Application.Groups;
using F1Trackr.Core.Domain.Management;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace F1Trackr.Server.Endpoints.Admin.Groups;

public sealed class AdminRenameGroupEndpoint : IEndpoint
{
    private sealed record Request(string Name);

    private sealed class RequestValidator : AbstractValidator<Request>
    {
        public RequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(100);
        }
    }

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut(
            "/api/admin/groups/{groupId:guid}/name",
            async (
                [FromRoute] Guid groupId,
                [FromBody] Request request,
                [FromServices] RequestValidator validator,
                [FromServices] RenameGroup.CommandHandler handler,
                CancellationToken ct) =>
            {
                var validationResult = await validator.ValidateAsync(request, ct);
                if (!validationResult.IsValid)
                {
                    return Results.BadRequest(validationResult.ToDictionary());
                }

                var command = new RenameGroup.Command(
                    new GroupId(groupId),
                    request.Name);

                var result = await handler.HandleAsync(command, ct);

                return result.ToApiResult();
            })
            .RequireAuthorization(AuthorizationPolicies.Admin);
    }
}
