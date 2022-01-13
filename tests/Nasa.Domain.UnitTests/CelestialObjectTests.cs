using System.Collections.Generic;
using FluentAssertions;
using Nasa.Domain.Entities;
using Nasa.Domain.UnitTests.Samples.SampleModels;
using Xunit;

namespace Nasa.Domain.UnitTests;

public class CelestialObjectTests
{
    [Fact]
    public void Type_Should_Be_Assigned_Correctly()
    {
        // Arrange
        var celestialTestData =
            JsonHelper.LoadJson<List<CelestialObjectTypesSampleModel>>("Samples/CelestialObjectTypesSample.json");

        foreach (var x in celestialTestData)
        {
            // Act
            var celestialObject = new CelestialObject(x.Name, x.Mass, x.EquatorialDiameter, x.SurfaceTemperature,
                x.DiscoveryDate, x.DiscoverySourceId);

            // Assert
            celestialObject.Type.Name.Should().Be(x.ExpectedType);
        }
    }
}