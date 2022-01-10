using MediatR;
using Nasa.API.EndpointDefinitions.Common;
using Nasa.Application.DiscoverySources.Commands.CreateDiscoverySource;
using Nasa.Application.DiscoverySources.Queries.GetDiscoverySources;
using Nasa.Shared.Application;

namespace Nasa.API.EndpointDefinitions;

public class DiscoverySourcesEndpointDefinition : IEndpointDefinition
{
    private const string Name = "api/discoverySources";

    public void DefineEndpoints(WebApplication app)
    {
        app.MapPost(Name, CreateDiscoverySourceAsync)
            .Produces<Guid>();

        app.MapGet(Name, GetDiscoverySourcesAsync)
            .Produces<CommandResponse<GetDiscoverySourcesResponse>>();
    }

    public void DefineServices(IServiceCollection services)
    {
        // Register services related to current endpoints
    }

    private static async Task<IResult> GetDiscoverySourcesAsync(IMediator mediator)
    {
        return Results.Ok(await mediator.Send(new GetDiscoverySourcesCommand()));
    }

    private static async Task<IResult> CreateDiscoverySourceAsync(CreateDiscoverySourceCommand createDiscoverySourceCommand,
        IMediator mediator)
    {
        return Results.Created($"{Name}/id", await mediator.Send(createDiscoverySourceCommand));
    }
}