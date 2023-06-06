using VehicleLib.Enums;

namespace VehicleLib.Environment;

public abstract class MovementAbilities
{
    public abstract SpeedUnit Unit { get; }
    public abstract double MinSpeed { get; }
    public abstract double MaxSpeed { get; }

    public override string ToString()
    {
        return $"Speed unit : {Unit.ToString()}\n" +
               $"Minimal speed : {MinSpeed}" +
               $"Maximal speed : {MaxSpeed}";
    }
}