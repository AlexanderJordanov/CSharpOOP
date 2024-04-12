using HighwayToPeak.Models;
using HighwayToPeak.Models.Contracts;
using HighwayToPeak.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HighwayToPeak.Repositories
{
    public class ClimberRepository : IRepository<IClimber>
    {
        private List<IClimber> all;
        public ClimberRepository()
        {
            all = new List<IClimber>();
        }
        public IReadOnlyCollection<IClimber> All => all.AsReadOnly();

        public void Add(IClimber model)
        {
            all.Add(model);
        }

        public IClimber Get(string name)
        {
            IClimber climber = all.FirstOrDefault(x => x.Name == name);
            if (climber != null)
            {
                return climber;
            }
            else
            {
                return null;
            }
        }
    }
}
