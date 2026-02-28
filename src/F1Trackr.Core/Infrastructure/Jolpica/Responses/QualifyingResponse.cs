using System.Text.Json;
using F1Trackr.Core.Infrastructure.Jolpica.Models;

namespace F1Trackr.Core.Infrastructure.Jolpica.Responses;

public class QualifyingResponse : ErgastResponse<RaceResultTable<QualifyingRaceResult>>
{
    public List<QualifyingRaceResult> GetResults()
    {
        var jsonElement = ResponseData.Properties?["RaceTable"] as JsonElement?;
        var table = jsonElement?.Deserialize<RaceResultTable<QualifyingRaceResult>>();
        
        return table?.RaceResults ?? [];
    }
}
