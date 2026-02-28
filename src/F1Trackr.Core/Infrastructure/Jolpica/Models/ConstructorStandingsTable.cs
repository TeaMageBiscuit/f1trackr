using System.Text.Json.Serialization;

namespace F1Trackr.Core.Infrastructure.Jolpica.Models;

public sealed class ConstructorStandingsTable
{
    [JsonPropertyName("season")]
    public string? Season { get; set; }

    [JsonPropertyName("round")]
    public string? Round { get; set; }

    [JsonPropertyName("StandingsLists")]
    public List<ConstructorStandingsList>? StandingsLists { get; set; }
}
