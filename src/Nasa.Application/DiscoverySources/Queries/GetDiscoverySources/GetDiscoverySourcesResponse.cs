using Nasa.Application.DiscoverySources.Queries.Common;

namespace Nasa.Application.DiscoverySources.Queries.GetDiscoverySources;

public class GetDiscoverySourcesResponse
{
    public IEnumerable<DiscoverySourceResponse> DiscoverySources { get; set; }
}