using F1Trackr.Core.Domain;

namespace F1Trackr.Core.Application.FormulaOne;

public sealed record RaceSummary(
    RaceId Id,
    string Name,
    string Circuit,
    string Location,
    string Country,
    DateTimeOffset GrandPrixTime,
    DateTimeOffset? FirstPracticeTime);
