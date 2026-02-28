using System.Text.Json.Serialization;

namespace F1Trackr.Core.Infrastructure.Jolpica.Models;

public class Race
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
    
    [JsonPropertyName("FirstPractice")]
    public RaceSession? FirstPractice { get; set; }
    
    [JsonPropertyName("SecondPractice")]
    public RaceSession? SecondPractice { get; set; }
    
    [JsonPropertyName("ThirdPractice")]
    public RaceSession? ThirdPractice { get; set; }
    
    [JsonPropertyName("Qualifying")]
    public RaceSession? Qualifying { get; set; }
    
    [JsonPropertyName("SprintQualifying")]
    public RaceSession? SprintQualifying { get; set; }
    
    [JsonPropertyName("Sprint")]
    public RaceSession? Sprint { get; set; }
}
