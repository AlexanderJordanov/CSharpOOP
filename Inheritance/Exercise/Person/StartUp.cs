using System;

namespace Person
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            string name = Console.ReadLine();
            int years = int.Parse(Console.ReadLine());
            if (years > 15)
            {
                Person person = new Person(name, years);
                Console.WriteLine(person);
            }
            else
            {
                Child child = new Child(name, years);
                Console.WriteLine(child);
            }
        }
    }
}