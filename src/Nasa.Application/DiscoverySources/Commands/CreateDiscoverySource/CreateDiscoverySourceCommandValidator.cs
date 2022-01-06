using FluentValidation;

namespace Nasa.Application.DiscoverySources.Commands.CreateDiscoverySource;

public class CreateDiscoverySourceCommandValidator : AbstractValidator<CreateDiscoverySourceCommand>
{
    public CreateDiscoverySourceCommandValidator()
    {
        RuleFor(c => c.Name)
            .MinimumLength(1)
            .NotEmpty();

        RuleFor(c => c.EstablishmentDate)
            .NotEmpty();

        RuleFor(c => c.Type)
            .NotEmpty();

        RuleFor(c => c.StateOwner)
            .NotEmpty();
    }
}