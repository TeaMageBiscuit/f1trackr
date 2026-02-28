using System.Text.Json.Serialization;

namespace F1Trackr.Core.Infrastructure.Jolpica.Models;

public class RacingResult
{
    [JsonPropertyName("number")]
    public string? Number { get; set; }
    
    [JsonPropertyName("position")]
    public string? Position { get; set; }
    
    [JsonPropertyName("points")]
    public string? Points { get; set; }
    
    [JsonPropertyName("Driver")]
    public Driver? Driver { get; set; }
    
    [JsonPropertyName("Constructor")]
    public Constructor? Constructor { get; set; }
    
    [JsonPropertyName("grid")]
    public string? Grid { get; set; }
    
    [JsonPropertyName("laps")]
    public string? Laps { get; set; }
    
    [JsonPropertyName("status")]
    public string? Status { get; set; }
    
    [JsonPropertyName("Time")]
    public TimeResult? Time { get; set; }
    
    [JsonPropertyName("FastestLap")]
    public FastestLap? FastestLap { get; set; }
}
