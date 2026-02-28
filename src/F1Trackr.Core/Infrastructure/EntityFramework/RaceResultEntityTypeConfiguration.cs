using System.Text.Json;
using F1Trackr.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace F1Trackr.Core.Infrastructure.EntityFramework;

internal sealed class RaceResultEntityTypeConfiguration : IEntityTypeConfiguration<RaceResult>
{
    public void Configure(EntityTypeBuilder<RaceResult> builder)
    {
        builder.ToTable("RaceResults");

        builder.ComplexProperty(x => x.RaceId, raceId =>
        {
            raceId.Property(x => x.Season)
                .HasColumnName("Season")
                .HasMaxLength(4)
                .IsRequired();

            raceId.Property(x => x.Round)
                .HasColumnName("Round")
                .IsRequired();
        });

        builder.Property(x => x.Season)
            .HasColumnName("Season")
            .HasMaxLength(4)
            .IsRequired();

        builder.Property(x => x.Round)
            .HasColumnName("Round")
            .IsRequired();

        builder.HasKey(x => new { x.Season, x.Round });

        builder.Property(x => x.UpdatedAt).IsRequired();

        builder.Property(x => x.SprintDriverResults)
            .HasConversion(
                items => JsonSerializer.Serialize(items),
                json => !string.IsNullOrWhiteSpace(json)
                    ?  JsonSerializer.Deserialize<ICollection<DriverPosition>>(json) ?? new List<DriverPosition>()
                    : new List<DriverPosition>(),
                new ValueComparer<ICollection<DriverPosition>>(
                    (a, b) => a != null && b != null && a.SequenceEqual(b),
                    items => items.Aggregate(0, (acc, item) => HashCode.Combine(acc, item.GetHashCode())),
                    items => items.ToList()));

        builder.Property(x => x.QualifyingResults)
            .HasConversion(
                items => JsonSerializer.Serialize(items),
                json => !string.IsNullOrWhiteSpace(json)
                    ? JsonSerializer.Deserialize<ICollection<QualifyingPosition>>(json) ?? new List<QualifyingPosition>()
                    : new List<QualifyingPosition>(),
                new ValueComparer<ICollection<QualifyingPosition>>(
                    (a, b) => a != null && b != null && a.SequenceEqual(b),
                    items => items.Aggregate(0, (acc, item) => HashCode.Combine(acc, item.GetHashCode())),
                    items => items.ToList()));

        builder.Property(x => x.DriverResults)
            .HasConversion(
                items => JsonSerializer.Serialize(items),
                json => !string.IsNullOrWhiteSpace(json)
                    ? JsonSerializer.Deserialize<ICollection<DriverPosition>>(json) ?? new List<DriverPosition>()
                    : new List<DriverPosition>(),
                new ValueComparer<ICollection<DriverPosition>>(
                    (a, b) => a != null && b != null && a.SequenceEqual(b),
                    items => items.Aggregate(0, (acc, item) => HashCode.Combine(acc, item.GetHashCode())),
                    items => items.ToList()));

        builder.Property(x => x.ConstructorStandingsSnapshot)
            .HasConversion(
                items => JsonSerializer.Serialize(items),
                json => !string.IsNullOrWhiteSpace(json)
                    ? JsonSerializer.Deserialize<ICollection<ConstructorStanding>>(json) ?? new List<ConstructorStanding>()
                    : new List<ConstructorStanding>(),
                new ValueComparer<ICollection<ConstructorStanding>>(
                    (a, b) => a != null && b != null && a.SequenceEqual(b),
                    items => items.Aggregate(0, (acc, item) => HashCode.Combine(acc, item.GetHashCode())),
                    items => items.ToList()));

        builder.Property(x => x.DriverStandingsSnapshot)
            .HasConversion(
                items => JsonSerializer.Serialize(items),
                json => !string.IsNullOrWhiteSpace(json)
                    ? JsonSerializer.Deserialize<ICollection<DriverStanding>>(json) ?? new List<DriverStanding>()
                    : new List<DriverStanding>(),
                new ValueComparer<ICollection<DriverStanding>>(
                    (a, b) => a != null && b != null && a.SequenceEqual(b),
                    items => items.Aggregate(0, (acc, item) => HashCode.Combine(acc, item.GetHashCode())),
                    items => items.ToList()));
    }
}
