using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Alba;
using FluentAssertions;
using Nasa.API.IntegrationTests.Common;
using Nasa.API.IntegrationTests.Samples.SampleModels;
using Nasa.Application.CelestialObjects.Commands.CreateCelestialObject;
using Nasa.Application.CelestialObjects.Queries.GetCelestialObjects;
using Nasa.Domain.Entities;
using Nasa.Infrastructure.Persistence;
using Xunit;

namespace Nasa.API.IntegrationTests;

public class CelestialObjectIntegrationTests : TestBase
{
    public string BaseEndpointName { get; } = "/api/celestialObjects";

    [Fact]
    public async Task Add_Endpoint_Should_Add_Celestial_Object_To_Database()
    {
        // Arrange
        var host = await CreateAlbaHostAsync(nameof(Add_Endpoint_Should_Add_Celestial_Object_To_Database));
        var databaseContext = GetRequiredService<DatabaseContext>(host);
        var discoverySource = new DiscoverySource("Hubble Space Telescope", DateTime.UtcNow,
            DiscoverySourceType.GroundTelescope, "USA");
        databaseContext.DiscoverySources.Add(discoverySource);
        await databaseContext.SaveChangesAsync();
        
        var result = await host.Scenario(_ =>
        {
            _.Post.Json(new CreateCelestialObjectCommand
                {
                    Name = "Kepler-37b",
                    Mass = 5.97237e24,
                    EquatorialDiameter = 12756200,
                    SurfaceTemperature = 5800,
                    DiscoveryDate = DateTime.Now,
                    DiscoverySourceId = discoverySource.Id
                })
                .ToUrl(BaseEndpointName);
            _.StatusCodeShouldBeOk();
            _.StatusCodeShouldBe(HttpStatusCode.Created);
            _.Header("location").ShouldHaveValues("api/celestialObjects/id");
        });

        // Assert
        var response = result.ReadAsJson<CommandResponsePublic<Guid>>();
        response?.Succeeded.Should().BeTrue();
        Guid.TryParse(response?.Result.ToString(), out _).Should().BeTrue();
    }

    [Fact]
    public async Task Get_Should_Return_All_If_No_Query_String_Is_Provided()
    {
        // Arrange
        var host = await CreateAlbaHostAsync(nameof(Get_Should_Return_All_If_No_Query_String_Is_Provided));
        var celestialObjectsSamples = await SeedDatabaseWithCelestialObjectsAsync(host);
        
        // Act
        var result = await host.Scenario(_ =>
        {
            _.Get.Url(BaseEndpointName);
            _.StatusCodeShouldBeOk();
        });

        // Assert
        var response = result.ReadAsJson<CommandResponsePublic<GetCelestialObjectsResponse>>();
        response?.Succeeded.Should().BeTrue();
        response?.Result.CelestialObjects.Should().HaveCount(celestialObjectsSamples.Count);
    }
    
