namespace PizzaCalories
{
    public class Topping
    {
        private const double baseCaloriesPerGram = 2.0;
        private readonly string toppingType;
        private readonly double weight;

        public Topping(string toppingType, double weight)
        {
            ToppingType = toppingType;
            Weight = weight;
        }

        public double Weight
        {
            get => weight;
            init
            {
                if (value < 1 || value > 50)
                {
                    throw new ArgumentException($"{ToppingType} weight should be in the range [1..50].");
                }
                weight = value;
            }
        }
        public string ToppingType
        {
            get => toppingType;
            init
            {
                if (value.ToLower() != "meat" &&  value.ToLower() != "veggies" && value.ToLower() != "cheese" && value.ToLower() != "sauce")
                {
                    throw new ArgumentException($"Cannot place {value} on top of your pizza.");
                }
                toppingType = value;
            }
        }
        public double CalculateCaloriesPerGram()
        {
                double caloriesPerGram = baseCaloriesPerGram;
                if (toppingType.ToLower() == "meat")
                {
                    caloriesPerGram *= 1.2;
                }
                if (toppingType.ToLower() == "veggies")
                {
                    caloriesPerGram *= 0.8;
                }
                if (toppingType.ToLower() == "cheese")
                {
                    caloriesPerGram *= 1.1;
                }
                if (toppingType.ToLower() == "sauce")
                {
                    caloriesPerGram *= 0.9;
                }
                return caloriesPerGram;
        }
    }
}
