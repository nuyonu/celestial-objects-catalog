using Ardalis.Specification;
using Nasa.Domain.Entities;

namespace Nasa.Application.CelestialObjects.Specifications;

public sealed class CelestialObjectsByNameSpec : Specification<CelestialObject>
{
    public CelestialObjectsByNameSpec(string name)
    {
        Query.Where(c => c.Name == name);
    }
}