using System;
using VehicleLib.Enums;
using VehicleLib.Interfaces;

namespace VehicleLib.Environment;

public class LandEnvironment : MovementAbilities, IEnvironment
{
    public int Tires { get; }

    public EnvironmentType Environment { get; } = EnvironmentType.Land;
    public override SpeedUnit Unit { get; } = SpeedUnit.KpH;
    public override double MinSpeed { get; } = 1;
    public override double MaxSpeed { get; } = 350;

    public LandEnvironment(int tires) => Tires = Math.Abs(tires);


}