using System;
using System.Net;
using System.Threading.Tasks;
using Alba;
using FluentAssertions;
using Nasa.API.IntegrationTests.Common;
using Nasa.Application.DiscoverySources.Commands.CreateDiscoverySource;
using Nasa.Domain.Entities;
using Xunit;

namespace Nasa.API.IntegrationTests;

public class DiscoverySourceIntegrationTests : TestBase
{
    public string BaseEndpointName { get; } = "/api/discoverySources";
    
    [Fact]
    public async Task Add_Endpoint_Should_Add_Celestial_Object_To_Database()
    {
        // Arrange
        var host = await CreateAlbaHostAsync(nameof(Add_Endpoint_Should_Add_Celestial_Object_To_Database));

        var result = await host.Scenario(_ =>
        {
            _.Post.Json(new CreateDiscoverySourceCommand
                {
                    Name = "Hubble Space Telescope",
                    Type = DiscoverySourceType.GroundTelescope.Name,
                    EstablishmentDate = DateTime.Now,
                    StateOwner = "USA"
                })
                .ToUrl(BaseEndpointName);
            _.StatusCodeShouldBeOk();
            _.StatusCodeShouldBe(HttpStatusCode.Created);
            _.Header("location").ShouldHaveValues("api/discoverySources/id");
        });

        // Assert
        var response = result.ReadAsJson<CommandResponsePublic<Guid>>();
        response?.Succeeded.Should().BeTrue();
        Guid.TryParse(response?.Result.ToString(), out _).Should().BeTrue();
    }
}