using System;
using VehicleLib.Enums;
using VehicleLib.Interfaces;
namespace VehicleLib.Environment;

public sealed class SkyEnvironment: MovementAbilities , IEnvironment
{
    public EnvironmentType Environment { get; } = EnvironmentType.Sky;
    public override SpeedUnit Unit { get; } = SpeedUnit.MpS;
    public override double MinSpeed { get; } = 20;
    public override double MaxSpeed { get; } = 200;
}


