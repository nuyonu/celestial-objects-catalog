namespace Nasa.Application.DiscoverySources.Queries.Common;

public class DiscoverySourceResponse
{
    public DiscoverySourceResponse(string name, DateTime establishmentDate, string type, string stateOwner)
    {
        Name = name;
        EstablishmentDate = establishmentDate;
        Type = type;
        StateOwner = stateOwner;
    }

    public string Name { get; }

    public DateTime EstablishmentDate { get; }

    public string Type { get; }

    public string StateOwner { get; }
}