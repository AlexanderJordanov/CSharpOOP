namespace PizzaCalories
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            try
            {
                string[] pizzaData = Console.ReadLine()
                    .Split(' ')
                    .ToArray();
                Pizza pizza = new Pizza(pizzaData[1]);
                string[] doughData = Console.ReadLine()
                    .Split(' ')
                    .ToArray();
                Dough dough = new Dough(doughData[1], doughData[2], double.Parse(doughData[3]));
                pizza.Dough = dough;


                string command;
                while ((command = Console.ReadLine()) != "END") 
                {
                    string[] toppingData = command.Split(' ');
                    Topping topping = new Topping(toppingData[1], double.Parse(toppingData[2]));
                    pizza.AddTopping(topping);
                }
                Console.WriteLine($"{pizza.Name} - {pizza.TotalCalories:f2} Calories.");
            }
            catch (ArgumentException ae)
            {
                Console.WriteLine(ae.Message);
            }
        }
    }
}