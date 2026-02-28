namespace F1Trackr.Core.Domain;

public sealed class RaceResult
{
    public required RaceId RaceId
    {
        get => new RaceId(Season, Round);
        init => (Season, Round) = (value.Season, value.Round);
    }

    internal string Season { get; private set; } = string.Empty;

    internal int Round { get; private set; }

    public required DateTimeOffset UpdatedAt { get; set; }

    public ICollection<DriverPosition> SprintDriverResults { get; set; } = [];

    public ICollection<QualifyingPosition> QualifyingResults { get; set; } = [];

    public ICollection<DriverPosition> DriverResults { get; set; } = [];

    public ICollection<ConstructorStanding> ConstructorStandingsSnapshot { get; set; } = [];

    public ICollection<DriverStanding> DriverStandingsSnapshot { get; set; } = [];
}
