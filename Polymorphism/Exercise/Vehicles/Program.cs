namespace Vehicles
{
    public class StartUp
    {
        public static void Main()
        {
            string[] carData = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);
            string[] truckData = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);
            string[] busData = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);
            Car car = new Car(double.Parse(carData[1]), double.Parse(carData[2]), double.Parse(carData[3]));
            Truck truck = new Truck(double.Parse(truckData[1]), double.Parse(truckData[2]), double.Parse(truckData[3]));
            Bus bus = new Bus(double.Parse(busData[1]), double.Parse(busData[2]), double.Parse(busData[3]));
            int lines = int.Parse(Console.ReadLine());
            for (int i = 0; i < lines; i++)
            {
                string[] commandData = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);
                if (commandData[0] == "Drive")
                {
                    if (commandData[1] == "Car")
                    {
                        car.Drive(double.Parse(commandData[2]));
                    }
                    else if (commandData[1] == "Truck")
                    {
                        truck.Drive(double.Parse(commandData[2]));
                    }
                    else if (commandData[1] == "Bus")
                    {
                        bus.Drive(double.Parse(commandData[2]));
                    }
                }
                else if (commandData[0] == "Refuel")
                {
                    if (commandData[1] == "Car")
                    {
                        car.Refuel(double.Parse(commandData[2]));
                    }
                    else if (commandData[1] == "Truck")
                    {
                        truck.Refuel(double.Parse(commandData[2]));
                    }
                    else if (commandData[1] == "Bus")
                    {
                        bus.Refuel(double.Parse(commandData[2]));
                    }
                }
                else if (commandData[0] == "DriveEmpty")
                {
                    bus.DriveEmpty(double.Parse(commandData[2]));
                }
            }
            Console.WriteLine($"{car.GetType().Name}: {car.FuelQuantity:f2}");
            Console.WriteLine($"{truck.GetType().Name}: {truck.FuelQuantity:f2}");
            Console.WriteLine($"{bus.GetType().Name}: {bus.FuelQuantity:f2}");
        }
    }
}