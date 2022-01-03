using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nasa.Domain.Entities;

namespace Nasa.Infrastructure.Persistence.Configurations;

public class CelestialObjectConfiguration : IEntityTypeConfiguration<CelestialObject>
{
    public void Configure(EntityTypeBuilder<CelestialObject> builder)
    {
        builder.HasOne(co => co.DiscoverySource)
            .WithMany(ds => ds.CelestialObjects)
            .HasForeignKey(co => co.DiscoverySourceId);

        builder.Property(co => co.Type)
            .HasConversion(co => co.Value,
                co => CelestialObjectType.FromValue(co));
    }
}