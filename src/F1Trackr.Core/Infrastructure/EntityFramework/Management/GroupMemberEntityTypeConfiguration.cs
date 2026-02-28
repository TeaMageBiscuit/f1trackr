using System.Text.Json;
using F1Trackr.Core.Domain.Management;
using F1Trackr.Core.Domain.Predictions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace F1Trackr.Core.Infrastructure.EntityFramework.Management;

internal sealed class GroupMemberEntityTypeConfiguration : IEntityTypeConfiguration<GroupMember>
{
    public void Configure(EntityTypeBuilder<GroupMember> builder)
    {
        builder.ToTable("GroupMembers");
        builder.HasKey(m => new
        {
            m.GroupId,
            m.UserId,
        });

        builder.Property(m => m.GroupId)
            .IsRequired()
            .HasConversion(x => x.Value, value => new GroupId(value));

        builder.Property(m => m.UserId)
            .IsRequired()
            .HasConversion(x => x.Value, value => new UserId(value));

        builder.HasOne(m => m.User).WithMany().HasForeignKey(m => m.UserId);

        builder.Property(m => m.ConstructorPredictions)
            .HasConversion(
                predictions => JsonSerializer.Serialize(predictions),
                json => JsonSerializer.Deserialize<ICollection<ConstructorPrediction>>(json) ?? new List<ConstructorPrediction>(),
                new ValueComparer<ICollection<ConstructorPrediction>>(
                    (a, b) => a != null && b != null && a.SequenceEqual(b),
                    predictions => predictions.Aggregate(0, (a, prediction) => HashCode.Combine(a, prediction.GetHashCode())),
                    predictions => predictions.ToList()));

        builder.Property(m => m.DriverPredictions)
            .HasConversion(
                predictions => JsonSerializer.Serialize(predictions),
                json => JsonSerializer.Deserialize<ICollection<DriverPrediction>>(json) ?? new List<DriverPrediction>(),
                new ValueComparer<ICollection<DriverPrediction>>(
                    (a, b) => a != null && b != null && a.SequenceEqual(b),
                    predictions => predictions.Aggregate(0, (a, prediction) => HashCode.Combine(a, prediction.GetHashCode())),
                    predictions => predictions.ToList()));

        builder.Property(m => m.DriverRacePredictions)
            .HasConversion(
                predictions => JsonSerializer.Serialize(predictions),
                json => JsonSerializer.Deserialize<ICollection<DriverRacePrediction>>(json) ?? new List<DriverRacePrediction>(),
                new ValueComparer<ICollection<DriverRacePrediction>>(
                    (a, b) => a != null && b != null && a.SequenceEqual(b),
                    predictions => predictions.Aggregate(0, (a, prediction) => HashCode.Combine(a, prediction.GetHashCode())),
                    predictions => predictions.ToList()));

        builder.Property(m => m.ScoreSnapshots)
            .HasConversion(
                snapshots => JsonSerializer.Serialize(snapshots),
                json => JsonSerializer.Deserialize<ICollection<GroupMemberScoreSnapshot>>(json) ?? new List<GroupMemberScoreSnapshot>(),
                new ValueComparer<ICollection<GroupMemberScoreSnapshot>>(
                    (a, b) => a != null && b != null && a.SequenceEqual(b),
                    snapshots => snapshots.Aggregate(0, (a, s) => HashCode.Combine(a, s.GetHashCode())),
                    snapshots => snapshots.ToList()));
    }
}
