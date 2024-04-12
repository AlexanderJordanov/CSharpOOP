namespace WildFarm.AnimalModels.BirdModels
{
    public class Hen : Bird
    {
        public Hen(string name, double weight, double wingSize) : base(name, weight, wingSize)
        {
        }

        public override void Eat(string foodType, int quantity)
        {
            this.Weight += quantity * 0.35;
            FoodEaten += quantity;
        }

        public override string ProduceSound()
        {
            return "Cluck";
        }
    }
}
