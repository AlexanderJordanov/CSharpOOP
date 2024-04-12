using HighwayToPeak.Core.Contracts;
using HighwayToPeak.Models;
using HighwayToPeak.Models.Contracts;
using HighwayToPeak.Repositories;
using HighwayToPeak.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HighwayToPeak.Core
{
    public class Controller : IController
    {
        private PeakRepository peaks = new();
        private ClimberRepository climbers = new();
        private BaseCamp baseCamp = new();
        public string AddPeak(string name, int elevation, string difficultyLevel)
        {
            IPeak peak = new Peak (name, elevation, difficultyLevel);   
            if (peaks.Get(name) != null)
            {
                return string.Format(OutputMessages.PeakAlreadyAdded, name);
            }
            else if (difficultyLevel != "Extreme" && difficultyLevel != "Hard" && difficultyLevel != "Moderate")
            {
                return string.Format(OutputMessages.PeakDiffucultyLevelInvalid, difficultyLevel);
            }
            peaks.Add(peak);
            return string.Format(OutputMessages.PeakIsAllowed, name, peaks.GetType().Name);
        }

        public string NewClimberAtCamp(string name, bool isOxygenUsed)
        {
            if (climbers.Get(name) != null)
            {
                return string.Format(OutputMessages.ClimberCannotBeDuplicated, name, climbers.GetType().Name);
            }
            else
            {
                IClimber climber;
                if (!isOxygenUsed)
                {
                    climber = new NaturalClimber(name);
                }
                else
                {
                    climber = new OxygenClimber(name);
                }
                climbers.Add(climber);
                baseCamp.ArriveAtCamp(name);
                return $"{name} has arrived at the BaseCamp and will wait for the best conditions.";
            }
        }

        public string AttackPeak(string climberName, string peakName)
        {
            if (climbers.Get(climberName) == null) 
            {
                return string.Format(OutputMessages.ClimberNotArrivedYet, climberName);
            }
            if (peaks.Get(peakName) == null)
            {
                return string.Format(OutputMessages.PeakIsNotAllowed, peakName);
            }
            if (!baseCamp.Residents.Contains(climberName)) 
            { 
                return string.Format(OutputMessages.ClimberNotFoundForInstructions, climberName, peakName);
            }
            IClimber climber = climbers.Get(climberName);
            IPeak peak = peaks.Get(peakName);
            if (peak.DifficultyLevel == "Extreme" && climber.GetType().Name == "NaturalClimber")
            {
                return string.Format(OutputMessages.NotCorrespondingDifficultyLevel, climberName, peakName);
            }
            baseCamp.LeaveCamp(climberName);
            climber.Climb(peak);
            if (climber.Stamina <= 0)
            {
                return string.Format(OutputMessages.NotSuccessfullAttack, climberName);
            }
            else
            {
                baseCamp.ArriveAtCamp(climberName);
                return string.Format(OutputMessages.SuccessfulAttack, climberName, peakName);
            }
        }

        public string BaseCampReport()
        {
            StringBuilder sb= new StringBuilder();
            sb.AppendLine("BaseCamp residents:");
            foreach(var climberName in baseCamp.Residents)
            {
                IClimber climber = climbers.Get(climberName);
                sb.AppendLine($"Name: {climber.Name}, Stamina: {climber.Stamina}, Count of Conquered Peaks: {climber.ConqueredPeaks.Count}");
            }
            return sb.ToString().Trim();
        }

        public string CampRecovery(string climberName, int daysToRecover)
        {
            if (!baseCamp.Residents.Contains(climberName)) 
            { 
                return string.Format(OutputMessages.ClimberIsNotAtBaseCamp, climberName);
            }
            IClimber climber = climbers.Get(climberName);   
            if (climber.Stamina == 10)
            {
                return string.Format(OutputMessages.NoNeedOfRecovery, climberName);
            }
            climber.Rest(daysToRecover);
            return string.Format(OutputMessages.ClimberRecovered, climberName, daysToRecover);
        }

        

        public string OverallStatistics()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("***Highway-To-Peak***");
            foreach(var climber in climbers.All.OrderByDescending(c => c.ConqueredPeaks.Count).ThenBy(c => c.Name))
            {
                sb.AppendLine(climber.ToString());
                List<IPeak> climberPeaks = new List<IPeak>();
                foreach(var peakName in climber.ConqueredPeaks)
                {
                    IPeak peak = peaks.Get(peakName);
                    climberPeaks.Add(peak);
                }
                foreach(var peak in climberPeaks.OrderByDescending(p => p.Elevation))
                {
                    sb.AppendLine(peak.ToString());
                }
            }
            return sb.ToString().Trim();
        }
    }
}
