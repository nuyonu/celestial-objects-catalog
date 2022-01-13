using System;

namespace Nasa.Domain.UnitTests.Samples.SampleModels;

public class CelestialObjectTypesSampleModel
{
    public string Name { get; set; }

    public double Mass { get; set; }

    public double EquatorialDiameter { get; set; }

    public double SurfaceTemperature { get; set; }

    public DateTime DiscoveryDate { get; set; }

    public Guid DiscoverySourceId { get; set; }

    public string ExpectedType { get; set; }
}