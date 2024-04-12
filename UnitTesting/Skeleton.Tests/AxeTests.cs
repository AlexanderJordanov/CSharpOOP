using NUnit.Framework;
using System;

namespace Skeleton.Tests
{
    [TestFixture]
    public class AxeTests
    {
        private Axe axe;
        private Dummy dummy;
        [SetUp]
        public void SetUp()
        {
            axe = new Axe(3, 1);
            dummy = new Dummy(10, 10);
        }
        [Test]
        public void Ctor_AxeShouldSetDataCorrectly()
        {
            Assert.AreEqual(axe.AttackPoints, 3);
            Assert.AreEqual(axe.DurabilityPoints, 1);
        }
        [Test]
        public void Attack_AxeShouldLoseDurability()
        {
            axe.Attack(dummy);
            Assert.AreEqual(axe.DurabilityPoints, 0);
        }
        [Test]
        public void Attack_BrokenAxe_ShouldThrowException()
        {
            axe.Attack(dummy);
            Assert.Throws<InvalidOperationException>(() => axe.Attack(dummy));
        }
    }
}