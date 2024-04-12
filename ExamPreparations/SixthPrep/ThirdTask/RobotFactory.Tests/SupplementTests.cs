using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotFactory.Tests
{
    public class SupplementTests
    {
        private Supplement supplement;
        [SetUp]
        public void Setup()
        {
            supplement = new Supplement("Name", 20);
        }

        [Test]
        public void Ctor_SetsDataCorrectly()
        {
            Assert.IsNotNull(supplement);
            Assert.AreEqual("Name", supplement.Name);
            Assert.AreEqual(20, supplement.InterfaceStandard);
        }
        [Test]
        public void ToString_ReturnsCorrectString()
        {
            string expected = "Supplement: Name IS: 20";
            string returned = supplement.ToString();
            Assert.AreEqual(expected, returned);
        }
    }
}