    [Fact]
    public async Task Get_Should_Return_All_Filtered_Celestial_Objects_By_Query_Strings()
    {
        // Arrange
        var host = await CreateAlbaHostAsync(nameof(Get_Should_Return_All_Filtered_Celestial_Objects_By_Query_Strings));
        var celestialObjectsSamples = await SeedDatabaseWithCelestialObjectsAsync(host);

        #region CelestialObjectsWithTypePlanet

        // Act - Get all celestial objects with type Planet
        var result = await host.Scenario(_ =>
        {
            _.Get.Url(BaseEndpointName)
                .QueryString("type", CelestialObjectType.Planet.Name);
            _.StatusCodeShouldBeOk();
        });

        // Assert - Get all celestial objects with type Planet
        var response = result.ReadAsJson<CommandResponsePublic<GetCelestialObjectsResponse>>();
        response?.Succeeded.Should().BeTrue();
        response?.Result.CelestialObjects.Should().HaveCount(celestialObjectsSamples.Count(c => c.ExpectedType == CelestialObjectType.Planet.Name));

        #endregion

        #region CelestialObjectsThatContainsLetterEInName

        // Act - Get all celestial that contains letter e in name
        result = await host.Scenario(_ =>
        {
            _.Get.Url(BaseEndpointName)
                .QueryString("name", "e");
            _.StatusCodeShouldBeOk();
        });

        // Assert - Get all celestial that contains letter e in name
        response = result.ReadAsJson<CommandResponsePublic<GetCelestialObjectsResponse>>();
        response?.Succeeded.Should().BeTrue();
        response?.Result.CelestialObjects.Should().HaveCount(celestialObjectsSamples.Count(c => c.Name.Contains('e')));

        #endregion
        
        #region CelestialObjectsThatContainsLetterEInNameAndTypePlanet

        // Act - Get all celestial that contains letter e in name and type planet
        result = await host.Scenario(_ =>
        {
            _.Get.Url(BaseEndpointName)
                .QueryString("name", "e")
                .QueryString("type", CelestialObjectType.Planet.Name);
            _.StatusCodeShouldBeOk();
        });

        // Assert - Get all celestial that contains letter e in name and type planet
        response = result.ReadAsJson<CommandResponsePublic<GetCelestialObjectsResponse>>();
        response?.Succeeded.Should().BeTrue();
        response?.Result.CelestialObjects.Should().HaveCount(celestialObjectsSamples.Count(c => c.Name.Contains('e') && c.ExpectedType == CelestialObjectType.Planet.Name));

        #endregion

        #region CelestialObjectsWithRandomStateOwner

        // Act - Get all celestial with random state owner
        result = await host.Scenario(_ =>
        {
            _.Get.Url(BaseEndpointName)
                .QueryString("stateOwner", Guid.NewGuid().ToString());
            _.StatusCodeShouldBeOk();
        });

        // Assert - Get all celestial with random state owner
        response = result.ReadAsJson<CommandResponsePublic<GetCelestialObjectsResponse>>();
        response?.Succeeded.Should().BeTrue();
        response?.Result.CelestialObjects.Should().HaveCount(0);
        
        #endregion
    }
    
    [Fact]
    public async Task Get_By_Id_Should_Return_404_Not_Found_When_Entity_Not_Exists()
    {
        // Arrange
        var host = await CreateAlbaHostAsync(nameof(Get_By_Id_Should_Return_404_Not_Found_When_Entity_Not_Exists));
        var randomId = Guid.NewGuid();
    
        // Act && Assert
        var result = await host.Scenario(_ =>
        {
            _.Get.Url($"/api/celestialObjects/{randomId}");
            _.StatusCodeShouldBe(HttpStatusCode.NotFound);
        });
    
        // Assert
        var response = result.ReadAsJson<CommandResponsePublic<string>>();
        response?.Succeeded.Should().BeFalse();
        response?.Result.Should().BeNull();
        response?.Errors.Should().HaveCount(1);
    }

    private static async Task<List<CelestialObjectsSampleModel>> SeedDatabaseWithCelestialObjectsAsync(IAlbaHost host)
    {
        var databaseContext = GetRequiredService<DatabaseContext>(host);
        
        var discoverySource = new DiscoverySource("Hubble Space Telescope", DateTime.UtcNow,
            DiscoverySourceType.GroundTelescope, "USA");
        
        databaseContext.DiscoverySources.Add(discoverySource);
        
        await databaseContext.SaveChangesAsync();

        var celestialObjectsSamples =
            JsonHelper.LoadJson<List<CelestialObjectsSampleModel>>("Samples/CelestialObjectsSample.json");

        if (celestialObjectsSamples is null)
        {
            throw new InvalidOperationException("Samples missing");
        }

        var celestialObjects = celestialObjectsSamples.Select(c => new CelestialObject(c.Name, c.Mass,
            c.EquatorialDiameter, c.SurfaceTemperature, c.DiscoveryDate, discoverySource.Id));

        await databaseContext.AddRangeAsync(celestialObjects);

        await databaseContext.SaveChangesAsync();

        return celestialObjectsSamples;
    }
}