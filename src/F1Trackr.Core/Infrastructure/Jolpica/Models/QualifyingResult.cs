using System.Text.Json.Serialization;

namespace F1Trackr.Core.Infrastructure.Jolpica.Models;

public class QualifyingResult
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
    
    [JsonPropertyName("Q1")]
    public string? Q1 { get; set; }

    [JsonPropertyName("Q2")]
    public string? Q2 { get; set; }

    [JsonPropertyName("Q3")]
    public string? Q3 { get; set; }
}
