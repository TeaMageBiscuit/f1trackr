using System.Text.Json.Serialization;

namespace F1Trackr.Core.Infrastructure.Jolpica.Responses;

public class ErgastResponse<T>
{
    [JsonPropertyName("MRData")]
    public required ResponseData<T> ResponseData { get; set; }
}
