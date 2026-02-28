using System.Text.Json;
using F1Trackr.Core.Infrastructure.Jolpica.Models;

namespace F1Trackr.Core.Infrastructure.Jolpica.Responses;

public class RacesResponse : ErgastResponse<RaceTable>
{
    public List<Race> GetRaces()
    {
        var jsonElement = ResponseData.Properties?["RaceTable"] as JsonElement?;
        var table = jsonElement?.Deserialize<RaceTable>();
        
        return table?.Races ?? [];
    }
}
