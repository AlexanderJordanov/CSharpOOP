namespace Telephony
{
    public class Smartphone : ICaller, IBrowser
    {
        public void Browse(string url)
        {
            Console.WriteLine($"Browsing: {url}!");
        }

        public void Call(string phoneNumber)
        {
            Console.WriteLine($"Calling... {phoneNumber}");
        }
    }
}
