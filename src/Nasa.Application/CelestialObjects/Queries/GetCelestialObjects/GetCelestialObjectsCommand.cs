using Ardalis.Specification;
using MediatR;
using Nasa.Application.CelestialObjects.Queries.Common;
using Nasa.Application.CelestialObjects.Specifications;
using Nasa.Application.Common.Interfaces;
using Nasa.Domain.Entities;
using Nasa.Shared.Application;

namespace Nasa.Application.CelestialObjects.Queries.GetCelestialObjects;

public class GetCelestialObjectsCommand : Command<GetCelestialObjectsResponse>
{
    private string? name;
    private string? stateOwner;
    private string? type;

    private GetCelestialObjectsCommand()
    {
        Specifications = new List<Specification<CelestialObject>>();
    }

    public string? Type
    {
        get => type;
        private set
        {
            type = value;

            if (!string.IsNullOrEmpty(value)) Specifications.Add(new CelestialObjectsByTypeSpec(type));
        }
    }

    public string? Name
    {
        get => name;
        private set
        {
            name = value;

            if (!string.IsNullOrEmpty(value)) Specifications.Add(new CelestialObjectsByContainingNameSpec(name));
        }
    }

    public string? StateOwner
    {
        get => stateOwner;
        private set
        {
            stateOwner = value;

            if (!string.IsNullOrEmpty(value)) Specifications.Add(new CelestialObjectsByStateOwnerSpec(stateOwner));
        }
    }

    public List<Specification<CelestialObject>> Specifications { get; }

    public static GetCelestialObjectsCommand Create(string? type, string? name, string? stateOwner)
    {
        var command = new GetCelestialObjectsCommand
        {
            Type = type,
            Name = name,
            StateOwner = stateOwner
        };

        if (!command.Specifications.Any()) command.Specifications.Add(new CelestialObjectsAllSpec());

        return command;
    }
}

public class GetCelestialObjectsCommandHandler : IRequestHandler<GetCelestialObjectsCommand,
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
        var celestialObjects = await readRepository.ListAsync(request.Specifications, cancellationToken);

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