using RobotService.Models.Contracts;
using RobotService.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotService.Repositories
{
    public class RobotRepository : IRepository<IRobot>
    {
        private List<IRobot> models;
        public RobotRepository() 
        { 
            models = new List<IRobot>();
        }
        public IReadOnlyCollection<IRobot> Models() => models.AsReadOnly();

        public void AddNew(IRobot model)
        {
            models.Add(model);
        }

        public IRobot FindByStandard(int interfaceStandard)
        {
            return models.FirstOrDefault(m => m.InterfaceStandards.Contains(interfaceStandard)); //Does it return null?
        }    

        public bool RemoveByName(string typeName)
        {
            return models.Remove(models.FirstOrDefault(m => m.GetType().Name == typeName));
        }
    }
}
