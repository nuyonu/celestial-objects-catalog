using Ardalis.Specification;
using Nasa.Domain.Entities;

namespace Nasa.Application.CelestialObjects.Specifications;

public sealed class CelestialObjectsByStateOwnerSpec : Specification<CelestialObject>
{
    public CelestialObjectsByStateOwnerSpec(string? stateOwner)
    {
        Query.Where(c => c.DiscoverySource.StateOwner == (stateOwner ?? string.Empty));
    }
}