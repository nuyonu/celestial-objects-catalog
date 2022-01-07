using Ardalis.Specification;
using MediatR;
using Nasa.Application.CelestialObjects.Queries.Common;
using Nasa.Application.CelestialObjects.Specifications;
using Nasa.Application.Common.Interfaces;
using Nasa.Domain.Entities;
using Nasa.Shared.Application;

namespace Nasa.Application.CelestialObjects.Queries.GetCelestialObjectsByQuery;

public class GetCelestialObjectsByQueryCommand : Command<GetCelestialObjectsByQueryResponse>
{
    public string? Type { get; set; }
    
    public string? Name { get; set; }
}

public class GetCelestialObjectsByQueryCommandHandler : IRequestHandler<GetCelestialObjectsByQueryCommand, CommandResponse<GetCelestialObjectsByQueryResponse>>
{
    private readonly IReadRepository<CelestialObject> readRepository;

    public GetCelestialObjectsByQueryCommandHandler(IReadRepository<CelestialObject> readRepository)
    {
        this.readRepository = readRepository;
    }
    
    public async Task<CommandResponse<GetCelestialObjectsByQueryResponse>> Handle(GetCelestialObjectsByQueryCommand request, CancellationToken cancellationToken)
    {
        // TODO refactor this and add more specifications
        Specification<CelestialObject> spec = new CelestialObjectsAllSpec();
        
        if (request.Type is not null)
        {
            spec = new CelestialObjectsByTypeSpec(request.Type);
        }
        else if (request.Name is not null)
        {
            spec = new CelestialObjectsByContainingNameSpec(request.Name);
        }

        var celestialObjects = await readRepository.ListAsync(spec, cancellationToken);
        
        var response = new GetCelestialObjectsByQueryResponse
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

        return Command<GetCelestialObjectsByQueryResponse>.Success(response);
    }
}