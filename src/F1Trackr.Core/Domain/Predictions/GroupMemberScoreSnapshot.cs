namespace F1Trackr.Core.Domain.Predictions;

public sealed record GroupMemberScoreSnapshot(
    RaceId RaceId,
    int BasePoints,
    int ConstructorStandingsPenalty,
    int DriverStandingsPenalty,
    int RaceBonusThisRound,
    int RaceBonusTotal,
    int TotalPoints,
    DateTimeOffset ComputedAt);
