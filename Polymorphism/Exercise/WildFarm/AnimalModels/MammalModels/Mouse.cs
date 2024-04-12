namespace WildFarm.AnimalModels.MammalModels
{
    public class Mouse : Mammal
    {
        public Mouse(string name, double weight, string livingRegion) : base(name, weight, livingRegion)
        {
        }

        public override void Eat(string foodType, int quantity)
        {
            if (foodType != "Vegetable" && foodType != "Fruit")
            {
                Console.WriteLine($"{this.GetType().Name} does not eat {foodType}!");
            }
            else
            {
                this.Weight += quantity * 0.1;
                FoodEaten += quantity;
            }
        }

        public override string ProduceSound()
        {
            return "Squeak";
        }
    }
}
