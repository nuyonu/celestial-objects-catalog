using MediatR;
using Nasa.API.EndpointDefinitions.Common;
using Nasa.Application.CelestialObjects.Commands.CreateCelestialObject;
using Nasa.Application.CelestialObjects.Queries.Common;
using Nasa.Application.CelestialObjects.Queries.GetCelestialObjectById;
using Nasa.Application.CelestialObjects.Queries.GetCelestialObjects;
using Nasa.Shared.Application;

namespace Nasa.API.EndpointDefinitions;

public class CelestialObjectsEndpointDefinition : IEndpointDefinition
{
    private const string Name = "api/celestialObjects";

    public void DefineEndpoints(WebApplication app)
    {
        app.MapPost(Name, CreateCelestialObjectAsync)
            .Produces<CommandResponse<Guid>>();

        app.MapGet(Name, GetCelestialObjectsAsync)
            .Produces<CommandResponse<GetCelestialObjectsResponse>>();

        app.MapGet($"{Name}/{{id}}", GetCelestialObjectByIdAsync)
            .Produces<CommandResponse<CelestialObjectResponse>>();
    }

    public void DefineServices(IServiceCollection services)
    {
        // Register services related to current endpoints
    }

    private static async Task<IResult> CreateCelestialObjectAsync(
        CreateCelestialObjectCommand command, IMediator mediator)
    {
        return Results.Created($"{Name}/id", await mediator.Send(command));
    }

    private static async Task<IResult> GetCelestialObjectsAsync(string? type, string? name, string? stateOwner,
        IMediator mediator)
    {
        return Results.Ok(await mediator.Send(new GetCelestialObjectsCommand(type, name, stateOwner)));
    }
    
    private static async Task<IResult> GetCelestialObjectByIdAsync(Guid id, IMediator mediator)
    {
        return Results.Ok(await mediator.Send(new GetCelestialObjectByIdCommand(id)));
    }
}