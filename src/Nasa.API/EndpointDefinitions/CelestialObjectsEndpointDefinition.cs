using MediatR;
using Nasa.API.EndpointDefinitions.Common;
using Nasa.Application.CelestialObjects.Commands.CreateCelestialObject;
using Nasa.Application.CelestialObjects.Queries.GetCelestialObjects;
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
    }

    public void DefineServices(IServiceCollection services)
    {
        // Register services related to current endpoints
    }

    private static async Task<IResult> CreateCelestialObjectAsync(
        CreateCelestialObjectCommand createCelestialObjectCommand, IMediator mediator)
    {
        return Results.Ok(await mediator.Send(createCelestialObjectCommand));
    }

    private static async Task<IResult> GetCelestialObjectsAsync(IMediator mediator)
    {
        return Results.Ok(await mediator.Send(new GetCelestialObjectsCommand()));
    }
}