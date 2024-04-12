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
    public class PeakRepository : IRepository<IPeak>
    {
        private List<IPeak> all;
        public PeakRepository()
        {
            all = new List<IPeak>();
        }
        public IReadOnlyCollection<IPeak> All => all.AsReadOnly();

        public void Add(IPeak model)
        {
            all.Add(model);
        }

        public IPeak Get(string name)
        {
            IPeak peak = all.FirstOrDefault(x => x.Name == name);
            if (peak != null)
            {
                return peak;
            }
            else
            {
                return null;
            }
        }
    }
}
