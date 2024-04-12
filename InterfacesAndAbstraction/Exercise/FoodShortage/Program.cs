namespace FoodShortage
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            int lines = int.Parse(Console.ReadLine());
            List<IBuyer> list = new List<IBuyer>();
            for (int i = 0; i < lines; i++)
            {
                string[] buyerTokens = Console.ReadLine().Split();
                if (buyerTokens.Length == 3)
                {
                    Rebel rebel = new Rebel(buyerTokens[0], int.Parse(buyerTokens[1]), buyerTokens[2]);
                    list.Add(rebel);
                }
                else
                {
                    Citizen citizen = new Citizen(buyerTokens[0], int.Parse(buyerTokens[1]), buyerTokens[2], buyerTokens[3]);
                    list.Add(citizen);
                }
            }
            string input;
            while ((input = Console.ReadLine()) != "End")
            {
                foreach(IBuyer buyer in list)
                {
                    if (buyer.Name == input)
                    {
                        buyer.BuyFood();
                    }
                }
            }

            Console.WriteLine(list.Sum(b => b.Food));
        }
    }
}