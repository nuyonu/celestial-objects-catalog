using Ardalis.SmartEnum;
using Nasa.Domain.Common;

namespace Nasa.Domain.Entities;

public class CelestialObjectType : SmartEnum<CelestialObjectType>
{
    public static readonly CelestialObjectType Unknown =
        new("Unknown", 0, _ => false, default);

    public static readonly CelestialObjectType Planet =
        new("Planet", 1, celestialObject => celestialObject.Mass < Constants.JupiterMass, 1);

    public static readonly CelestialObjectType Star = new("Star", 2,
        celestialObject => celestialObject.Mass > Constants.JupiterMass && celestialObject.SurfaceTemperature >= 2500,
        1);

    public static readonly CelestialObjectType BlackHole = new("Black hole", 3,
        celestialObject => celestialObject.EquatorialDiameter / 2 < 2 * Constants.GravitationalConstant *
            celestialObject.Mass / Math.Pow(Constants.SpeedOfLight, 2), 0);

    private CelestialObjectType(string name, int value, Func<CelestialObject, bool> condition, int rulePrecedence) :
        base(name, value)
    {
        Condition = condition;
        RulePrecedence = rulePrecedence;
    }

    public Func<CelestialObject, bool> Condition { get; }

    public int RulePrecedence { get; }
}