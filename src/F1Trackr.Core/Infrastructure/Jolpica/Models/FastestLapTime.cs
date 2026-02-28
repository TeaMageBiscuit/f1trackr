using System.Text.Json.Serialization;

namespace F1Trackr.Core.Infrastructure.Jolpica.Models;

public class FastestLapTime
{
    [JsonPropertyName("time")]
    public string? Time { get; set; }
}
