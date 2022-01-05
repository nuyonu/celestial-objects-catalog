using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Nasa.Domain.Entities;

namespace Nasa.Infrastructure.Persistence;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions options) : base(options)
    { }

    public DbSet<CelestialObject> CelestialObjects { get; set; }

    public DbSet<DiscoverySource> DiscoverySources { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }
}