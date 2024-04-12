using NUnit.Framework;

namespace RobotFactory.Tests
{
    public class FactoryTests
    {
        private Factory factory;    
        [SetUp]
        public void Setup()
        {
            factory = new Factory("Factory", 1);
        }

        [Test]
        public void Ctor_SetsDataCorrectly()
        {        
            Assert.IsNotNull(factory);
            Assert.IsNotNull(factory.Robots);
            Assert.IsNotNull(factory.Supplements);
            Assert.AreEqual(factory.Robots.Count, 0);
            Assert.AreEqual(factory.Supplements.Count, 0);
            Assert.AreEqual(factory.Name, "Factory");
            Assert.AreEqual(factory.Capacity, 1);
        }
        [Test]
        public void ProduceRobot_AddsRobot()
        {
            string model = "KP3";
            double price = 100;
            int interfaceStandard = 50;
            string returned = factory.ProduceRobot(model, price, interfaceStandard);
            string expected = $"Produced --> {new Robot(model, price, interfaceStandard).ToString()}";
            Assert.AreEqual(expected, returned);
            Assert.AreEqual(factory.Robots.Count, 1);
        }
        [Test]
        public void ProduceRobot_CapacityReached_ShouldNotAddRobot()
        {
            string model = "KP3";
            double price = 100;
            int interfaceStandard = 50;
            factory.ProduceRobot(model, price, interfaceStandard);
            string returned = factory.ProduceRobot(model, price, interfaceStandard);
            string expected = "The factory is unable to produce more robots for this production day!";
            Assert.AreEqual(expected, returned);
            Assert.AreEqual(factory.Robots.Count, 1);
        }
        [Test]
        public void ProduceSupplement_ShouldAddSupplement()
        {
            string suppName = "Name";
            int interfaceStandard = 50;

            string returned = factory.ProduceSupplement(suppName, interfaceStandard);
            string expected = new Supplement(suppName, interfaceStandard).ToString();
            Assert.AreEqual(expected,returned);
            Assert.AreEqual(factory.Supplements.Count, 1);
            factory.ProduceSupplement(suppName, interfaceStandard);
            Assert.AreEqual(factory.Supplements.Count, 2);
        }
        [Test]
        public void UpgradeRobot_ShouldAddSupplementToRobot()
        {
            Robot robot = new Robot("KP3",100,50);
            Supplement supplement = new Supplement("Name",50);
            bool returned = factory.UpgradeRobot(robot, supplement);
            bool expected = true;
            Assert.AreEqual(expected,returned); 
            Assert.IsTrue(robot.Supplements.Contains(supplement));
            Assert.AreEqual(robot.Supplements.Count, 1);
            supplement = new Supplement("Name2", 50);
            factory.UpgradeRobot(robot, supplement);
            Assert.AreEqual(robot.Supplements.Count, 2);
        }
        [Test]
        public void UpgradeRobot_RobotAlreadyContainsSupplement()
        {
            Robot robot = new Robot("KP3", 100, 50);
            Supplement supplement = new Supplement("Name", 50);
            factory.UpgradeRobot(robot, supplement);
            Assert.AreEqual(robot.Supplements.Count, 1);
            Assert.IsTrue(robot.Supplements.Contains(supplement));
            bool returned = factory.UpgradeRobot(robot, supplement);
            bool expected = false;
            Assert.AreEqual(expected, returned);         
            Assert.AreEqual(robot.Supplements.Count, 1);
        }
        [Test]
        public void UpgradeRobot_StandardsDontMatch()
        {
            Robot robot = new Robot("KP3", 100, 50);
            Supplement supplement = new Supplement("Name", 20);
            bool returned = factory.UpgradeRobot(robot, supplement);
            bool expected = false;
            Assert.AreEqual(expected, returned);
            Assert.IsFalse(robot.Supplements.Contains(supplement));
            Assert.AreEqual(robot.Supplements.Count, 0);
        }
        [Test]
        public void SellRobot_EqualPrice_ShouldReturnRobot()
        {
            factory.ProduceRobot("KP3", 100, 50);
            Robot expectedRobot = new Robot("KP3", 100, 50);
            Robot returnedRobot = factory.SellRobot(100);
            Assert.AreEqual(expectedRobot.Model, returnedRobot.Model);
            Assert.AreEqual(expectedRobot.Price, returnedRobot.Price);
            Assert.AreEqual(expectedRobot.InterfaceStandard, returnedRobot.InterfaceStandard);
        }
        [Test]
        public void SellRobot_RobotPriceIsLower_ShouldReturnRobot()
        {
            factory.ProduceRobot("KP3", 80, 50);
            Robot expectedRobot = new Robot("KP3", 80, 50);
            Robot returnedRobot = factory.SellRobot(100);
            Assert.AreEqual(expectedRobot.Model, returnedRobot.Model);
            Assert.AreEqual(expectedRobot.Price, returnedRobot.Price);
            Assert.AreEqual(expectedRobot.InterfaceStandard, returnedRobot.InterfaceStandard);
        }
        [Test]
        public void SellRobot_RobotPriceIsHigher_ShouldReturnNull()
        {
            factory.ProduceRobot("KP3", 180, 50);
            Robot expectedRobot = null;
            Robot returnedRobot = factory.SellRobot(100);
            Assert.AreEqual(expectedRobot, returnedRobot);
        }
        [Test]
        public void SellRobot_RobotDoesntExist_ShouldReturnNull()
        {
            Robot expectedRobot = null;
            Robot returnedRobot = factory.SellRobot(100);
            Assert.AreEqual(expectedRobot, returnedRobot);
        }
    }
}