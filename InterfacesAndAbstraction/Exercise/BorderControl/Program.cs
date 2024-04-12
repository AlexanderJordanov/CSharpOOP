namespace BorderControl
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            List<IBirthdable> list = new List<IBirthdable>();
            string input;
            while ((input = Console.ReadLine()) != "End") 
            {
                string[] entityTokens = input.Split();
                if (entityTokens[0] == "Citizen")
                {
                    Citizen citizen = new Citizen(entityTokens[1], int.Parse(entityTokens[2]), entityTokens[3], entityTokens[4]);
                    list.Add(citizen);
                }
                else if (entityTokens[0] == "Pet")
                {
                    Pet pet = new Pet(entityTokens[1], entityTokens[2]);
                    list.Add(pet);
                }
            }
            string year = Console.ReadLine();
            foreach (var entity in list)
            {
                if (entity.Birthdate.EndsWith(year))
                {
                    Console.WriteLine(entity.Birthdate);
                }
            }
        }
    }
}