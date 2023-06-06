using System;
using VehicleLib.Interfaces;
using VehicleLib.Enums;
using VehicleLib.Environment;
using VehicleLib.Vehicle_Components;
using VehicleLib.Map;

using System.Collections.Generic;

namespace VehicleLib.Vehicle;

public class Vehicle : IVehicle
{
        private SkyEnvironment? Sky;
        private LandEnvironment? Land;
        private SeaEnvironment? Sea;

        private bool TrippleEnvironmentVehicle
        {
            get
            {
                if (Sky is not null && Land is not null && Sea is not null) return true;
                else return false;
            }
        }
        private bool MultiEnvironmentVehicle
        {
            get
            {
                if (Sky is not null && Land is not null) return true;
                if (Sky is not null && Sea is not null) return true;
                if (Land is not null && Sea is not null) return true;
                else return false;
            }
        }
        
        /*VEHICLE INFO*/
        public string Name { get; private set; } 
        public double ActualSpeed { get; private set; } = 0;
        /**/
        
        /*ENVIRONMENT*/
        public EnvironmentType ActualEnvironment { get; private set; }
        public List<EnvironmentType> EnvironmentsList { get; private set; }

        public MovementState ActualMovementState
        {
            get
            {
                if (ActualSpeed > 0) return MovementState.Move;
                else return MovementState.Stay;
            }
        }
        /**/
        
        /*ENGINE*/
        public Engine Engine { get; private set; }
        public Engine.State EngineState => Engine.ActualState;
        /**/

        /*PUBLIC METHODS*/
        public void StopVehicle()
        {
            if (ActualEnvironment == EnvironmentType.Sky) return;
            SpeedDecrease(ActualSpeed);
            PowerOff();
        }
        public void StartVehicle()
        {
            PowerOn();
        }
        public void GetMaxSpeed()
        {
            Console.WriteLine($"Actual Environmne : {EnvironmentMap.EnvironmentToString(ActualEnvironment)}");
            Console.WriteLine($"{EnvironmentMap.MaxSpeed(ActualEnvironment).ToString()} {EnvironmentMap.SpeedUnitString(ActualEnvironment)}");
        }
        public void GetMinSpeed()
        {
            Console.WriteLine($"{EnvironmentMap.MinSpeed(ActualEnvironment).ToString()} {EnvironmentMap.SpeedUnitString(ActualEnvironment)}");
        }
        public void GetEnvironmentInfo()
        {
            Console.WriteLine($"Actual Environment : {EnvironmentMap.EnvironmentToString(ActualEnvironment)}");
            Console.WriteLine($"Unit : {EnvironmentMap.SpeedUnitString(ActualEnvironment)} ");
            Console.WriteLine($"Minimal speed :{EnvironmentMap.MinSpeed(ActualEnvironment).ToString()} {EnvironmentMap.SpeedUnitString(ActualEnvironment)}");
            Console.WriteLine($"Maximal speed :{EnvironmentMap.MaxSpeed(ActualEnvironment).ToString()} {EnvironmentMap.SpeedUnitString(ActualEnvironment)}");

        }
        public void GetEngineInfo()
        {
            Console.WriteLine(Engine.ToString());
        }
        public void PowerOn()
        {
            bool result = Engine.PowerOn();
            if(result) Console.WriteLine("Engine Power On");
            else Console.WriteLine("Invalid action. Engine is on.");
        }
        public void PowerOff()
        {
            bool result = Engine.PowerOff();
            if(result) Console.WriteLine("Engine Power Off");
            else Console.WriteLine("Invalid action. Engine is off.");
        }
        public void SpeedIncrease(double value)
        {
            
            if (ActualSpeed + value > EnvironmentMap.MaxSpeed(ActualEnvironment))
            {
                Console.WriteLine("Too high value."); 
                return;
            }

            if (Engine.ActualState == Engine.State.Off)
            {
                Console.WriteLine("Engine is off.");
                return;
            }

            double finalValue = ActualSpeed + value;
            while (ActualSpeed < finalValue)
            {
                ActualSpeed += 1;
                if(ActualSpeed % 5 == 0) Console.WriteLine(ActualSpeed);
                if(MultiEnvironmentVehicle) EnvironmentControl();

            }

            Console.WriteLine($"Speed increased to : {ActualSpeed.ToString()}");
            
        }
        public void SpeedDecrease(double value)
        {
            
            if (Engine.ActualState == Engine.State.Off)
            {
                Console.WriteLine("Engine is off.");
                return;
            }

            if (ActualSpeed - value < 0)
            {
                Console.WriteLine("Too low value.");
                return;
            }

            double finalValue = ActualSpeed - value;
            while (ActualSpeed > finalValue)
            {
                ActualSpeed -= 1;
                if(ActualSpeed % 5 == 0) Console.WriteLine(ActualSpeed);
                if(MultiEnvironmentVehicle) EnvironmentControl();
            }

            Console.WriteLine($"Speed decreased to : {ActualSpeed.ToString()}");
            if(ActualSpeed == 0) Console.WriteLine("VehicleLib stoped.");
        }
        
        public override string ToString()
        {
            string environments = "Environments : ";
            foreach (var item in EnvironmentsList)
            {
                environments += EnvironmentMap.EnvironmentToString(item)+ " ";
            }
            
            return $"VehicleLib name {Name}\n" +
                   $"Current speed : {ActualSpeed.ToString()}\n" +
                   $"{environments}\n" +
                   $"Actual Environment : {EnvironmentMap.EnvironmentToString(ActualEnvironment)}\n" +
                   $"Unit : {EnvironmentMap.SpeedUnitString(ActualEnvironment)}\n" +
                   $"Minimal speed :{EnvironmentMap.MinSpeed(ActualEnvironment).ToString()} {EnvironmentMap.SpeedUnitString(ActualEnvironment)}\n" +
                   $"Maximal speed :{EnvironmentMap.MaxSpeed(ActualEnvironment).ToString()} {EnvironmentMap.SpeedUnitString(ActualEnvironment)}";
        }
        
