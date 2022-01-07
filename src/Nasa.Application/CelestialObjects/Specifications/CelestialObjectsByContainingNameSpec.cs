using Ardalis.Specification;
using Nasa.Domain.Entities;

namespace Nasa.Application.CelestialObjects.Specifications;

public sealed class CelestialObjectsByContainingNameSpec : Specification<CelestialObject>
{
    public CelestialObjectsByContainingNameSpec(string name)
    {
        Query.Where(c => c.Name.Contains(name));
    }
}