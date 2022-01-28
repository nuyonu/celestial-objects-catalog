using Nasa.Application.CelestialObjects.Queries.Common;

namespace Nasa.Application.CelestialObjects.Queries.GetCelestialObjects;

public class GetCelestialObjectsResponse
{
    public GetCelestialObjectsResponse(IEnumerable<CelestialObjectResponse> celestialObjects)
    {
        CelestialObjects = celestialObjects;
    }

    public IEnumerable<CelestialObjectResponse> CelestialObjects { get; }
}