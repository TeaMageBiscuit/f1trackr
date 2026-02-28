using System.Text.Json.Serialization;

namespace F1Trackr.Core.Infrastructure.Jolpica.Models;

public class RaceResultTable<TRaceResult>
{
    [JsonPropertyName("season")]
    public string? Season { get; set; }
    
    [JsonPropertyName("round")]
    public string? Round { get; set; }
    
    [JsonPropertyName("Races")]
    public List<TRaceResult>? RaceResults { get; set; }
}
