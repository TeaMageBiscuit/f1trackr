using System.Text.Json;
using F1Trackr.Core.Infrastructure.Jolpica.Models;

namespace F1Trackr.Core.Infrastructure.Jolpica.Responses;

public class SprintsResponse : ErgastResponse<SprintRaceResultTable>
{
    public List<SprintRaceResult> GetResults()
    {
        var jsonElement = ResponseData.Properties?["RaceTable"] as JsonElement?;
        var table = jsonElement?.Deserialize<SprintRaceResultTable>();
        
        return table?.SprintRaceResults ?? [];
    }
}
