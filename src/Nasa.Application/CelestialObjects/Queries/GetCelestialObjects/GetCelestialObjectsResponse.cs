using Nasa.Application.CelestialObjects.Queries.Common;

namespace Nasa.Application.CelestialObjects.Queries.GetCelestialObjects;

public class GetCelestialObjectsResponse
{
    public GetCelestialObjectsResponse()
    {
        CelestialObjects = new List<CelestialObjectResponse>();
    }

    public IEnumerable<CelestialObjectResponse> CelestialObjects { get; set; }
}