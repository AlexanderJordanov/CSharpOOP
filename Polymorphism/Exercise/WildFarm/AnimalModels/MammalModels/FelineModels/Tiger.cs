namespace WildFarm.AnimalModels.MammalModels.FelineModels
{
    public class Tiger : Feline
    {
        public Tiger(string name, double weight, string livingRegion, string breed) : base(name, weight, livingRegion, breed)
        {
        }

        public override void Eat(string foodType, int quantity)
        {
            if (foodType != "Meat")
            {
                Console.WriteLine($"{this.GetType().Name} does not eat {foodType}!");
            }
            else
            {
                this.Weight += quantity;
                FoodEaten += quantity;
            }
        }

        public override string ProduceSound()
        {
            return "ROAR!!!";
        }
    }
}
