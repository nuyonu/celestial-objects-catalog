﻿using Nasa.Shared.Domain;

namespace Nasa.Domain.Entities;

public class CelestialObject : BaseEntity
{
    #pragma warning disable CS8618
    // TODO remove set and use another migration tool
    private CelestialObject()
    {
        // EF
    }

    public CelestialObject(string name, double mass, double equatorialDiameter, double surfaceTemperature,
        DateTime discoveryDate, Guid discoverySourceId)
    {
        Id = Guid.NewGuid();
        Name = name;
        Mass = mass;
        EquatorialDiameter = equatorialDiameter;
        SurfaceTemperature = surfaceTemperature;
        DiscoveryDate = discoveryDate;
        DiscoverySourceId = discoverySourceId;
        Type = GetObjectType();
    }

    public string Name { get; set; }

    public double Mass { get; set; }

    public double EquatorialDiameter { get; set; }

    public double SurfaceTemperature { get; set; }

    public DateTime DiscoveryDate { get; set; }

    public DiscoverySource DiscoverySource { get; set; }

    public Guid DiscoverySourceId { get; set; }

    public CelestialObjectType Type { get; set; }

    private CelestialObjectType GetObjectType()
    {
        var celestialObjectType = CelestialObjectType.List
            .Where(celestialObjectType => celestialObjectType.Condition.Invoke(this))
            .OrderBy(c => c.RulePrecedence)
            .FirstOrDefault();

        return celestialObjectType ?? CelestialObjectType.Unknown;
    }
}