        /**/
        /*PRIVATE METHODS*/
        private SpeedUnit ActualSpeedUnit()
        {
            return EnvironmentMap.SpeedUnit(ActualEnvironment);
        }
        private void EnvironmentControl()
        {
            if (TrippleEnvironmentVehicle || MultiEnvironmentVehicle)
            {
                /*Actual speed in KpH*/
                double actualSpeed = UnitConvert(ActualSpeed, ActualSpeedUnit(), SpeedUnit.KpH);
                
                /*Speed Increse*/
                if (actualSpeed >= 72 && ActualEnvironment != EnvironmentType.Sky)
                {
                    ActualEnvironment = EnvironmentType.Sky;
                    Console.WriteLine("Lift off. Environment change to Sky.");
                }
                
                /*Speed Degresse*/
                if (actualSpeed < 72 && ActualEnvironment == EnvironmentType.Sky)
                {
                    Console.WriteLine("Your actual speed is critical low!");
                    Console.WriteLine("Where you want to land ? ");
                    Console.WriteLine("A.Ground\nB.Sea");
                    bool condition = true;
                    while (condition)
                    {
                        var input = Console.ReadKey();
                        switch (input.Key)
                        {
                            case ConsoleKey.A:
                            {
                                ActualEnvironment = EnvironmentType.Land;
                                condition = false;
                                break;
                            }
                            case ConsoleKey.B:
                            {
                                ActualEnvironment = EnvironmentType.Sea;
                                condition = false;
                                break;
                            }
                            default:
                            {
                                Console.WriteLine("Type A or B!");
                                break;
                            }
                        }
                    }
                    
                }
            }
        }
        /**/

        /*STATIC METHODS*/
        public static double UnitConvert(double input,SpeedUnit unit, SpeedUnit outUnit)
        {
            switch ((unit,outUnit))
            {
               case (SpeedUnit.KpH,SpeedUnit.Knot) : return input * 0.54 ;
               case (SpeedUnit.KpH,SpeedUnit.MpS) : return input * 0.27;
               
               case (SpeedUnit.MpS,SpeedUnit.Knot) : return input * 1.94;
               case (SpeedUnit.MpS,SpeedUnit.KpH) : return input * 3.6;
               
               case (SpeedUnit.Knot,SpeedUnit.KpH) : return input * 1.852;
               case (SpeedUnit.Knot,SpeedUnit.MpS) : return input * 0.514;
               
            }
            return input;
        }
        /**/
        
        
        /*CONSTRUCTOR*/
        private Vehicle()
        {}
        /**/
        
        /*FACTORY*/
        public static Vehicle CreateLandVehicle(string name,int tires,string engineName,FuelType fuelType, int horsePower) => new()
            {
            Name = name,
            Engine = new Engine(engineName,fuelType,horsePower),
            Land = new LandEnvironment(tires),
            EnvironmentsList = new List<EnvironmentType>() { EnvironmentType.Land },
            
            };
        public static Vehicle CreateLandSeaVehicle(string vehicleName, int tires, double displacement, int horsePower, string engineName = "Oli Engine") => new()
            {
                Name = vehicleName,
                Engine = new Engine(engineName,FuelType.Oil,horsePower),
                Land = new LandEnvironment(tires),
                EnvironmentsList = new List<EnvironmentType>() { EnvironmentType.Land ,EnvironmentType.Sea,EnvironmentType.Sky}
            };
        public static Vehicle CreateLandSkyVehicle(string name, int tires,string engineName , FuelType fuelType, int horsePower) => new()
            {
            Name = name,
            Engine = new Engine(engineName,fuelType,horsePower),
            Land = new LandEnvironment(tires),
            Sky = new SkyEnvironment(),
            EnvironmentsList = new List<EnvironmentType>() { EnvironmentType.Land, EnvironmentType.Sky },
            };
        public static Vehicle CreateSeaVehicle(string vehicleName, double displacement, int horsePower, string engineName = "Oil Engine") => new()
            {
            Name = vehicleName,
            Engine = new Engine(engineName,FuelType.Oil,horsePower),
            Sea = new SeaEnvironment(displacement),
            EnvironmentsList = new List<EnvironmentType>() { EnvironmentType.Sea }
            };
        public static Vehicle CreateSeaSkyVehicle(string name, int tires, double displacement, int horsePower) => new()
            {
                Name = name,
                Engine = new Engine("Oil Engine",FuelType.Oil,horsePower),
                Sky = new SkyEnvironment(),
                Sea = new SeaEnvironment(displacement),
                EnvironmentsList = new List<EnvironmentType>() { EnvironmentType.Sea ,EnvironmentType.Sky}
            };
        public static Vehicle CreateLandSeaSkyVehicle(string name, int tires, double displacement,string engineName ,FuelType fuelType,int horsePower) => new()
            {
                Name = name,
                Engine = new Engine("Oil Engine",FuelType.Oil,horsePower),
                Sky = new SkyEnvironment(),
                Sea = new SeaEnvironment(displacement),
                Land = new LandEnvironment(tires),
                EnvironmentsList = new List<EnvironmentType>() { EnvironmentType.Sea ,EnvironmentType.Sky, EnvironmentType.Land}
            };
        
        
}