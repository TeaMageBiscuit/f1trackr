using System.Text.Json.Serialization;

namespace F1Trackr.Core.Infrastructure.Jolpica.Models;

public class DriverStandingsTable
{
    [JsonPropertyName("season")]
    public string? Season { get; set; }
    
    [JsonPropertyName("StandingsLists")]
    public List<DriverStandingsList>? StandingsLists { get; set; }
}
