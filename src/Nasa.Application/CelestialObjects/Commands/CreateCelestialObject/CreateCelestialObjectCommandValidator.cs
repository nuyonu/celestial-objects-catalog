﻿using FluentValidation;
using Nasa.Application.CelestialObjects.Specifications;
using Nasa.Application.Common.Interfaces;
using Nasa.Domain.Entities;

namespace Nasa.Application.CelestialObjects.Commands.CreateCelestialObject;

public class CreateCelestialObjectCommandValidator : AbstractValidator<CreateCelestialObjectCommand>
{
    private readonly IReadRepository<CelestialObject> readRepository;

    public CreateCelestialObjectCommandValidator(IReadRepository<CelestialObject> readRepository)
    {
        this.readRepository = readRepository;
        
        this.RuleFor(c => c.Name)
            .NotEmpty()
            .MinimumLength(1)
            .MustAsync(NotExistAnotherCelestialWithSameName).WithMessage($"{nameof(CelestialObject.Name)} should be unique.");
        
        this.RuleFor(c => c.Mass)
            .GreaterThan(0);
        
        this.RuleFor(c => c.EquatorialDiameter)
            .GreaterThan(0);
        
        this.RuleFor(c => c.DiscoverySourceId)
            .NotEmpty();
    }

    private async Task<bool> NotExistAnotherCelestialWithSameName(string name, CancellationToken cancellationToken)
    {
        var spec = new CelestialObjectsByNameSpec(name);
        var celestialObjects = await this.readRepository.ListAsync(spec, cancellationToken);

        return !celestialObjects.Any();
    }
}