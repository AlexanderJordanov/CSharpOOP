using System.Buffers;
using System.Xml.Linq;
using WildFarm.AnimalModels;
using WildFarm.AnimalModels.BirdModels;
using WildFarm.AnimalModels.MammalModels;
using WildFarm.AnimalModels.MammalModels.FelineModels;

namespace WildFarm
{
    public class Program
    {
        public static void Main()
        {
            List<Animal> animals = new List<Animal>();
            string command;
            while ((command = Console.ReadLine()) != "End")
            {
                string[] animalData = command.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                
                string[] foodData = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);
                if (animalData[0] == "Cat")
                {
                    Cat cat = new Cat(animalData[1], double.Parse(animalData[2]), animalData[3], animalData[4]);
                    animals.Add(cat);
                    Console.WriteLine(cat.ProduceSound());
                    cat.Eat(foodData[0], int.Parse(foodData[1]));
                }
                else if (animalData[0] == "Tiger")
                {
                    Tiger tiger = new Tiger(animalData[1], double.Parse(animalData[2]), animalData[3], animalData[4]);
                    animals.Add(tiger);
                    Console.WriteLine(tiger.ProduceSound());                 
                    tiger.Eat(foodData[0], int.Parse(foodData[1]));
                }
                else if (animalData[0] == "Dog")
                {
                    Dog dog = new Dog(animalData[1], double.Parse(animalData[2]), animalData[3]);
                    animals.Add(dog);
                    Console.WriteLine(dog.ProduceSound());
                    dog.Eat(foodData[0], int.Parse(foodData[1]));
                }
                else if (animalData[0] == "Mouse")
                {
                    Mouse mouse = new Mouse(animalData[1], double.Parse(animalData[2]), animalData[3]);
                    animals.Add(mouse);
                    Console.WriteLine(mouse.ProduceSound());
                    mouse.Eat(foodData[0], int.Parse(foodData[1]));
                }
                else if (animalData[0] == "Hen")
                {
                    Hen hen = new Hen(animalData[1], double.Parse(animalData[2]), double.Parse(animalData[3]));
                    animals.Add(hen);
                    Console.WriteLine(hen.ProduceSound());
                    hen.Eat(foodData[0], int.Parse(foodData[1]));
                }
                else if (animalData[0] == "Owl")
                {
                    Owl owl = new Owl(animalData[1], double.Parse(animalData[2]), double.Parse(animalData[3]));
                    animals.Add(owl);
                    Console.WriteLine(owl.ProduceSound());
                    owl.Eat(foodData[0], int.Parse(foodData[1]));
                }               
            }
            foreach(var animal in animals)
            {
                Console.WriteLine(animal);
            }
        }
    }
}