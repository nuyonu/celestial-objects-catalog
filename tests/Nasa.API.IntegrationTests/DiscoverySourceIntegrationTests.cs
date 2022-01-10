using System;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using FluentAssertions;
using Nasa.API.IntegrationTests.Common;
using Nasa.Application.DiscoverySources.Commands.CreateDiscoverySource;
using Nasa.Application.DiscoverySources.Queries.GetDiscoverySources;
using Nasa.Domain.Entities;
using Xunit;

namespace Nasa.API.IntegrationTests;

public class DiscoverySourceIntegrationTests : IClassFixture<NasaAppFixture>
{
    private readonly NasaAppFixture fixture;

    public DiscoverySourceIntegrationTests(NasaAppFixture fixture)
    {
        this.fixture = fixture;
    }
    
    [Fact]
    public async Task Can_Manage_Discovery_Sources_With_Api()
    {
        // Arrange - Before adding
        var client = fixture.CreateClient();

        // Act - Before adding
        var response = await client.GetFromJsonAsync<CommandResponsePublic<GetDiscoverySourcesResponse>>("/api/discoverySources");

        // Assert - Before adding
        response?.Succeeded.Should().BeTrue();
        response?.Errors.Should().HaveCount(0);
        response?.Result.DiscoverySources.Should().HaveCount(0);
        
        // Arrange - Adding one discovery source
        var newDiscoverySource = new CreateDiscoverySourceCommand
        {
            Name = "Hubble Space Telescope",
            Type = 1,
            EstablishmentDate = DateTime.Now,
            StateOwner = "USA"
        };

        // Act - Adding one discovery source
        var createdResponse = await client.PostAsJsonAsync("/api/discoverySources", newDiscoverySource);

        // Assert - Adding one discovery source
        createdResponse.StatusCode.Should().Be(HttpStatusCode.Created);
        createdResponse.Headers.Location.Should().NotBeNull();
        var createdContentResponse = await createdResponse.Content.ReadFromJsonAsync<CommandResponsePublic<Guid>>();
        createdContentResponse?.Succeeded.Should().BeTrue();
        Guid.TryParse(createdContentResponse?.Result.ToString(), out _).Should().BeTrue();
        
        // Arrange - After adding
        
        // Act - After adding
        var responseAfterAddingDiscoverySource = await client.GetFromJsonAsync<CommandResponsePublic<GetDiscoverySourcesResponse>>("/api/discoverySources");
        
        // Assert - After adding
        responseAfterAddingDiscoverySource?.Succeeded.Should().BeTrue();
        responseAfterAddingDiscoverySource?.Errors.Should().HaveCount(0);
        responseAfterAddingDiscoverySource?.Result.DiscoverySources.Should().Contain(x =>
            x.Name == newDiscoverySource.Name &&
            x.Type == DiscoverySourceType.FromValue(newDiscoverySource.Type).Name &&
            x.EstablishmentDate == newDiscoverySource.EstablishmentDate &&
            x.StateOwner == newDiscoverySource.StateOwner);
    }
}