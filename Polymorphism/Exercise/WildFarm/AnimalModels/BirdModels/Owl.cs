using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WildFarm.AnimalModels.BirdModels
{
    public class Owl : Bird
    {
        public Owl(string name, double weight, double wingSize) : base(name, weight, wingSize)
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
                this.Weight += quantity * 0.25;
                FoodEaten += quantity;
            }
        }

        public override string ProduceSound()
        {
            return "Hoot Hoot";
        }
    }
}
