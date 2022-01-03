using FluentValidation;

namespace Nasa.Application.CelestialObjects.Commands.CreateCelestialObject;

public class CreateCelestialObjectCommandValidator : AbstractValidator<CreateCelestialObjectCommand>
{
    public CreateCelestialObjectCommandValidator()
    {
        // TODO add more validators maybe??
        this.RuleFor(c => c.Name)
            .NotEmpty();
        
        this.RuleFor(c => c.Mass)
            .NotEmpty();
    }
}