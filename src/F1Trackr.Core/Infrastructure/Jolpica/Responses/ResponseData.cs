using System.Text.Json.Serialization;

namespace F1Trackr.Core.Infrastructure.Jolpica.Responses;

public class ResponseData<T>
{
    [JsonPropertyName("xmlns")]
    public string? Xmlns { get; set; }
    
    [JsonPropertyName("series")]
    public string? Series { get; set; }
    
    [JsonPropertyName("url")]
    public string? Url { get; set; }
    
    [JsonPropertyName("limit")]
    public string? Limit { get; set; }
    
    [JsonPropertyName("offset")]
    public string? Offset { get; set; }
    
    [JsonPropertyName("total")]
    public string? Total { get; set; }

    [JsonExtensionData]
    public Dictionary<string, object>? Properties { get; set; }
}
