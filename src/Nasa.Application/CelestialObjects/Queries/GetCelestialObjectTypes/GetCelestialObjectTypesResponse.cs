namespace Nasa.Application.CelestialObjects.Queries.GetCelestialObjectTypes;

public class GetCelestialObjectTypesResponse
{
    public GetCelestialObjectTypesResponse(IEnumerable<string> types)
    {
        Types = types;
    }

    public IEnumerable<string> Types { get; }
}