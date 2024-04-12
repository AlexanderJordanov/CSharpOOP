namespace Vehicles
{
    public class Car : Vehicle
    {
        private const double carCondintioner = 0.9;
        public Car(double fuelQuantity, double fuelConsumption, double capacity) : base(fuelQuantity, fuelConsumption + carCondintioner, capacity)
        {
        }
    }
}
