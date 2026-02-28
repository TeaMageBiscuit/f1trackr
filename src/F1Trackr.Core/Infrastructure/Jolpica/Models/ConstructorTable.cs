using System.Text.Json.Serialization;

namespace F1Trackr.Core.Infrastructure.Jolpica.Models;

public class ConstructorTable
{
    [JsonPropertyName("season")]
    public string? Season { get; set; }
    
    [JsonPropertyName("Constructors")]
    public List<Constructor>? Constructors { get; set; }
}
