using System.Text.Json;
using F1Trackr.Core.Infrastructure.Jolpica.Models;

namespace F1Trackr.Core.Infrastructure.Jolpica.Responses;

public sealed class ConstructorStandingsResponse : ErgastResponse<ConstructorStandingsTable>
{
    public List<ConstructorStanding> GetStandings()
    {
        var jsonElement = ResponseData.Properties?["StandingsTable"] as JsonElement?;
        var table = jsonElement?.Deserialize<ConstructorStandingsTable>();

        return table?.StandingsLists?.SelectMany(list => list.ConstructorStandings ?? []).ToList() ?? [];
    }
}
