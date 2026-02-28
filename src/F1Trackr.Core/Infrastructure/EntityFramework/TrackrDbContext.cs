using F1Trackr.Core.Domain;
using F1Trackr.Core.Domain.Management;
using Microsoft.EntityFrameworkCore;

namespace F1Trackr.Core.Infrastructure.EntityFramework;

public sealed class TrackrDbContext : DbContext
{
    public TrackrDbContext(DbContextOptions<TrackrDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; }

    public DbSet<Group> Groups { get; set; }

    public DbSet<Constructor> Constructors { get; set; }

    public DbSet<Driver> Drivers { get; set; }

    public DbSet<Race> Races { get; set; }

    public DbSet<RaceResult> RaceResults { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TrackrDbContext).Assembly);
    }
}
