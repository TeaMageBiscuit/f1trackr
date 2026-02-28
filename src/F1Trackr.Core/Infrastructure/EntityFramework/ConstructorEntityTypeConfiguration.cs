using F1Trackr.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace F1Trackr.Core.Infrastructure.EntityFramework;

internal sealed class ConstructorEntityTypeConfiguration : IEntityTypeConfiguration<Constructor>
{
    public void Configure(EntityTypeBuilder<Constructor> builder)
    {
        builder.ToTable("Constructors");

        builder.HasKey(x => new
        {
            x.Id,
            x.Season,
        });

        builder.Property(x => x.Id)
            .HasConversion(x => x.Value, value => new ConstructorId(value));

        builder.Property(x => x.Season)
            .HasMaxLength(4)
            .IsRequired();

        builder.Property(x => x.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.Nationality)
            .HasMaxLength(100);

        builder.Property(x => x.LogoAddress)
            .HasMaxLength(200);
    }
}
