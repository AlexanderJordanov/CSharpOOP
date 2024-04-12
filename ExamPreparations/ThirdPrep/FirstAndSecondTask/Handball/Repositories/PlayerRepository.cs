using Handball.Models.Contracts;
using Handball.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handball.Repositories
{
    public class PlayerRepository : IRepository<IPlayer>
    {
        private List<IPlayer> _models;
        public PlayerRepository()
        {
            _models = new List<IPlayer>();
        }
        public IReadOnlyCollection<IPlayer> Models => _models;

        public void AddModel(IPlayer model)
        {
            _models.Add(model);
        }

        public bool ExistsModel(string name)
        {
            IPlayer player = _models.FirstOrDefault(x => x.Name == name);
            if (player == null)
            {
                return false;
            }
            return true;
        }

        public IPlayer GetModel(string name)
        {
            IPlayer player = _models.FirstOrDefault(m => m.Name == name);
            if (player == null)
            {
                return null;
            }
            else
            {
                return player;
            }
        }

        public bool RemoveModel(string name)
        {
            IPlayer player = _models.FirstOrDefault(m => m.Name == name);
            if (player == null)
            {
                return false;
            }
            _models.Remove(player);
            return true;
        }
    }
}
