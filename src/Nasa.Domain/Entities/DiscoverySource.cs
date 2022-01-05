using Nasa.Shared;

namespace Nasa.Domain.Entities;

public class DiscoverySource : BaseEntity
{
    private DiscoverySource()
    {
        // EF
    }
    
    public DiscoverySource(string name, DateTime establishmentDate, DiscoverySourceType type, string stateOwner)
    {
        Name = name;
        EstablishmentDate = establishmentDate;
        Type = type;
        StateOwner = stateOwner;
    }

    public string Name { get; set; }

    public DateTime EstablishmentDate { get; set; }

    public DiscoverySourceType Type { get; set; }

    public string StateOwner { get; set; }
    
    public List<CelestialObject> CelestialObjects { get; set; }
}