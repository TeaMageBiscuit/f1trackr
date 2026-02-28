using System.Text.Json;
using F1Trackr.Core.Infrastructure.Jolpica.Models;

namespace F1Trackr.Core.Infrastructure.Jolpica.Responses;

public class DriversResponse : ErgastResponse<DriverTable>
{
    public List<Driver> GetDrivers()
    {
        var jsonElement = ResponseData.Properties?["DriverTable"] as JsonElement?;
        var table = jsonElement?.Deserialize<DriverTable>();

        return table?.Drivers ?? [];
    }
}
