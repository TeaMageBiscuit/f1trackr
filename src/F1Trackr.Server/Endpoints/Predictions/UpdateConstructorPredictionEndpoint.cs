using System.Security.Claims;
using F1Trackr.Core;
using F1Trackr.Core.Application.Predictions;
using F1Trackr.Core.Domain;
using F1Trackr.Core.Domain.Management;
using Microsoft.AspNetCore.Mvc;

namespace F1Trackr.Server.Endpoints.Predictions;

public sealed class UpdateConstructorPredictionEndpoint : IEndpoint
{
    public sealed record Request(IDictionary<string, int> Positions);

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut(
            "/api/groups/{groupId:guid}/constructors",
            async (
                [FromRoute] Guid groupId,
                [FromServices] UpdateConstructorPrediction.CommandHandler handler,
                [FromBody] Request request,
                ClaimsPrincipal currentUser,
                CancellationToken ct) =>
            {
                var command = new UpdateConstructorPrediction.Command(
                    new GroupId(groupId),
                    currentUser.UserId!,
                    request.Positions.ToDictionary(x => new ConstructorId(x.Key), x => x.Value));

                var result = await handler.HandleAsync(command, ct);

                return result.ToApiResult();
            })
            .RequireAuthorization();
    }
}
