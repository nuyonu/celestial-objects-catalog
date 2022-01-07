using Ardalis.Specification;
using Nasa.Domain.Entities;

namespace Nasa.Application.CelestialObjects.Specifications;

public sealed class CelestialObjectsByTypeSpec : Specification<CelestialObject>
{
    public CelestialObjectsByTypeSpec(string type)
    {
        Query.Where(c => c.Type == CelestialObjectType.FromName(type, true));
    }
}