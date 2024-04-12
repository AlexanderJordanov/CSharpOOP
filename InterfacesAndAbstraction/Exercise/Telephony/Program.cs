namespace Telephony
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] phoneNumbers = Console.ReadLine().Split();
            string[] urls = Console.ReadLine().Split();
            
            foreach (string number in phoneNumbers)
            {
                if (number.All(char.IsDigit))
                {
                    ICaller phone;
                    if (number.Length == 7)
                    {
                        phone = new StationaryPhone();
                    }
                    else
                    {
                        phone = new Smartphone();
                    }

                    phone.Call(number);
                }
                else
                {
                    Console.WriteLine("Invalid number!");
                }
            }
            foreach (string url in urls)
            {
                if (url.Any(char.IsDigit)) 
                {
                    Console.WriteLine("Invalid URL!");
                }
                else
                {
                    IBrowser phone = new Smartphone();
                    phone.Browse(url);
                }              
            }
        }
    }
}