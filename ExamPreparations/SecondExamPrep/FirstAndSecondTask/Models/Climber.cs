using HighwayToPeak.Models.Contracts;
using HighwayToPeak.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HighwayToPeak.Models
{
    public abstract class Climber : IClimber
    {
        private string name;
        private int stamina;
        List<string> conqueredPeaks;

        protected Climber(string name, int stamina)
        {
            Name = name;
            Stamina = stamina;
            conqueredPeaks = new List<string>();
        }

        public string Name
        {
            get => name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.ClimberNameNullOrWhiteSpace);
                }
                name = value;
            }
        }

        public int Stamina
        {
            get => stamina;
            protected set
            {
                if (value <= 0)
                {
                    stamina = 0;
                }
                else if (value > 10)
                {
                    stamina = 10;
                }
                else
                {
                    stamina = value;
                }
            }
        }

        public IReadOnlyCollection<string> ConqueredPeaks => conqueredPeaks.AsReadOnly();

        public void Climb(IPeak peak)
        {
            if (peak.DifficultyLevel == "Extreme")
            {
                stamina -= 6;
            }
            else if (peak.DifficultyLevel == "Hard")
            {
                stamina -= 4;
            }
            else if (peak.DifficultyLevel == "Moderate")
            {
                stamina -= 2;
            }
            if (!conqueredPeaks.Contains(peak.Name))
            {
                conqueredPeaks.Add(peak.Name);
            }
        }

        public abstract void Rest(int daysCount);

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"{this.GetType().Name} - Name: {Name}, Stamina: {Stamina}");
            if (conqueredPeaks.Count > 0)
            {
                sb.AppendLine($"Peaks conquered: {conqueredPeaks.Count}");
            }
            else
            {
                sb.AppendLine("Peaks conquered: no peaks conquered");
            }
            return sb.ToString().Trim();
        }
    }
}
