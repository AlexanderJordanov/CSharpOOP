using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FootballTeamGenerator
{
    public class Team
    {
        private string name;
        private readonly List<Player> players;

        public Team(string name)
        {
            Name = name;
            players = new List<Player>();
        }

        public int NumberOfPlayers => players.Count;
        public string Name
        {
            get => name;
            private set
            {
                if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("A name should not be empty.");
                }
                name = value;
            }
        }

        public IReadOnlyCollection<Player> Players
        {
            get
            {
                return players.AsReadOnly();
            }
        }
        public int Rating()
        {
            double rating = 0;
            foreach (var player in players)
            {
                rating += (player.Dribble + player.Endurance + player.Shooting + player.Passing + player.Sprint) / 5.0;
            }
            if (rating > 0)
            {
                rating /= NumberOfPlayers;
                return (int)Math.Round(rating);
            }
            else
            {
                return 0;
            }
        }
        public void AddPlayer(Player player)
        {
            players.Add(player);
        }
        public void RemovePlayer(string name)
        {
            Player foundPlayer = players.Find(p => p.Name == name);
            if (foundPlayer != null)
            {
                players.Remove(foundPlayer);
            }
            else
            {
                Console.WriteLine($"Player {name} is not in {this.Name} team.");
            }
        }
    }
}
