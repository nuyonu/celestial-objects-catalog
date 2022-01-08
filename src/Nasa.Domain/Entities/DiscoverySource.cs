using Nasa.Shared.Domain;

namespace Nasa.Domain.Entities;

public class DiscoverySource : BaseEntity
{
    #pragma warning disable CS8618
    // TODO remove set and use another migration tool
    private DiscoverySource()
    {
        // EF
        CelestialObjects = new List<CelestialObject>();
    }

    public DiscoverySource(string name, DateTime establishmentDate, DiscoverySourceType type, string stateOwner)
    {
        Name = name;
        EstablishmentDate = establishmentDate;
        Type = type;
        StateOwner = stateOwner;
        CelestialObjects = new List<CelestialObject>();
    }

    public string Name { get; set; }

    public DateTime EstablishmentDate { get; set; }

    public DiscoverySourceType Type { get; set; }

    public string StateOwner { get; set; }

    public List<CelestialObject> CelestialObjects { get; set; }
}