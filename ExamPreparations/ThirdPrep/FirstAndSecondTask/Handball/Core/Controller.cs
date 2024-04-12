using Handball.Core.Contracts;
using Handball.Models;
using Handball.Models.Contracts;
using Handball.Repositories;
using Handball.Repositories.Contracts;
using Handball.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handball.Core
{
    public class Controller : IController
    {
        private IRepository<IPlayer> players;
        private IRepository<ITeam> teams;
        public Controller()
        {
            players = new PlayerRepository();
            teams = new TeamRepository();
        }

        public string NewTeam(string name)
        {
            ITeam team = teams.GetModel(name);
            if (team != null)
            {
                return $"{name} is already added to the {teams.GetType().Name}.";
            }
            else
            {
                team = new Team(name);
                teams.AddModel(team);
                return $"{name} is successfully added to the {teams.GetType().Name}.";
            }
        }

        public string NewPlayer(string typeName, string name)
        {
            if (typeName != "Goalkeeper" && typeName != "CenterBack" && typeName != "ForwardWing")
            {
                return $"{typeName} is invalid position for the application.";
            }
            IPlayer player = players.GetModel(name);
            if (player != null)
            {
                return $"{name} is already added to the {players.GetType().Name} as {player.GetType().Name}.";
            }
            if (typeName == "Goalkeeper")
            {
                player = new Goalkeeper(name);
            }
            else if (typeName == "CenterBack")
            {
                player = new CenterBack(name);
            }
            else if (typeName == "ForwardWing")
            {
                player = new ForwardWing(name);
            }
            players.AddModel(player);
            return $"{name} is filed for the handball league.";
        }


        public string NewContract(string playerName, string teamName)
        {
            IPlayer player = players.GetModel(playerName);
            if (player == null)
            {
                return $"Player with the name {playerName} does not exist in the {players.GetType().Name}.";
            }

            ITeam team = teams.GetModel(teamName);
            if (team == null)
            {
                return $"Team with the name {teamName} does not exist in the {teams.GetType().Name}.";
            }

            if (!string.IsNullOrEmpty(player.Team))
            {
                return $"Player {playerName} has already signed with {player.Team}.";
            }

            team.SignContract(player);
            player.JoinTeam(teamName);
            return $"Player {playerName} signed a contract with {teamName}.";
        }

        public string NewGame(string firstTeamName, string secondTeamName)
        {
            ITeam firstTeam = teams.GetModel(firstTeamName);
            ITeam secondTeam = teams.GetModel(secondTeamName);
            if (firstTeam.OverallRating > secondTeam.OverallRating)
            {
                firstTeam.Win();
                secondTeam.Lose();
                return $"Team {firstTeam.Name} wins the game over {secondTeam.Name}!";
            }

            else if (secondTeam.OverallRating > firstTeam.OverallRating)
            {
                secondTeam.Win();
                firstTeam.Lose();
                return $"Team {secondTeam.Name} wins the game over {firstTeam.Name}!";
            }

            else
            {
                firstTeam.Draw();
                secondTeam.Draw();
                return $"The game between {firstTeamName} and {secondTeamName} ends in a draw!";
            }          
        }

        

        

        public string PlayerStatistics(string teamName)
        {
            ITeam team = teams.GetModel(teamName);
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"***{teamName}***");
            foreach (var player in team.Players.OrderByDescending(p => p.Rating)
                .ThenBy(p => p.Name))
            {
                sb.AppendLine(player.ToString());
            }
            return sb.ToString().Trim();
        }
        public string LeagueStandings()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("***League Standings***");
            foreach (ITeam team in teams.Models
                .OrderByDescending(t => t.PointsEarned)
                .ThenByDescending(t => t.OverallRating)
                .ThenBy(t => t.Name))
            {
                sb.AppendLine(team.ToString());
            }
            return sb.ToString().Trim();
        }
    }
}
