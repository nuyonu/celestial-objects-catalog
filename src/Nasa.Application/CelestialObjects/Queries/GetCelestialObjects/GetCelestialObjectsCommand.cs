using MediatR;
using Nasa.Application.Common.Interfaces;
using Nasa.Domain.Entities;
using Nasa.Shared.Application;

namespace Nasa.Application.CelestialObjects.Queries.GetCelestialObjects;

public class GetCelestialObjectsCommand : Command<GetCelestialObjectsResponse> { }

public class
    GetCelestialObjectsCommandHandler : IRequestHandler<GetCelestialObjectsCommand,
        CommandResponse<GetCelestialObjectsResponse>>
{
    private readonly IReadRepository<CelestialObject> readRepository;

    public GetCelestialObjectsCommandHandler(IReadRepository<CelestialObject> readRepository)
    {
        this.readRepository = readRepository;
    }

    public async Task<CommandResponse<GetCelestialObjectsResponse>> Handle(GetCelestialObjectsCommand request,
        CancellationToken cancellationToken)
    {
        var celestialObjects = await readRepository.ListAsync(cancellationToken);

        var response = new GetCelestialObjectsResponse
        {
            CelestialObjects = celestialObjects.Select(c => new CelestialObjectResponse
            {
                Name = c.Name,
                Mass = c.Mass,
                Type = c.Type.Name,
                DiscoveryDate = c.DiscoveryDate,
                EquatorialDiameter = c.EquatorialDiameter,
                SurfaceTemperature = c.SurfaceTemperature,
                DiscoverySourceId = c.DiscoverySourceId
            })
        };

        return Command<GetCelestialObjectsResponse>.Success(response);
    }
}