using F1Trackr.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace F1Trackr.Core.Infrastructure.EntityFramework;

internal sealed class RaceEntityTypeConfiguration : IEntityTypeConfiguration<Race>
{
    public void Configure(EntityTypeBuilder<Race> builder)
    {
        builder.ToTable("Races");

        builder.ComplexProperty(x => x.Id, raceId =>
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

        builder.Property(x => x.Name)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(x => x.Circuit)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(x => x.Location)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(x => x.Country)
            .HasMaxLength(200)
            .IsRequired();
    }
}
