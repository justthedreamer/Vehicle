using System.Collections.Generic;
using VehicleLib.Enums;
using VehicleLib.Environment;

namespace VehicleLib.Map;

public static class EnvironmentMap
{
    private static Dictionary<EnvironmentType, MovementAbilities> environmentAblitilies = new Dictionary<EnvironmentType, MovementAbilities>()
    {
        { EnvironmentType.Land, new LandEnvironment(0) },
        { EnvironmentType.Sea, new SeaEnvironment(0) },
        { EnvironmentType.Sky, new SkyEnvironment() }
    };
    private static Dictionary<EnvironmentType, string> environmentToString = new Dictionary<EnvironmentType, string>()
    {
        { EnvironmentType.Land, "Land" },
        { EnvironmentType.Sea, "Sea" },
        { EnvironmentType.Sky, "Sky" }
    };
    private static Dictionary<EnvironmentType, string> environmentUnit = new Dictionary<EnvironmentType, string>()
    {
        { EnvironmentType.Land, "KpH" },
        { EnvironmentType.Sea, "Knot" },
        { EnvironmentType.Sky, "MpS" }
    };
    
    public static double MinSpeed(EnvironmentType environment) => environmentAblitilies[environment].MinSpeed;
    public static double MaxSpeed(EnvironmentType environment) => environmentAblitilies[environment].MaxSpeed;
    public static SpeedUnit SpeedUnit(EnvironmentType environment) => environmentAblitilies[environment].Unit;
    
    public static string SpeedUnitString(EnvironmentType environment) => environmentUnit[environment];
    
    public static string EnvironmentToString(EnvironmentType environment) => environmentToString[environment];


}