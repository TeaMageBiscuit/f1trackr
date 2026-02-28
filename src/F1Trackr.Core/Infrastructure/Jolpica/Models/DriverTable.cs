using System.Text.Json.Serialization;

namespace F1Trackr.Core.Infrastructure.Jolpica.Models;

public class DriverTable
{
    [JsonPropertyName("season")]
    public string? Season { get; set; }
    
    [JsonPropertyName("Drivers")]
    public List<Driver>? Drivers { get; set; }
}
