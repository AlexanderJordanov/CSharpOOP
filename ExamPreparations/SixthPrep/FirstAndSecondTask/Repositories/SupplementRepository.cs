using RobotService.Models.Contracts;
using RobotService.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotService.Repositories
{
    public class SupplementRepository : IRepository<ISupplement>
    {
        private List<ISupplement> models;
        public IReadOnlyCollection<ISupplement> Models() => models.AsReadOnly();
        public SupplementRepository()
        {
            models = new List<ISupplement>();
        }

        public void AddNew(ISupplement model)
        {
            models.Add(model);
        }

        public ISupplement FindByStandard(int interfaceStandard)
        {
            return models.FirstOrDefault(m => m.InterfaceStandard == interfaceStandard); //Does it return null?
        }

        public bool RemoveByName(string typeName)
        {
            return models.Remove(models.FirstOrDefault(m => m.GetType().Name == typeName));
        }
    }
}
