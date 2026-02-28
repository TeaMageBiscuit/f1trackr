namespace F1Trackr.Core.Domain;

public sealed record QualifyingPosition(
    DriverId DriverId,
    ConstructorId ConstructorId,
    int Position,
    string? Q1,
    string? Q2,
    string? Q3);
