using MediatR;
using Nasa.Application.Common.Interfaces;
using Nasa.Domain.Entities;
using Nasa.Shared.Application;

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
    private readonly IReadRepository<DiscoverySource> discoverySourceReadRepository;

    public CreateCelestialObjectCommandHandler(IRepository<CelestialObject> repository, IReadRepository<DiscoverySource> discoverySourceReadRepository)
    {
        this.repository = repository;
        this.discoverySourceReadRepository = discoverySourceReadRepository;
    }

    public async Task<CommandResponse<Guid>> Handle(CreateCelestialObjectCommand request,
        CancellationToken cancellationToken)
    {
        await discoverySourceReadRepository.GetByIdAsync(request.DiscoverySourceId, cancellationToken);
        
        var celestialObject = new CelestialObject(request.Name, request.Mass, request.EquatorialDiameter,
            request.SurfaceTemperature, request.DiscoveryDate, request.DiscoverySourceId);

        await repository.CreateAsync(celestialObject);

        return CommandResponse<Guid>.Success(celestialObject.Id);
    }
}