using System;

namespace Animals
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            string input;
            while ((input = Console.ReadLine()) != "Beast!") 
            {
                string[] animalTokens = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);
                string animalName = animalTokens[0];
                int animalYear = int.Parse(animalTokens[1]);
                try
                {

                    if (input == "Dog")
                    {
                        Dog doggy = new Dog(animalName, animalYear, animalTokens[2]);
                        Console.WriteLine(doggy.GetType().Name);
                        Console.WriteLine(doggy);
                        Console.WriteLine(doggy.ProduceSound());
                    }
                    else if (input == "Cat")
                    {
                        Cat cat = new Cat(animalName, animalYear, animalTokens[2]);
                        Console.WriteLine(cat.GetType().Name);
                        Console.WriteLine(cat);
                        Console.WriteLine(cat.ProduceSound());
                    }
                    else if (input == "Frog")
                    {
                        Frog peepo = new Frog(animalName, animalYear, animalTokens[2]);
                        Console.WriteLine(peepo.GetType().Name);
                        Console.WriteLine(peepo);
                        Console.WriteLine(peepo.ProduceSound());
                    }
                    else if (input == "Kitten")
                    {
                        Kitten kitty = new Kitten(animalName, animalYear);
                        Console.WriteLine(kitty.GetType().Name);
                        Console.WriteLine(kitty);
                        Console.WriteLine(kitty.ProduceSound());
                    }
                    else if (input == "Tomcat")
                    {
                        Tomcat tom = new Tomcat(animalName, animalYear);
                        Console.WriteLine(tom.GetType().Name);
                        Console.WriteLine(tom);
                        Console.WriteLine(tom.ProduceSound());
                    }
                }
                catch(ArgumentException ae)
                {
                    Console.WriteLine(ae.Message);
                }
            }
        }
    }
}
