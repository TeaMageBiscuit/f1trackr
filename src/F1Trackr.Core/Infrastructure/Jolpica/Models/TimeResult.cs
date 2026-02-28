using System.Text.Json.Serialization;

namespace F1Trackr.Core.Infrastructure.Jolpica.Models;

public class TimeResult
{
    [JsonPropertyName("millis")]
    public string? Milliseconds { get; set; }

    [JsonPropertyName("time")]
    public string? Time { get; set; }
}
