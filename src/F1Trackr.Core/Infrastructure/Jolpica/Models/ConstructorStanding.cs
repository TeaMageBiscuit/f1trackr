using System.Text.Json.Serialization;

namespace F1Trackr.Core.Infrastructure.Jolpica.Models;

public class ConstructorStanding
{
    [JsonPropertyName("position")]
    public string? Position { get; set; }
    
    [JsonPropertyName("points")]
    public string? Points { get; set; }
    
    [JsonPropertyName("wins")]
    public string? Wins { get; set; }
    
    [JsonPropertyName("Constructor")]
    public Constructor? Constructor { get; set; }
}
