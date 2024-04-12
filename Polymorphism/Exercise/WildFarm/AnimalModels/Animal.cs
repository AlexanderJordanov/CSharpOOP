namespace WildFarm.AnimalModels
{
    public abstract class Animal
    {
        protected Animal(string name, double weight)
        {
            Name = name;
            Weight = weight;
            FoodEaten = 0;
        }

        public string Name { get; protected set; }
        public double Weight { get; protected set; }
        public int FoodEaten { get; protected set; }
        public abstract string ProduceSound();
        public abstract void Eat(string foodType, int quantity);
    }
}
