using MediatR;
using Nasa.Application.CelestialObjects.Queries.Common;
using Nasa.Application.Common.Interfaces;
using Nasa.Domain.Entities;
using Nasa.Shared.Application;

namespace Nasa.Application.CelestialObjects.Queries.GetCelestialObjectById;

public class GetCelestialObjectByIdCommand : Command<CelestialObjectResponse>
{
    public GetCelestialObjectByIdCommand(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; }
}

public class GetCelestialObjectByIdCommandHandler : IRequestHandler<GetCelestialObjectByIdCommand,
        CommandResponse<CelestialObjectResponse>>
{
    private readonly IReadRepository<CelestialObject> readRepository;

    public GetCelestialObjectByIdCommandHandler(IReadRepository<CelestialObject> readRepository)
    {
        this.readRepository = readRepository;
    }
    
    public async Task<CommandResponse<CelestialObjectResponse>> Handle(GetCelestialObjectByIdCommand request, CancellationToken cancellationToken)
    {
        var celestialObject = await readRepository.GetByIdAsync(request.Id, cancellationToken);
        
        return CommandResponse<CelestialObjectResponse>.Success(new CelestialObjectResponse
        {
            Name = celestialObject.Name,
            Mass = celestialObject.Mass,
            Type = celestialObject.Type.Name,
            DiscoveryDate = celestialObject.DiscoveryDate,
            EquatorialDiameter = celestialObject.EquatorialDiameter,
            SurfaceTemperature = celestialObject.SurfaceTemperature,
            DiscoverySourceId = celestialObject.DiscoverySourceId
        });
    }
}