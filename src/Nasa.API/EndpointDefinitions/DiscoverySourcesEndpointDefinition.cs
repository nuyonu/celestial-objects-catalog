using MediatR;
using Nasa.API.EndpointDefinitions.Common;
using Nasa.Application.DiscoverySources.Commands.CreateDiscoverySource;

namespace Nasa.API.EndpointDefinitions;

public class DiscoverySourcesEndpointDefinition : IEndpointDefinition
{
    public void DefineEndpoints(WebApplication app)
    {
        app.MapPost("/discoverySources", CreateDiscoverySource)
            .Produces<Guid>();
    }

    private static async Task<IResult> CreateDiscoverySource(CreateDiscoverySourceCommand createDiscoverySourceCommand, IMediator mediator)
    {
        return Results.Ok(await mediator.Send(createDiscoverySourceCommand));
    }

    public void DefineServices(IServiceCollection services)
    {
        // Register services related to current endpoints
    }
}