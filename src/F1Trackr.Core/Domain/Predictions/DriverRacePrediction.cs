namespace F1Trackr.Core.Domain.Predictions;

public sealed record DriverRacePrediction(
    RaceId RaceId,
    ICollection<DriverPrediction> Drivers);
