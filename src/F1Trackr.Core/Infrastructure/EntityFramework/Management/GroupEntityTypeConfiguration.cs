using F1Trackr.Core.Domain.Management;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace F1Trackr.Core.Infrastructure.EntityFramework.Management;

internal sealed class GroupEntityTypeConfiguration : IEntityTypeConfiguration<Group>
{
    public void Configure(EntityTypeBuilder<Group> builder)
    {
        builder.ToTable("Groups");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasConversion(x => x.Value, value => new GroupId(value));

        builder.Property(x => x.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.Season)
            .HasMaxLength(4)
            .IsRequired();

        builder.HasIndex(x => new { x.Name, x.Season }).IsUnique();

        builder.HasMany(x => x.Members).WithOne().HasForeignKey(x => x.GroupId);
    }
}
