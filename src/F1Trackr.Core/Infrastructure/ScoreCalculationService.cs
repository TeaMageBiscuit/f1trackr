using F1Trackr.Core.Domain;
using F1Trackr.Core.Domain.Management;
using F1Trackr.Core.Domain.Predictions;
using F1Trackr.Core.Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace F1Trackr.Core.Infrastructure;

public sealed class ScoreCalculationService
{
    private readonly TrackrDbContext _dbContext;

    public ScoreCalculationService(TrackrDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task HandleAsync(RaceId raceId, CancellationToken cancellationToken)
    {
        var raceResult = await _dbContext.RaceResults
            .Where(r => r.RaceId == raceId)
            .SingleOrDefaultAsync(cancellationToken);

        if (raceResult is null)
        {
            // not scorable yet
            return;
        }

        var groups = await _dbContext.Groups
            .Include(g => g.Members)
            .Where(g => g.Season == raceId.Season)
            .ToListAsync(cancellationToken);

        foreach (var member in groups.SelectMany(group => group.Members))
        {
            UpdateMemberSnapshotForRound(member, raceId, raceResult);
        }

        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    private static void UpdateMemberSnapshotForRound(
        GroupMember member,
        RaceId raceId,
        RaceResult result)
    {
        var predictedDrivers = member.DriverPredictions
            .ToDictionary(x => x.Driver.Id, x => x.PredictedPosition);

        var actualDrivers = result.DriverStandingsSnapshot
            .ToDictionary(x => x.DriverId, x => x.Position);

        var predictedConstructors = member.ConstructorPredictions
            .ToDictionary(x => x.Constructor.Id, x => x.PredictedPosition);

        var actualConstructors = result.ConstructorStandingsSnapshot
            .ToDictionary(x => x.ConstructorId, x => x.Position);

        var driverPenalty = CalculateStandingsPenalty(predictedDrivers, actualDrivers);
        var constructorPenalty = CalculateStandingsPenalty(predictedConstructors, actualConstructors);

        var bonusThisRound = 0;

        var racePrediction = member.DriverRacePredictions
            .SingleOrDefault(x => x.RaceId == raceId);

        if (racePrediction is not null)
        {
            var predictedTop10 = racePrediction.Drivers
                .ToDictionary(x => x.PredictedPosition, x => x.Driver.Id);

            var actualTop10 = result.DriverResults
                .ToDictionary(x => x.Position, x => x.DriverId);

            bonusThisRound = CalculateRaceBonus(predictedTop10, actualTop10);
        }

        var previousBonusTotal = member.ScoreSnapshots
            .Where(s => s.RaceId.Round < raceId.Round)
            .OrderByDescending(s => s.RaceId.Round)
            .Select(s => s.RaceBonusTotal)
            .FirstOrDefault();

        var bonusTotal = previousBonusTotal + bonusThisRound;

        var total = Scoring.BaseScore + bonusTotal - driverPenalty - constructorPenalty;

        var snapshot = new GroupMemberScoreSnapshot(
            raceId,
            Scoring.BaseScore,
            constructorPenalty,
            driverPenalty,
            bonusThisRound,
            bonusTotal,
            total,
            DateTimeOffset.UtcNow);

        var existingSnapshot = member.ScoreSnapshots.SingleOrDefault(s => s.RaceId == raceId);
        if (existingSnapshot is not null)
        {
            member.ScoreSnapshots.Remove(existingSnapshot);
        }

        member.ScoreSnapshots.Add(snapshot);

        member.CurrentScore = member.ScoreSnapshots
            .OrderByDescending(s => s.RaceId.Round)
            .First()
            .TotalPoints;
    }

    private static int CalculateStandingsPenalty<TKey>(
        Dictionary<TKey, int> predicted,
        Dictionary<TKey, int> actual)
        where TKey : notnull
    {
        var penalty = 0;

        foreach (var (driverId, position) in predicted)
        {
            if (!actual.TryGetValue(driverId, out var actualPosition))
            {
                continue;
            }

            penalty += Math.Abs(position - actualPosition);
        }

        return penalty;
    }

    private static int CalculateRaceBonus(
        IDictionary<int, DriverId> predicted,
        IDictionary<int, DriverId> actual)
    {
        var bonus = 0;

        for (var position = 1; position <= 10; position++)
        {
            if (predicted.TryGetValue(position, out var p) && actual.TryGetValue(position, out var a) && p == a)
            {
                bonus++;
            }
        }

        return bonus;
    }
}
