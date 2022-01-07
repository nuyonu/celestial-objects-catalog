using MediatR;
using Nasa.API.EndpointDefinitions.Common;
using Nasa.Application.CelestialObjects.Commands.CreateCelestialObject;
using Nasa.Application.CelestialObjects.Queries.GetCelestialObjects;
using Nasa.Application.CelestialObjects.Queries.GetCelestialObjectsByQuery;
using Nasa.Shared.Application;

namespace Nasa.API.EndpointDefinitions;

public class CelestialObjectsEndpointDefinition : IEndpointDefinition
{
    private const string Name = "celestialObjects";

    public void DefineEndpoints(WebApplication app)
    {
        app.MapPost(Name, CreateCelestialObjectAsync)
            .Produces<CommandResponse<Guid>>();

        app.MapGet(Name, GetCelestialObjectsAsync)
            .Produces<CommandResponse<GetCelestialObjectsResponse>>();
        
        app.MapGet("test", GetCelestialObjectsByQueryAsync)
            .Produces<CommandResponse<GetCelestialObjectsByQueryResponse>>();
    }

    public void DefineServices(IServiceCollection services)
    {
        // Register services related to current endpoints
    }

    private static async Task<IResult> CreateCelestialObjectAsync(
        CreateCelestialObjectCommand command, IMediator mediator)
    {
        return Results.Ok(await mediator.Send(command));
    }

    private static async Task<IResult> GetCelestialObjectsAsync(IMediator mediator)
    {
        return Results.Ok(await mediator.Send(new GetCelestialObjectsCommand()));
    }
    
    private static async Task<IResult> GetCelestialObjectsByQueryAsync(string? type, string? name, IMediator mediator)
    {
        var command = new GetCelestialObjectsByQueryCommand
        {
            Type = type,
            Name = name
        };
        
        return Results.Ok(await mediator.Send(command));
    }
}