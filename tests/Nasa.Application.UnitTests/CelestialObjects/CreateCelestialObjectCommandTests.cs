using System;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Nasa.Application.CelestialObjects.Commands.CreateCelestialObject;
using Nasa.Application.Common.Interfaces;
using Nasa.Domain.Entities;
using NSubstitute;
using Xunit;

namespace Nasa.Application.UnitTests.CelestialObjects;

public class CreateCelestialObjectCommandTests
{
    private readonly CreateCelestialObjectCommandHandler handler;
    private readonly IRepository<CelestialObject> repository;

    public CreateCelestialObjectCommandTests()
    {
        repository = Substitute.For<IRepository<CelestialObject>>();
        handler = new CreateCelestialObjectCommandHandler(repository);
    }

    [Fact]
    public async Task Handler_Should_Create_Celestial_Object_And_Insert_To_DatabaseAsync()
    {
        // Arrange
        var discoverySourceId = Guid.NewGuid();
        var command = new CreateCelestialObjectCommand
        {
            Name = "V538 Carinae",
            Mass = 3.65e29,
            EquatorialDiameter = 184502000,
            DiscoveryDate = DateTime.Parse("2010-01-25"),
            SurfaceTemperature = 4800,
            DiscoverySourceId = discoverySourceId
        };

        // Act
        var response = await handler.Handle(command, CancellationToken.None);

        // Assert
        const double tolerance = 1;
        response.Succeeded.Should().BeTrue();
        response.Errors.Should().BeEmpty();
        await this.repository.Received(1).CreateAsync(Arg.Is<CelestialObject>(c =>
            c.Name == command.Name &&
            Math.Abs(c.Mass - command.Mass) < tolerance &&
            Math.Abs(c.EquatorialDiameter - command.EquatorialDiameter) < tolerance &&
            c.DiscoveryDate == command.DiscoveryDate &&
            Math.Abs(c.SurfaceTemperature - command.SurfaceTemperature) < tolerance &&
            c.DiscoverySourceId == command.DiscoverySourceId &&
            c.Type == CelestialObjectType.Star));
    }
}