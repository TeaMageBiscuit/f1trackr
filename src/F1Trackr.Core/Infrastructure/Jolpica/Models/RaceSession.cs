using System.Text.Json.Serialization;

namespace F1Trackr.Core.Infrastructure.Jolpica.Models;

public class RaceSession
{
    [JsonPropertyName("date")]
    public string? Date { get; set; }
    
    [JsonPropertyName("time")]
    public string? Time { get; set; }
}
