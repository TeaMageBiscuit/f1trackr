using System.Text.Json.Serialization;

namespace F1Trackr.Core.Infrastructure.Jolpica.Models;

public class DriverStanding
{
    [JsonPropertyName("position")]
    public string? Position { get; set; }
    
    [JsonPropertyName("points")]
    public string? Points { get; set; }
    
    [JsonPropertyName("wins")]
    public string? Wins { get; set; }
    
    [JsonPropertyName("Driver")]
    public Driver? Driver { get; set; }
    
    [JsonPropertyName("Constructors")]
    public List<Constructor>? Constructors { get; set; }
}
