using VehicleLib.Enums;
using VehicleLib.Vehicle_Components;

namespace VehicleLib.Interfaces
{
    public interface IVehicle
    {
        public interface IVehicle
        {
            public string Name { get; }
            public double ActualSpeed { get; }
            
            public EnvironmentType ActualEnvironment { get; }
            public MovementState ActualMovementState { get; }
            
            public Engine Engine { get; }
            public EngineState EngineState { get ;  }

        
            public void GetMaxSpeed();
            public void GetMinSpeed();
            public void GetEnvironmentInfo();
            public void GetEngineInfo();

            public void PowerOn();
            public void PowerOff();
        }
    }
}