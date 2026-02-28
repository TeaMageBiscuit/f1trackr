using System.Text.Json.Serialization;

namespace F1Trackr.Core.Infrastructure.Jolpica.Models;

public class Circuit
{
    [JsonPropertyName("circuitId")]
    public required string CircuitId { get; set; }
    
    [JsonPropertyName("url")]
    public string? Url { get; set; }
    
    [JsonPropertyName("circuitName")]
    public string? CircuitName { get; set; }
    
    [JsonPropertyName("Location")]
    public Location? Location { get; set; }
}
