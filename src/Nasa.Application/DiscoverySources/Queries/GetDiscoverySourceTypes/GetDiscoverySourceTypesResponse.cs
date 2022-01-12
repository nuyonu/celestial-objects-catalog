namespace Nasa.Application.DiscoverySources.Queries.GetDiscoverySourceTypes;

public class GetDiscoverySourceTypesResponse
{
    public GetDiscoverySourceTypesResponse(IEnumerable<string> types)
    {
        Types = types;
    }

    public IEnumerable<string> Types { get; }
}