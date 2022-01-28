using MediatR;
using Nasa.Application.Common.Interfaces;
using Nasa.Application.DiscoverySources.Queries.Common;
using Nasa.Domain.Entities;
using Nasa.Shared.Application;

namespace Nasa.Application.DiscoverySources.Queries.GetDiscoverySourceById;

public class GetDiscoverySourceByIdCommand : Command<DiscoverySourceResponse>
{
    public GetDiscoverySourceByIdCommand(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; }
}

public class GetDiscoverySourceByIdCommandHandler : IRequestHandler<GetDiscoverySourceByIdCommand,
    CommandResponse<DiscoverySourceResponse>>
{
    private readonly IReadRepository<DiscoverySource> readRepository;

    public GetDiscoverySourceByIdCommandHandler(IReadRepository<DiscoverySource> readRepository)
    {
        this.readRepository = readRepository;
    }

    public async Task<CommandResponse<DiscoverySourceResponse>> Handle(GetDiscoverySourceByIdCommand request,
        CancellationToken cancellationToken)
    {
        var discoverySource = await readRepository.GetByIdAsync(request.Id, cancellationToken);

        var response = new DiscoverySourceResponse(discoverySource.Name, discoverySource.EstablishmentDate,
            discoverySource.Type.Name, discoverySource.StateOwner);

        return CommandResponse<DiscoverySourceResponse>.Success(response);
    }
}