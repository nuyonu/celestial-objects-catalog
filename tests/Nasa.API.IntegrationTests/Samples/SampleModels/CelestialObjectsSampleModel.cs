using System;

namespace Nasa.API.IntegrationTests.Samples.SampleModels;

public class CelestialObjectsSampleModel
{
    public string Name { get; set; }

    public double Mass { get; set; }

    public double EquatorialDiameter { get; set; }

    public double SurfaceTemperature { get; set; }

    public DateTime DiscoveryDate { get; set; }

    public string ExpectedType { get; set; }
}