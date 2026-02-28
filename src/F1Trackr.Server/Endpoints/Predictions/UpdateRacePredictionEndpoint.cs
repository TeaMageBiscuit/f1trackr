using System.Security.Claims;
using F1Trackr.Core;
using F1Trackr.Core.Application.Predictions;
using F1Trackr.Core.Domain;
using F1Trackr.Core.Domain.Management;
using Microsoft.AspNetCore.Mvc;

namespace F1Trackr.Server.Endpoints.Predictions;

public sealed class UpdateRacePredictionEndpoint : IEndpoint
{
    public sealed record Request(IDictionary<string, int> Positions);

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut(
            "/api/groups/{groupId:guid}/races/{round:int}",
            async (
                [FromRoute] Guid groupId,
                [FromRoute] int round,
                [FromServices] UpdateRacePrediction.CommandHandler handler,
                [FromBody] Request request,
                ClaimsPrincipal currentUser,
                CancellationToken ct) =>
            {
                var command = new UpdateRacePrediction.Command(
                    new GroupId(groupId),
                    currentUser.UserId!,
                    round,
                    request.Positions.ToDictionary(x => new DriverId(x.Key), x => x.Value));

                var result = await handler.HandleAsync(command, ct);

                return result.ToApiResult();
            })
            .RequireAuthorization();
    }
}
