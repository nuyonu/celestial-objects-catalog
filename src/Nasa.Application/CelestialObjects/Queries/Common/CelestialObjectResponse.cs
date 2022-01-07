namespace Nasa.Application.CelestialObjects.Queries.Common;

public class CelestialObjectResponse
{
    public string Name { get; set; }

    public double Mass { get; set; }

    public double EquatorialDiameter { get; set; }

    public double SurfaceTemperature { get; set; }

    public DateTime DiscoveryDate { get; set; }

    public Guid DiscoverySourceId { get; set; }

    public string Type { get; set; }
}