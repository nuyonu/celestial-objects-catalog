using MediatR;
using Nasa.API.EndpointDefinitions.Common;
using Nasa.Application.DiscoverySources.Commands.CreateDiscoverySource;
using Nasa.Application.DiscoverySources.Queries.GetDiscoverySources;
using Nasa.Shared;

namespace Nasa.API.EndpointDefinitions;

public class DiscoverySourcesEndpointDefinition : IEndpointDefinition
{
    public void DefineEndpoints(WebApplication app)
    {
        app.MapPost("/discoverySources", CreateDiscoverySource)
            .Produces<Guid>();

        app.MapGet("/discoverySources", GetDiscoverySources)
            .Produces<CommandResponse<GetDiscoverySourcesResponse>>();
    }

    private static async Task<IResult> GetDiscoverySources(IMediator mediator)
    {
        return Results.Ok(await mediator.Send(new GetDiscoverySourcesCommand()));
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