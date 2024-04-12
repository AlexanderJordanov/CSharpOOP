using Handball.Models.Contracts;
using Handball.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handball.Repositories
{
    public class TeamRepository : IRepository<ITeam>
    {
        private List<ITeam> _models;
        public TeamRepository()
        {
            _models = new List<ITeam>();
        }
        public IReadOnlyCollection<ITeam> Models => _models;

        public void AddModel(ITeam model)
        {
            _models.Add(model);
        }

        public bool ExistsModel(string name)
        {
            ITeam team = _models.FirstOrDefault(m => m.Name == name);
            if (team == null)
            {
                return false;
            }
            return true;
        }

        public ITeam GetModel(string name)
        {
            ITeam team = _models.FirstOrDefault(m => m.Name == name);
            if (team == null)
            {
                return null;
            }    
            return team;
        }

        public bool RemoveModel(string name)
        {
            ITeam team = _models.FirstOrDefault(m => m.Name == name);
            if (team == null)
            {
                return false;
            }
            _models.Remove(team);
            return true;
        }
    }
}
