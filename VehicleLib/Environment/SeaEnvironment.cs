using System;
using VehicleLib.Enums;
using VehicleLib.Interfaces;

namespace VehicleLib.Environment;

public sealed class SeaEnvironment : MovementAbilities, IEnvironment
{
    public double Displacement { get; }
    
    public EnvironmentType Environment { get; } = EnvironmentType.Sea;
    public override SpeedUnit Unit { get; } = SpeedUnit.MpS;
    public override double MinSpeed { get; } = 1;
    public override double MaxSpeed { get; } = 40;

    public SeaEnvironment(double displacement) => Displacement = Math.Abs(displacement);
}