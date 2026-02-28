namespace F1Trackr.Core.Domain;

public sealed class Race
{
    public required RaceId Id
    {
        get => new RaceId(Season, Round);
        init => (Season, Round) = (value.Season, value.Round);
    }

    internal string Season { get; private set; } = string.Empty;

    internal int Round { get; private set; }

    public required string Name { get; set; }

    public required string Circuit { get; set; }

    public required string Location { get; set; }

    public required string Country { get; set; }

    public required DateTimeOffset GrandPrixTime { get; set; }

    public DateTimeOffset? FirstPracticeTime { get; set; }

    public DateTimeOffset? SecondPracticeTime { get; set; }

    public DateTimeOffset? ThirdPracticeTime { get; set; }

    public DateTimeOffset? QualifyingTime { get; set; }

    public DateTimeOffset? SprintQualifyingTime { get; set; }

    public DateTimeOffset? SprintTime { get; set; }
}
