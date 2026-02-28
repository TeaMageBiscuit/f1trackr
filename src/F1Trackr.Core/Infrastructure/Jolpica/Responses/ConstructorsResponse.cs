using System.Text.Json;
using F1Trackr.Core.Infrastructure.Jolpica.Models;

namespace F1Trackr.Core.Infrastructure.Jolpica.Responses;

public class ConstructorsResponse : ErgastResponse<ConstructorTable>
{
    public List<Constructor> GetConstructors()
    {
        var jsonElement = ResponseData.Properties?["ConstructorTable"] as JsonElement?;
        var table = jsonElement?.Deserialize<ConstructorTable>();

        return table?.Constructors ?? [];
    }
}
