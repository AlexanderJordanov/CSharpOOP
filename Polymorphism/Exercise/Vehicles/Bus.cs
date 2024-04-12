using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vehicles
{
    public class Bus : Vehicle
    {
        private const double busCondition = 1.4;
        public Bus(double fuelQuantity, double fuelConsumption, double capacity) : base(fuelQuantity, fuelConsumption, capacity)
        {
        }
        public override void Drive(double distance)
        {
            if (FuelQuantity >= distance * (FuelConsumption + busCondition))
            {
                FuelQuantity -= distance * (FuelConsumption + busCondition);
                Console.WriteLine($"{this.GetType().Name} travelled {distance} km");
            }
            else
            {
                Console.WriteLine($"{this.GetType().Name} needs refueling");
            }
        }
        public void DriveEmpty(double distance)
        {
            if (FuelQuantity >= distance * FuelConsumption)
            {
                FuelQuantity -= distance * FuelConsumption;
                Console.WriteLine($"{this.GetType().Name} travelled {distance} km");
            }
            else
            {
                Console.WriteLine($"{this.GetType().Name} needs refueling");
            }
        }
    }
}
