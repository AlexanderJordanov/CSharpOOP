using RobotService.Core.Contracts;
using RobotService.Models;
using RobotService.Models.Contracts;
using RobotService.Repositories;
using RobotService.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RobotService.Core
{
    public class Controller : IController
    {
        private IRepository<ISupplement> supplements;
        private IRepository<IRobot> robots;
        public Controller()
        {
            supplements = new SupplementRepository();
            robots = new RobotRepository();
        }
        public string CreateRobot(string model, string typeName)
        {
            if (typeName != "DomesticAssistant" && typeName != "IndustrialAssistant")
            {
                return $"Robot type {typeName} cannot be created.";
            }

            IRobot robot = null;
            if (typeName == "DomesticAssistant")
            {
                robot = new DomesticAssistant(model);
            }
            else if (typeName == "IndustrialAssistant")
            {
                robot = new IndustrialAssistant(model);
            }
            //what if I already have the same robot?
            robots.AddNew(robot);
            return $"{typeName} {model} is created and added to the RobotRepository.";
        }

        public string CreateSupplement(string typeName)
        {
            if (typeName != "SpecializedArm" && typeName != "LaserRadar")
            {
                return $"{typeName} is not compatible with our robots.";
            }

            ISupplement supplement = null;
            if (typeName == "SpecializedArm")
            {
                supplement = new SpecializedArm();
            }
            else if (typeName == "LaserRadar")
            {
                supplement = new LaserRadar();
            }
            //what if I already have the same supplement?
            supplements.AddNew(supplement);
            return $"{typeName} is created and added to the SupplementRepository.";
        }

        public string UpgradeRobot(string model, string supplementTypeName)
        {
            int standardToSearch = 0;
            if (supplementTypeName == "SpecializedArm")
            {
                standardToSearch = 10045;
            }
            else if (supplementTypeName == "LaserRadar")
            {
                standardToSearch = 20082;
            }
            ISupplement supplement = supplements.FindByStandard(standardToSearch);
            List<IRobot> models = new List<IRobot>();
            foreach (var robot in robots.Models())
            {
                if (!robot.InterfaceStandards.Contains(standardToSearch) && robot.Model == model)
                {
                    models.Add(robot);
                }
            }
            if (models.Count <= 0)
            {
                return $"All {model} are already upgraded!";
            }
            else
            {
                IRobot robot = models.First();
                robot.InstallSupplement(supplement);
                supplements.RemoveByName(supplementTypeName);
                return $"{model} is upgraded with {supplementTypeName}.";
            }
        }

        public string PerformService(string serviceName, int intefaceStandard, int totalPowerNeeded)
        {
            List<IRobot> models = new List<IRobot>();
            foreach (var robot in robots.Models())
            {
                if (robot.InterfaceStandards.Contains(intefaceStandard))
                {
                    models.Add(robot);
                }
            }
            if (models.Count == 0)
            {
                return $"Unable to perform service, {intefaceStandard} not supported!";
            }
            int counter = 0;
            int sum = models.Sum(b => b.BatteryLevel);
            if (sum < totalPowerNeeded)
            {
                return $"{serviceName} cannot be executed! {totalPowerNeeded - sum} more power needed.";
            }
            else
            {

                foreach (var robot in models.OrderByDescending(m => m.BatteryLevel))
                {
                    if (robot.BatteryLevel >= totalPowerNeeded)
                    {
                        counter++;
                        int currentBattery = robot.BatteryLevel;
                        robot.ExecuteService(totalPowerNeeded);
                        totalPowerNeeded -= currentBattery;
                    }
                    else
                    {
                        counter++;
                        totalPowerNeeded -= robot.BatteryLevel;
                        robot.ExecuteService(robot.BatteryLevel);                        
                    }
                    if (totalPowerNeeded <= 0)
                    {
                        break;
                    }

                }
                return $"{serviceName} is performed successfully with {counter} robots.";
            }
        }

        public string RobotRecovery(string model, int minutes)
        {
            var models = robots.Models();
            var correctModelRobots = models.Where(m => m.Model == model && m.BatteryLevel <= m.BatteryCapacity / 2).ToList();
            foreach (var robot in correctModelRobots)
            {
                robot.Eating(minutes);
            }
            return $"Robots fed: {correctModelRobots.Count}";
        }

        public string Report()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var robot in robots.Models().OrderByDescending(r => r.BatteryLevel).ThenBy(r => r.BatteryCapacity))
            {
                sb.AppendLine(robot.ToString());
            }
            return sb.ToString().TrimEnd();
        }
    }
}
