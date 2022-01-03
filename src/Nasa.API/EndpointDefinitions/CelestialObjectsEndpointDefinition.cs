using MediatR;
using Nasa.API.EndpointDefinitions.Common;
using Nasa.Application.CelestialObjects.Commands.CreateCelestialObject;
using Nasa.Application.CelestialObjects.Queries.GetCelestialObjects;
using Nasa.Shared;

namespace Nasa.API.EndpointDefinitions;

public class CelestialObjectsEndpointDefinition : IEndpointDefinition
{
    public void DefineEndpoints(WebApplication app)
    {
        app.MapPost("/celestialObjects", CreateCelestialObjectAsync)
            .Produces<CommandResponse<Guid>>();
        
        app.MapGet("/celestialObjects", GetCelestialObjectsAsync)
            .Produces<CommandResponse<GetCelestialObjectsResponse>>();
    }

    private static async Task<IResult> CreateCelestialObjectAsync(CreateCelestialObjectCommand createCelestialObjectCommand, IMediator mediator)
    {
        return Results.Ok(await mediator.Send(createCelestialObjectCommand));
    }
    
    private static async Task<IResult> GetCelestialObjectsAsync(IMediator mediator)
    {
        return Results.Ok(await mediator.Send(new GetCelestialObjectsCommand()));
    }

    public void DefineServices(IServiceCollection services)
    {
        // Register services related to current endpoints
    }
}