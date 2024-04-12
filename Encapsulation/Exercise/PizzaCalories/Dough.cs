namespace PizzaCalories
{
    public class Dough
    {
        private const double baseCaloriesPerGram = 2.0;
        private readonly string flourType;
        private readonly string bakingTechnique;
        private readonly double weight;

        public Dough(string flourType, string bakingTechnique, double weight)
        {
            FlourType = flourType;
            BakingTechnique = bakingTechnique;
            Weight = weight;
        }

        public double Weight
        {
            get => weight;
            init
            {
                if (value < 1 || value > 200)
                {
                    throw new ArgumentException("Dough weight should be in the range [1..200].");
                }
                weight = value;
            }
        }
        public string FlourType 
        {
            get => flourType;
            init
            {
                if (value.ToLower() != "white" && value.ToLower() != "wholegrain")
                {
                    throw new ArgumentException("Invalid type of dough.");
                }
                flourType = value;
            }
        }
        public string BakingTechnique
        {
            get => bakingTechnique;
            init
            {
                if ( value.ToLower() != "crispy" && value .ToLower() != "chewy" && value.ToLower() != "homemade")
                {
                    throw new ArgumentException("Invalid type of dough.");
                }
                bakingTechnique = value;
            }
        }
        public double CaloriesPerGram
        {
            get
            {
                double caloriesPerGram = baseCaloriesPerGram;

                if (flourType.ToLower() == "white")
                {
                    caloriesPerGram *= 1.5;
                }

                if (bakingTechnique.ToLower() == "crispy")
                {
                    caloriesPerGram *= 0.9;
                }
                else if (bakingTechnique.ToLower() == "chewy")
                {
                    caloriesPerGram *= 1.1;
                }

                return caloriesPerGram;
            }
        }
    }
}
