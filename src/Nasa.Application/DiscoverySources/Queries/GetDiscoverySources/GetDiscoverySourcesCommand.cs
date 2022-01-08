using MediatR;
using Nasa.Application.Common.Interfaces;
using Nasa.Domain.Entities;
using Nasa.Shared.Application;

namespace Nasa.Application.DiscoverySources.Queries.GetDiscoverySources;

public class GetDiscoverySourcesCommand : Command<GetDiscoverySourcesResponse> { }

public class
    GetDiscoverySourcesCommandHandler : IRequestHandler<GetDiscoverySourcesCommand,
        CommandResponse<GetDiscoverySourcesResponse>>
{
    private readonly IReadRepository<DiscoverySource> readRepository;

    public GetDiscoverySourcesCommandHandler(IReadRepository<DiscoverySource> readRepository)
    {
        this.readRepository = readRepository;
    }

    public async Task<CommandResponse<GetDiscoverySourcesResponse>> Handle(GetDiscoverySourcesCommand request,
        CancellationToken cancellationToken)
    {
        var discoverySources = await readRepository.ListAsync(cancellationToken);

        var response = new GetDiscoverySourcesResponse
        {
            DiscoverySources = discoverySources.Select(c => new DiscoverySourceResponse(c.Name, c.EstablishmentDate, c.Type.Name, c.StateOwner))
        };

        return CommandResponse<GetDiscoverySourcesResponse>.Success(response);
    }
}