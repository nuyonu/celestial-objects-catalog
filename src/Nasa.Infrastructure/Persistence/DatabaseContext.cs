using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Nasa.Domain.Entities;
using Nasa.Shared.Domain;

namespace Nasa.Infrastructure.Persistence;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions options) : base(options) { }

    public DbSet<CelestialObject> CelestialObjects { get; set; }

    public DbSet<DiscoverySource> DiscoverySources { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    {
        foreach (var entry in ChangeTracker.Entries<IAuditedEntity>())
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedOn = DateTime.Now;
                    break;
            }

        return base.SaveChangesAsync(cancellationToken);
    }
}