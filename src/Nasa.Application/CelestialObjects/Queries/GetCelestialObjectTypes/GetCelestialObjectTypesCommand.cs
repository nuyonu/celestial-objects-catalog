using MediatR;
using Nasa.Domain.Entities;
using Nasa.Shared.Application;

namespace Nasa.Application.CelestialObjects.Queries.GetCelestialObjectTypes;

public class GetCelestialObjectTypesCommand : Command<GetCelestialObjectTypesResponse> { }

public class GetDiscoverySourceTypesCommandHandler : IRequestHandler<GetCelestialObjectTypesCommand,
    CommandResponse<GetCelestialObjectTypesResponse>>
{
    public Task<CommandResponse<GetCelestialObjectTypesResponse>> Handle(GetCelestialObjectTypesCommand request,
        CancellationToken cancellationToken)
    {
        var response = new GetCelestialObjectTypesResponse(CelestialObjectType.List.Select(c => c.Name));

        return Task.FromResult(CommandResponse<GetCelestialObjectTypesResponse>.Success(response));
    }
}