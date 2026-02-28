using F1Trackr.Core.Domain;

namespace F1Trackr.Core.Application.FormulaOne;

public sealed record RaceOverview(
    RaceId Id,
    string Name,
    string Circuit,
    string Location,
    string Country,
    DateTimeOffset GrandPrixTime,
    DateTimeOffset? FirstPracticeTime,
    DateTimeOffset? SecondPracticeTime,
    DateTimeOffset? ThirdPracticeTime,
    DateTimeOffset? QualifyingTime,
    DateTimeOffset? SprintQualifyingTime,
    DateTimeOffset? SprintTime,
    IReadOnlyCollection<DriverPosition>? SprintDriverResults,
    IReadOnlyCollection<QualifyingPosition>? QualifyingResults,
    IReadOnlyCollection<DriverPosition>? DriverResults,
    IReadOnlyCollection<ConstructorStanding>? ConstructorStandingsSnapshot,
    IReadOnlyCollection<DriverStanding>? DriverStandingsSnapshot);
