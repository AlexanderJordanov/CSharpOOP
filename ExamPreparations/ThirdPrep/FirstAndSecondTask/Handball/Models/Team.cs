using Handball.Models.Contracts;
using Handball.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handball.Models
{
    public class Team : ITeam
    {
        private string _name;
        private List<IPlayer> _players;

        public Team(string name)
        {
            Name = name;
            _players = new List<IPlayer>();
            PointsEarned = 0;
        }

        public string Name
        {
            get => _name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value) || string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException(ExceptionMessages.TeamNameNull);
                }
                _name = value;
            }
        }
        public int PointsEarned { get; private set; }

        public double OverallRating
        {
            get
            {
                if (Players.Count == 0)
                {
                    return 0;
                }
                else
                {
                    double totalRating = 0;
                    foreach (var player in Players)
                    {
                        totalRating += player.Rating;
                    }
                    double averageRating = totalRating / Players.Count;
                    return Math.Round(averageRating,2);
                    
                }
            }
        }

        public IReadOnlyCollection<IPlayer> Players => _players;

        public void Draw()
        {
            PointsEarned += 1;
            IPlayer player = Players.FirstOrDefault(p => p.GetType().Name == "Goalkeeper");
            player.IncreaseRating();
        }

        public void Lose()
        {
            foreach (var player in _players)
            {
                player.DecreaseRating();
            }
        }

        public void SignContract(IPlayer player)
        {
            _players.Add(player);
        }

        public void Win()
        {
            PointsEarned += 3;
            foreach (var player in _players)
            {
                player.IncreaseRating();
            }
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Team: {Name} Points: {PointsEarned}");
            sb.AppendLine($"--Overall rating: {OverallRating}");
            if ( _players.Count > 0 )
            {
                var names = Players.Select(p => p.Name);
                sb.AppendLine($"--Players: {string.Join(", ", names)}");
            }
            else
            {
                sb.AppendLine($"--Players: none");
            }
            return sb.ToString().Trim();
        }
    }
}
