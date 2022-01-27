using MediatR;
using Nasa.Domain.Entities;
using Nasa.Shared.Application;

namespace Nasa.Application.DiscoverySources.Queries.GetDiscoverySourceTypes;

public class GetDiscoverySourceTypesCommand : Command<GetDiscoverySourceTypesResponse> { }

public class GetDiscoverySourceTypesCommandHandler : IRequestHandler<GetDiscoverySourceTypesCommand,
    CommandResponse<GetDiscoverySourceTypesResponse>>
{
    public Task<CommandResponse<GetDiscoverySourceTypesResponse>> Handle(GetDiscoverySourceTypesCommand request,
        CancellationToken cancellationToken)
    {
        var response = new GetDiscoverySourceTypesResponse(DiscoverySourceType.List.Select(c => c.Name));

        return Task.FromResult(CommandResponse<GetDiscoverySourceTypesResponse>.Success(response));
    }
}