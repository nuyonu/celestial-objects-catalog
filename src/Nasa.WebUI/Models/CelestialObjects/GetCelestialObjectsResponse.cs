namespace Nasa.WebUI.Models.CelestialObjects;

public class GetCelestialObjectsResponse
{
    public IEnumerable<CelestialObjectResponse> CelestialObjects { get; set; }
}