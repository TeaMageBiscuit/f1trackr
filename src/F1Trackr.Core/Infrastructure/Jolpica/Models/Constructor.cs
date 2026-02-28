using System.Text.Json.Serialization;

namespace F1Trackr.Core.Infrastructure.Jolpica.Models;

public class Constructor
{
    [JsonPropertyName("constructorId")]
    public required string ConstructorId { get; set; }
    
    [JsonPropertyName("url")]
    public string? Url { get; set; }
    
    [JsonPropertyName("name")]
    public required string Name { get; set; }
    
    [JsonPropertyName("nationality")]
    public string? Nationality { get; set; }
}
