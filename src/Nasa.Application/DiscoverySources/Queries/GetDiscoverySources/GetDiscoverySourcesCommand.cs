using MediatR;
using Nasa.Application.CelestialObjects.Queries.GetCelestialObjects;
using Nasa.Application.Common.Interfaces;
using Nasa.Domain.Entities;
using Nasa.Shared;

namespace Nasa.Application.DiscoverySources.Queries.GetDiscoverySources;

public class GetDiscoverySourcesCommand : Command<GetDiscoverySourcesResponse>
{ }

public class GetDiscoverySourcesCommandHandler : IRequestHandler<GetDiscoverySourcesCommand, CommandResponse<GetDiscoverySourcesResponse>>
{
    private readonly IReadRepository<DiscoverySource> readRepository;

    public GetDiscoverySourcesCommandHandler(IReadRepository<DiscoverySource> readRepository)
    {
        this.readRepository = readRepository;
    }
    
    public async Task<CommandResponse<GetDiscoverySourcesResponse>> Handle(GetDiscoverySourcesCommand request, CancellationToken cancellationToken)
    {
        var discoverySources = await this.readRepository.ListAsync(cancellationToken);

        var response = new GetDiscoverySourcesResponse
        {
            DiscoverySources = discoverySources.Select(c => new DiscoverySourceResponse
            {
                Name = c.Name,
                Type = c.Type.Name,
                EstablishmentDate = c.EstablishmentDate,
                StateOwner = c.StateOwner
            })
        };
        
        return CommandResponse<GetDiscoverySourcesResponse>.Success(response);
    }
}