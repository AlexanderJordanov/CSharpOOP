namespace Vehicles
{
    public class Truck : Vehicle
    {
        private const double refuelFactor = 0.95;
        private const double truckConditioner = 1.6;
        public Truck(double fuelQuantity, double fuelConsumption, double capacity) : base(fuelQuantity, fuelConsumption + truckConditioner, capacity)
        {
        }
        public override void Refuel(double liters)
        {
            if (FuelQuantity + liters > TankCapacity)
            {
                Console.WriteLine($"Cannot fit {liters} fuel in the tank");
            }
            else if (liters <= 0)
            {
                Console.WriteLine("Fuel must be a positive number");
            }
            else
            {
                FuelQuantity += liters * refuelFactor;
            }
        }
    }
}
