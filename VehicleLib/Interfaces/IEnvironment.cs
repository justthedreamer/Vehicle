using VehicleLib.Enums;

namespace VehicleLib.Interfaces;

public interface IEnvironment
{
    public EnvironmentType Environment { get; }
}