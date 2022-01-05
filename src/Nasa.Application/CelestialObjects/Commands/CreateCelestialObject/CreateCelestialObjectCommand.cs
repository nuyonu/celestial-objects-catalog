using MediatR;
using Nasa.Application.Common.Interfaces;
using Nasa.Domain.Entities;
using Nasa.Shared;

namespace Nasa.Application.CelestialObjects.Commands.CreateCelestialObject;

public class CreateCelestialObjectCommand : Command<Guid>
{
    public string Name { get; set; }

    public double Mass { get; set; }

    public double EquatorialDiameter { get; set; }

    public double SurfaceTemperature { get; set; }

    public DateTime DiscoveryDate { get; set; }

    public Guid DiscoverySourceId { get; set; }
}

public class CreateCelestialObjectCommandHandler : IRequestHandler<CreateCelestialObjectCommand, CommandResponse<Guid>>
{
    private readonly IRepository<CelestialObject> repository;

    public CreateCelestialObjectCommandHandler(IRepository<CelestialObject> repository)
    {
        this.repository = repository;
    }
    
    public async Task<CommandResponse<Guid>> Handle(CreateCelestialObjectCommand request, CancellationToken cancellationToken)
    {
        var celestialObject = new CelestialObject(request.Name, request.Mass, request.EquatorialDiameter,
            request.SurfaceTemperature, request.DiscoveryDate, request.DiscoverySourceId);

        await this.repository.CreateAsync(celestialObject);
        
        return CommandResponse<Guid>.Success(celestialObject.Id);
    }
}