namespace WildFarm.AnimalModels.MammalModels.FelineModels
{
    public class Cat : Feline
    {
        public Cat(string name, double weight, string livingRegion, string breed) : base(name, weight, livingRegion, breed)
        {
        }

        public override void Eat(string foodType, int quantity)
        {
            if (foodType != "Meat" &&  foodType != "Vegetable")
            {
                Console.WriteLine($"{this.GetType().Name} does not eat {foodType}!");
            }
            else
            {
                this.Weight += quantity * 0.3;
                FoodEaten += quantity;
            }
        }

        public override string ProduceSound()
        {
            return "Meow";
        }
    }
}
