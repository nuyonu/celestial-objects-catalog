using MediatR;
using Nasa.Application.Common.Interfaces;
using Nasa.Domain.Entities;
using Nasa.Shared.Application;

namespace Nasa.Application.DiscoverySources.Commands.CreateDiscoverySource;

public class CreateDiscoverySourceCommand : Command<Guid>
{
    public string Name { get; set; }

    public DateTime EstablishmentDate { get; set; }

    public int Type { get; set; }

    public string StateOwner { get; set; }
}

public class CreateDiscoverySourceCommandHandler : IRequestHandler<CreateDiscoverySourceCommand, CommandResponse<Guid>>
{
    private readonly IRepository<DiscoverySource> repository;

    public CreateDiscoverySourceCommandHandler(IRepository<DiscoverySource> repository)
    {
        this.repository = repository;
    }

    public async Task<CommandResponse<Guid>> Handle(CreateDiscoverySourceCommand request,
        CancellationToken cancellationToken)
    {
        var discoverySource =
            new DiscoverySource(request.Name, request.EstablishmentDate, DiscoverySourceType.FromValue(request.Type),
                request.StateOwner);

        await repository.CreateAsync(discoverySource);

        return CommandResponse<Guid>.Success(discoverySource.Id);
    }
}