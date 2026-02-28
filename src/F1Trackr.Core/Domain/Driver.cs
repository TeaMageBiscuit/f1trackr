namespace F1Trackr.Core.Domain;

public sealed class Driver
{
    public required DriverId Id { get; set; }

    public required string Season { get; set; }

    public required string GivenName { get; set; }

    public required string FamilyName { get; set; }

    public string? Nationality { get; set; }

    public string? Code { get; set; }

    public string? DriverNumber { get; set; }

    public string? ImageAddress { get; set; }
}
