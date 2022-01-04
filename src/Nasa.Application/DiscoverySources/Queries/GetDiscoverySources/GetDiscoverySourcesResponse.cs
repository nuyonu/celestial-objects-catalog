namespace Nasa.Application.DiscoverySources.Queries.GetDiscoverySources;

public class GetDiscoverySourcesResponse
{
    public IEnumerable<DiscoverySourceResponse> DiscoverySources { get; set; }
}

public class DiscoverySourceResponse
{
    public string Name { get; set; }

    public DateTime EstablishmentDate { get; set; }

    public string Type { get; set; }

    public string StateOwner { get; set; }
}