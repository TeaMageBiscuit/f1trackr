using System.Text.Json.Serialization;

namespace F1Trackr.Core.Infrastructure.Jolpica.Models;

public class SprintRaceResult
{
    [JsonPropertyName("season")]
    public string? Season { get; set; }
    
    [JsonPropertyName("round")]
    public string? Round { get; set; }
    
    [JsonPropertyName("url")]
    public string? Url { get; set; }
    
    [JsonPropertyName("raceName")]
    public string? RaceName { get; set; }
    
    [JsonPropertyName("Circuit")]
    public Circuit? Circuit { get; set; }
    
    [JsonPropertyName("date")]
    public string? Date { get; set; }
    
    [JsonPropertyName("time")]
    public string? Time { get; set; }
    
    [JsonPropertyName("SprintResults")]
    public List<RacingResult>? SprintResults { get; set; }
}
