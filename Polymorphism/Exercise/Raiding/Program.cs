namespace Raiding
{
    public class StartUp
    {
        public static void Main()
        {
            List<BaseHero> raidGroup = new List<BaseHero>();
            int heroCount = int.Parse(Console.ReadLine());
            for (int i = 0; i < heroCount; i++)
            {
                string name = Console.ReadLine();
                string heroType = Console.ReadLine();

                if (heroType == "Druid")
                {
                    Druid druid = new(name);
                    raidGroup.Add(druid);
                }
                else if (heroType == "Paladin")
                {
                    Paladin paladin = new(name);
                    raidGroup.Add(paladin);
                }
                else if (heroType == "Rogue")
                {
                    Rogue rogue = new(name);
                    raidGroup.Add(rogue);
                }
                else if (heroType == "Warrior")
                {
                    Warrior warrior = new(name);
                    raidGroup.Add(warrior);
                }
                else
                {
                    Console.WriteLine("Invalid hero!");
                    i--;
                }
            }
            int bossHealth = int.Parse(Console.ReadLine());
            foreach(var hero in raidGroup) 
            {
                Console.WriteLine(hero.CastAbility());
                bossHealth -= hero.Power;
            }
            if (bossHealth <= 0) 
            {
                Console.WriteLine("Victory!");
            }
            else
            {
                Console.WriteLine("Defeat...");
            }
        }
    }
}