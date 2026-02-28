namespace F1Trackr.Core.Domain;

public sealed record DriverPosition(
    DriverId DriverId,
    ConstructorId ConstructorId,
    int Position,
    int Points,
    int Grid,
    int Laps,
    string Status,
    string? TotalTime,
    string? FastestLapTime);
