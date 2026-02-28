namespace F1Trackr.Core.Domain;

public sealed class Constructor
{
    public required ConstructorId Id { get; set; }

    public required string Season { get; set; }

    public required string Name { get; set; }

    public string? Nationality { get; set; }

    public string? LogoAddress { get; set; }
}
