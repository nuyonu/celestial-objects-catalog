using Ardalis.SmartEnum;

namespace Nasa.Domain.Entities;

public class DiscoverySourceType : SmartEnum<DiscoverySourceType>
{
    public static readonly DiscoverySourceType SpaceTelescope = new("Space telescope", 1);
    public static readonly DiscoverySourceType GroundTelescope = new("Ground telescope", 2);
    public static readonly DiscoverySourceType Other = new("Other", 3);

    public DiscoverySourceType(string name, int value) : base(name, value) { }
}