namespace F1Trackr.Core.Domain.Predictions;

public sealed record DriverSeasonPrediction(
    string Season,
    IReadOnlyCollection<DriverPrediction> Drivers);
