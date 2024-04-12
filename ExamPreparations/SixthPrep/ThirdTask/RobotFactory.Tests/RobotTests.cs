using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RobotFactory.Tests
{
    public class RobotTests
    {
        private Robot robot;
        [SetUp]
        public void Setup()
        {
            robot = new Robot("KP3", 100, 50);
        }

        [Test]
        public void Ctor_SetsDataCorrectly()
        {
            Assert.IsNotNull(robot);
            Assert.AreEqual(robot.Model, "KP3");
            Assert.AreEqual(robot.Price, 100);
            Assert.AreEqual(robot.InterfaceStandard, 50);
            Assert.IsNotNull(robot.Supplements);
            Assert.AreEqual(robot.Supplements.Count, 0);
        }
        [Test]
        public void ToString_ReturnsCorrectString()
        {
            string expected = $"Robot model: KP3 IS: 50, Price: {100:f2}";
            string returned = robot.ToString();
            Assert.AreEqual(expected, returned);
        }
    }
}
