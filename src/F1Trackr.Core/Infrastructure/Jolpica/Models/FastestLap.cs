using System.Text.Json.Serialization;

namespace F1Trackr.Core.Infrastructure.Jolpica.Models;

public class FastestLap
{
    [JsonPropertyName("rank")]
    public string? Rank { get; set; }

    [JsonPropertyName("lap")]
    public string? Lap { get; set; }

    [JsonPropertyName("Time")]
    public FastestLapTime? Time { get; set; }
}
