using F1Trackr.Core.Application.Predictions;
using F1Trackr.Core.Domain;
using F1Trackr.Core.Domain.Management;
using Microsoft.AspNetCore.Mvc;

namespace F1Trackr.Server.Endpoints.Admin.Predictions;

public sealed class AdminUpdateRacePredictionEndpoint : IEndpoint
{
    public sealed record Request(IDictionary<string, int> Positions);

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut(
            "/api/admin/groups/{groupId:guid}/members/{userId:guid}/races/{round:int}",
            async (
                [FromRoute] Guid groupId,
                [FromRoute] Guid userId,
                [FromRoute] int round,
                [FromServices] UpdateRacePrediction.CommandHandler handler,
                [FromBody] Request request,
                CancellationToken ct) =>
            {
                var command = new UpdateRacePrediction.Command(
                    new GroupId(groupId),
                    new UserId(userId),
                    round,
                    request.Positions.ToDictionary(x => new DriverId(x.Key), x => x.Value));

                var result = await handler.HandleAsync(command, ct);

                return result.ToApiResult();
            })
            .RequireAuthorization(AuthorizationPolicies.Admin);
    }
}
