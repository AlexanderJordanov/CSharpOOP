using NUnit.Framework;
using System;

namespace Skeleton.Tests
{
    [TestFixture]
    public class DummyTests
    {
        private Dummy dummy;
        [SetUp]
        public void SetUp()
        {
            dummy = new Dummy(2, 2);
        }
        [Test]
        public void Ctor_DummyShouldSetDataCorrectly()
        {
            Assert.AreEqual(dummy.Health, 2);
        }
        [Test]
        public void TakeAttack_DummyShouldLoseHealth()
        {
            dummy.TakeAttack(2);
            Assert.AreEqual(dummy.Health, 0);
        }
        [Test]
        public void TakeAttack_DeadDummy_ShouldThrowException()
        {
            dummy.TakeAttack(2);
            Assert.Throws<InvalidOperationException>(() => dummy.TakeAttack(2));
        }
        [Test]
        public void GiveExperience_DeadDummy_ShouldGive()
        {
            dummy.TakeAttack(2);
            int exp = dummy.GiveExperience();
            Assert.AreEqual(exp, 2);
        }
        [Test]
        public void GiveExperience_AliveDummy_ShouldThrowException()
        {
            Assert.Throws<InvalidOperationException>(() => dummy.GiveExperience());          
        }
    }
}