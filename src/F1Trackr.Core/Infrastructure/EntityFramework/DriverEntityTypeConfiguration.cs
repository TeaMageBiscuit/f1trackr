using F1Trackr.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace F1Trackr.Core.Infrastructure.EntityFramework;

internal sealed class DriverEntityTypeConfiguration : IEntityTypeConfiguration<Driver>
{
    public void Configure(EntityTypeBuilder<Driver> builder)
    {
        builder.ToTable("Drivers");

        builder.HasKey(x => new
        {
            x.Id,
            x.Season,
        });

        builder.Property(x => x.Id)
            .HasConversion(x => x.Value, value => new DriverId(value));

        builder.Property(x => x.Season)
            .HasMaxLength(4)
            .IsRequired();

        builder.Property(x => x.GivenName)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.FamilyName)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.Nationality)
            .HasMaxLength(100);

        builder.Property(x => x.Code)
            .HasMaxLength(5);

        builder.Property(x => x.DriverNumber)
            .HasMaxLength(5);

        builder.Property(x => x.ImageAddress)
            .HasMaxLength(200);
    }
}
