using F1Trackr.Core.Domain.Predictions;

namespace F1Trackr.Core.Domain.Management;

public sealed class GroupMember
{
    public required GroupId GroupId { get; set; }

    public required UserId UserId { get; set; }

    public required User User { get; set; }

    public required int CurrentScore { get; set; }

    public ICollection<GroupMemberScoreSnapshot> ScoreSnapshots { get; set; } = [];

    public ICollection<ConstructorPrediction> ConstructorPredictions { get; set; } = [];

    public ICollection<DriverPrediction> DriverPredictions { get; set; } = [];

    public ICollection<DriverRacePrediction> DriverRacePredictions { get; set; } = [];
}
