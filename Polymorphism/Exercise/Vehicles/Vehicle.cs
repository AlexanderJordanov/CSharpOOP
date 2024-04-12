namespace Vehicles
{
    public abstract class Vehicle
    {
        private double fuelQuantity;
        private double fuelConsumption;
        private double tankCapacity;

        protected Vehicle(double fuelQuantity, double fuelConsumption, double capacity)
        {
            FuelConsumption = fuelConsumption;
            FuelQuantity = fuelQuantity;
            TankCapacity = capacity;
        }

        public double FuelConsumption
        {
            get { return fuelConsumption; }
            protected set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Fuel Consumption must be a positive number!");
                }
                fuelConsumption = value;
            }
        }
        public double FuelQuantity
        {
            get { return fuelQuantity; }
            protected set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Fuel quantity must be a positive number!");
                }
                fuelQuantity = value;
            }
        }
        public double TankCapacity
        {
            get { return tankCapacity; }
            protected set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Tank capacity must be a positive number");
                }
                else if (value < fuelQuantity)
                {
                    fuelQuantity = 0;
                }
                tankCapacity = value;
            }
        }
        public virtual void Drive(double distance)
        {
            if (distance * FuelConsumption < FuelQuantity)
            {
                fuelQuantity -= distance * FuelConsumption;
                Console.WriteLine($"{this.GetType().Name} travelled {distance} km");
            }

            else
            {
                Console.WriteLine($"{this.GetType().Name} needs refueling");
            }
        }
        public virtual void Refuel(double liters)
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
                FuelQuantity += liters;
            }
        }
    }
}
