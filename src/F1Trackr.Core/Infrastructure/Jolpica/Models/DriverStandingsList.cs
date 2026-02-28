using System.Text.Json.Serialization;

namespace F1Trackr.Core.Infrastructure.Jolpica.Models;

public class DriverStandingsList
{
    [JsonPropertyName("season")]
    public string? Season { get; set; }
    
    [JsonPropertyName("round")]
    public string? Round { get; set; }
    
    [JsonPropertyName("DriverStandings")]
    public List<DriverStanding>? DriverStandings { get; set; }
}
