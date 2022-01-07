using Nasa.Application.CelestialObjects.Queries.Common;

namespace Nasa.Application.CelestialObjects.Queries.GetCelestialObjectsByQuery;

public class GetCelestialObjectsByQueryResponse
{
    public IEnumerable<CelestialObjectResponse> CelestialObjects { get; set; }
}