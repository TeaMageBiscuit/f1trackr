using System.Text.Json;
using F1Trackr.Core.Infrastructure.Jolpica.Models;

namespace F1Trackr.Core.Infrastructure.Jolpica.Responses;

public sealed class DriverStandingsResponse : ErgastResponse<DriverStandingsTable>
{
    public List<DriverStanding> GetStandings()
    {
        var jsonElement = ResponseData.Properties?["StandingsTable"] as JsonElement?;
        var table = jsonElement?.Deserialize<DriverStandingsTable>();

        return table?.StandingsLists?.SelectMany(list => list.DriverStandings ?? []).ToList() ?? [];
    }
}
