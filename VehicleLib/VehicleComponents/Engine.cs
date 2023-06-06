using VehicleLib.Enums;


namespace VehicleLib.Vehicle_Components
{
    public sealed class Engine
    {
        public enum State {On,Off}
        public State ActualState { get; private set; } = State.Off;
        public string Name { get; }
        public FuelType FuelType { get; }
        public int HorsePower { get; }

        public bool PowerOn()
        {
            if (ActualState != State.On)
            {
                ActualState = State.On;
                return true;
            }
            else return false;
        }
        public bool PowerOff()
        {
            if (ActualState != State.Off)
            {
                ActualState = State.Off;
                return true;
            }
            else return false;
        }
        public override string ToString()
        {
            return $"Name : {Name}\n" +
                   $"State : {ActualState.ToString()}\n" +
                   $"Fuel type : {FuelType} " +
                   $"Horse power : {HorsePower}";
        }
        public Engine(string name, FuelType fuelType,int horsePower)
        {
            Name = name;
            FuelType = fuelType;
            HorsePower = horsePower;
        }
    }
}