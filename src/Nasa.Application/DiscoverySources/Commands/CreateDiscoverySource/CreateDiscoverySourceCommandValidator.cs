using FluentValidation;
using Nasa.Domain.Entities;

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
            .NotEmpty()
            .Must(BeValidType).WithMessage("Type is invalid.");

        RuleFor(c => c.StateOwner)
            .NotEmpty();
    }

    private static bool BeValidType(string type)
    {
        return DiscoverySourceType.List.Select(c => c.Name).Any(c => c == type);
    }
}