namespace WildFarm.AnimalModels.MammalModels
{
    public class Dog : Mammal
    {
        public Dog(string name, double weight, string livingRegion) : base(name, weight, livingRegion)
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
                this.Weight += quantity * 0.4;
                FoodEaten += quantity;
            }
        }

        public override string ProduceSound()
        {
            return "Woof!";
        }
    }
}
