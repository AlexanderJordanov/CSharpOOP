namespace FootballTeamGenerator
{
    public class Program
    {
        static void Main(string[] args)
        {
            List<Team> teams = new List<Team>();
            string command;
            while ((command = Console.ReadLine()) != "END")
            {
                string[] tokens = command.Split(';');
                if (tokens[0] == "Team")
                {
                    try
                    {
                        Team team = new Team(tokens[1]);
                        if (!teams.Contains(team))
                        {
                            teams.Add(team);
                        }
                    }
                    catch (ArgumentException ae)
                    {
                        Console.WriteLine(ae.Message);
                    }
                }
                else if (tokens[0] == "Add")
                {
                    string teamName = tokens[1];
                    string playerName = tokens[2];
                    int endurance = int.Parse(tokens[3]);
                    int sprint = int.Parse(tokens[4]);
                    int dribble = int.Parse(tokens[5]);
                    int passing = int.Parse(tokens[6]);
                    int shooting = int.Parse(tokens[7]);
                    try
                    {
                        Player player = new Player(playerName, endurance, sprint, dribble, passing, shooting);
                        Team foundTeam = teams.Find(t => t.Name == teamName);
                        if (foundTeam != null)
                        {
                            foundTeam.AddPlayer(player);
                        }
                        else
                        {
                            Console.WriteLine($"Team {teamName} does not exist.");
                        }
                    }
                    catch (ArgumentException ae)
                    {
                        Console.WriteLine(ae.Message);
                    }
                }
                else if (tokens[0] == "Remove")
                {
                    string teamName = tokens[1];
                    string playerName = tokens[2];
                    Team foundTeam = teams.Find(t => t.Name == teamName);
                    if (foundTeam != null)
                    {
                        foundTeam.RemovePlayer(playerName);
                    }
                    else
                    {
                        Console.WriteLine($"Team {teamName} does not exist.");
                    }
                }
                else if (tokens[0] == "Rating")
                {
                    string teamName = tokens[1];
                    Team team = teams.Find(t => t.Name == teamName);
                    if (team != null)
                    {
                        Console.WriteLine($"{team.Name} - {team.Rating()}");
                    }
                    else
                    {
                        Console.WriteLine($"Team {teamName} does not exist.");
                    }
                }
            }
        }
    }
}