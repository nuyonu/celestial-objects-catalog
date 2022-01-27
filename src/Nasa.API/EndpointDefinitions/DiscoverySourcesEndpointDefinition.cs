using MediatR;
using Nasa.API.Common;
using Nasa.API.EndpointDefinitions.Common;
using Nasa.Application.DiscoverySources.Commands.CreateDiscoverySource;
using Nasa.Application.DiscoverySources.Queries.Common;
using Nasa.Application.DiscoverySources.Queries.GetDiscoverySourceById;
using Nasa.Application.DiscoverySources.Queries.GetDiscoverySources;
using Nasa.Application.DiscoverySources.Queries.GetDiscoverySourceTypes;
using Nasa.Shared.Application;

namespace Nasa.API.EndpointDefinitions;

public class DiscoverySourcesEndpointDefinition : IEndpointDefinition
{
    private const string Name = "api/discoverySources";

    public void DefineEndpoints(WebApplication app)
    {
        app.MapPost(Name, CreateDiscoverySourceAsync)
            .Produces<CommandResponse<Guid>>()
            .Produces<CommandResponse<string>>(StatusCodes.Status400BadRequest);

        app.MapGet(Name, GetDiscoverySourcesAsync)
            .Produces<CommandResponse<GetDiscoverySourcesResponse>>();

        app.MapGet($"{Name}/types", GetDiscoverySourceTypesAsync)
            .Produces<CommandResponse<GetDiscoverySourceTypesResponse>>();

        app.MapGet($"{Name}/{{id}}", GetDiscoverySourceByIdAsync)
            .Produces<CommandResponse<DiscoverySourceResponse>>();
    }

    public void DefineServices(IServiceCollection services)
    {
        // Register services related to current endpoints
    }

    private static async Task<IResult> GetDiscoverySourcesAsync(IMediator mediator)
    {
        return Results.Ok(await mediator.Send(new GetDiscoverySourcesCommand()));
    }

    private static async Task<IResult> GetDiscoverySourceTypesAsync(IMediator mediator)
    {
        return Results.Ok(await mediator.Send(new GetDiscoverySourceTypesCommand()));
    }

    private static async Task<IResult> CreateDiscoverySourceAsync(
        JsonDeserializeWrapper<CreateDiscoverySourceCommand> createDiscoverySourceCommand,
        IMediator mediator)
    {
        return Results.Created($"{Name}/id", await mediator.Send(createDiscoverySourceCommand.Value!));
    }

    private static async Task<IResult> GetDiscoverySourceByIdAsync(Guid id, IMediator mediator)
    {
        return Results.Ok(await mediator.Send(new GetDiscoverySourceByIdCommand(id)));
    }
}