using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nasa.Domain.Entities;

namespace Nasa.Infrastructure.Persistence.Configurations;

public class DiscoverySourceConfiguration : IEntityTypeConfiguration<DiscoverySource>
{
    public void Configure(EntityTypeBuilder<DiscoverySource> builder)
    {
        builder.Property(ds => ds.Type)
            .HasConversion(ds => ds.Value,
                ds => DiscoverySourceType.FromValue(ds));
    }
}