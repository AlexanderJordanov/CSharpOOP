namespace ShoppingSpree
{
    public class Program
    {
        public static void Main(string[] args)
        {
            char[] delims = new char[] { '=', ';' };
            
            List<Person> people = new List<Person>();
            List<Product> products = new List<Product>();
            string[] peopleInfo = Console.ReadLine()
                .Split(delims, StringSplitOptions.RemoveEmptyEntries);
            string[] productsInfo = Console.ReadLine()
                .Split(delims, StringSplitOptions.RemoveEmptyEntries);
            try
            {
                for (int i = 0; i < peopleInfo.Length; i += 2)
                {
                    string personName = peopleInfo[i];
                    int personMoney = int.Parse(peopleInfo[i + 1]);
                    Person person = new Person(personName, personMoney);
                    people.Add(person);
                }
                for (int i = 0;i < productsInfo.Length; i += 2)
                {
                    string productName = productsInfo[i];
                    int productCost = int.Parse(productsInfo[i + 1]);
                    Product product = new Product(productName, productCost);
                    products.Add(product);
                }              
            }
            catch (ArgumentException ae)
            {
                Console.WriteLine(ae.Message);
                return;
            }
            string command;
            while ((command = Console.ReadLine()) != "END")
            {
                string[] commandInfo = command.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                string personName = commandInfo[0];
                string productName = commandInfo[1];

                Person currentPerson = people.Find(p => p.Name == personName);
                Product currentProduct = products.Find(p => p.Name == productName);
               try
                {
                    if (currentPerson != null && currentProduct != null)
                    {
                        currentPerson.AddProduct(currentProduct);
                        Console.WriteLine($"{currentPerson.Name} bought {currentProduct.Name}");
                    }
                }
                catch (ArgumentException ae)
                {
                    Console.WriteLine(ae.Message);
                }
            }

            foreach(Person person in people)
            {
                Console.Write($"{person.Name} - ");
                if (person.Bag.Count > 0)
                {
                    Console.WriteLine($"{string.Join(", ", person.Bag.Select(p => p.Name))}");
                }
                else
                {
                    Console.WriteLine("Nothing bought");
                }
            }
        }
    }
}