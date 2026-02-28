using System.Text.Json;
using F1Trackr.Core.Infrastructure.Jolpica.Models;

namespace F1Trackr.Core.Infrastructure.Jolpica.Responses;

public class ResultsResponse : ErgastResponse<RaceResultTable<RaceResult>>
{
    public List<RaceResult> GetResults()
    {
        var jsonElement = ResponseData.Properties?["RaceTable"] as JsonElement?;
        var table = jsonElement?.Deserialize<RaceResultTable<RaceResult>>();
        
        return table?.RaceResults ?? [];
    }
}
