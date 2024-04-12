using NauticalCatchChallenge.Core.Contracts;
using NauticalCatchChallenge.Models;
using NauticalCatchChallenge.Models.Contracts;
using NauticalCatchChallenge.Repositories;
using NauticalCatchChallenge.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace NauticalCatchChallenge.Core
{
    public class Controller : IController
    {
        private DiverRepository divers;
        private FishRepository fish;
        public Controller()
        {
            divers = new DiverRepository();
            fish = new FishRepository();
        }

        public string DiveIntoCompetition(string diverType, string diverName)
        {
            if (diverType != "FreeDiver" &&  diverType != "ScubaDiver")
            {
                return string.Format(OutputMessages.DiverTypeNotPresented, diverType);
            }
            if (divers.GetModel(diverName) != null)
            {
                return string.Format(OutputMessages.DiverNameDuplication, diverName, divers.GetType().Name);
            }
            IDiver diver = null;
            if (diverType == "FreeDiver")
            {
                diver = new FreeDiver(diverName);
            }
            else if (diverType == "ScubaDiver")
            {
                diver = new ScubaDiver(diverName);
            }
            divers.AddModel(diver);
            return string.Format(OutputMessages.DiverRegistered, diverName, divers.GetType().Name);
        }

        public string SwimIntoCompetition(string fishType, string fishName, double points)
        {
            if (fishType != "ReefFish" && fishType != "DeepSeaFish" && fishType != "PredatoryFish")
            {
                return string.Format(OutputMessages.FishTypeNotPresented, fishType);
            }
            if (fish.GetModel(fishName) != null)
            {
                return string.Format(OutputMessages.FishNameDuplication, fishName, fish.GetType().Name);
            }
            IFish fishToAdd = null;
            if (fishType == "ReefFish")
            {
                fishToAdd = new ReefFish(fishName,points);
            }
            else if (fishType == "DeepSeaFish")
            {
                fishToAdd = new DeepSeaFish(fishName,points);
            }
            else if (fishType == "PredatoryFish")
            {
                fishToAdd = new PredatoryFish(fishName,points);
            }
            fish.AddModel(fishToAdd);
            return string.Format(OutputMessages.FishCreated, fishName);
        }

        public string ChaseFish(string diverName, string fishName, bool isLucky)
        {
            if (divers.GetModel(diverName) == null)
            {
                return string.Format(OutputMessages.DiverNotFound, divers.GetType().Name, diverName);
            }
            if (fish.GetModel(fishName) == null)
            {
                return string.Format(OutputMessages.FishNotAllowed, fishName);
            }
            IDiver diverToChase = divers.GetModel(diverName);
            IFish chasedFish = fish.GetModel(fishName);
            if (diverToChase.HasHealthIssues)
            {
                return string.Format(OutputMessages.DiverHealthCheck, diverName);
            }
            if (diverToChase.OxygenLevel < chasedFish.TimeToCatch)
            {
                diverToChase.Miss(chasedFish.TimeToCatch);
                return string.Format(OutputMessages.DiverMisses, diverName, fishName);
            }
            else if (diverToChase.OxygenLevel ==  chasedFish.TimeToCatch)
            {
                if (isLucky)
                {
                    diverToChase.Hit(chasedFish);
                    return string.Format(OutputMessages.DiverHitsFish, diverName, chasedFish.Points, fishName);
                }
                else
                {
                    diverToChase.Miss(chasedFish.TimeToCatch);
                    return string.Format(OutputMessages.DiverMisses, diverName, fishName);
                }
            }
            else 
            {
                diverToChase.Hit(chasedFish);
                return string.Format(OutputMessages.DiverHitsFish, diverName, chasedFish.Points, fishName);
            }
        }

        public string HealthRecovery()
        {
            List<IDiver> unhealthyDivers = divers.Models.Where(d => d.HasHealthIssues).ToList();
            foreach (var diver in unhealthyDivers)
            {
                diver.UpdateHealthStatus();
                diver.RenewOxy();
            }
            return string.Format(OutputMessages.DiversRecovered, unhealthyDivers.Count);
        }


        public string DiverCatchReport(string diverName)
        {
            IDiver diver = divers.GetModel(diverName);
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Diver [ Name: {diver.Name}, Oxygen left: {diver.OxygenLevel}, Fish caught: {diver.Catch.Count}, Points earned: {diver.CompetitionPoints} ]");
            sb.AppendLine("Catch Report:");
            foreach(var fishName in diver.Catch)
            {
                IFish reportFish = fish.GetModel(fishName);
                sb.AppendLine(reportFish.ToString());
            }
            return sb.ToString().Trim();
        }
          
        public string CompetitionStatistics()
        {
            List<IDiver> reportDivers = divers.Models.Where(d => d.HasHealthIssues == false)
                .OrderByDescending(d => d.CompetitionPoints)
                .ThenByDescending(d => d.Catch.Count)
                .ThenBy(d => d.Name)
                .ToList();
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("**Nautical-Catch-Challenge**");
            foreach(var diver in reportDivers)
            {
                sb.AppendLine(diver.ToString());
            }
            return sb.ToString().Trim();
        } 
    }
}
