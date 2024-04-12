namespace CarManager.Tests
{
    using NUnit.Framework;
    using System;
    using System.Diagnostics.Contracts;

    [TestFixture]
    public class CarManagerTests
    {
        private const string CorrectMake = "Lamborghini";
        private const string CorrectModel = "Aventador";
        private const double CorrectFuelConsumption = 7.5;
        private const double CorrectFuelCapacity = 100;
        Car lamborghini;

        [SetUp]
        public void SetUp()
        {
            lamborghini = new Car(CorrectMake, CorrectModel, CorrectFuelConsumption, CorrectFuelCapacity);
        }
        [Test]
        public void Ctor_LegitData_ShouldCreateNewInstance()
        {
            Assert.IsNotNull(lamborghini);
            Assert.AreEqual(lamborghini.Make, CorrectMake);
            Assert.AreEqual(lamborghini.Model, CorrectModel);
            Assert.AreEqual(lamborghini.FuelConsumption, CorrectFuelConsumption);
            Assert.AreEqual(lamborghini.FuelCapacity, CorrectFuelCapacity);
            Assert.AreEqual(lamborghini.FuelAmount, 0);
        }
        [Test]
        public void Ctor_NullMake_ShouldThrowException()
        {
           Assert.Throws<ArgumentException> (() =>lamborghini = new Car(null, CorrectModel, CorrectFuelConsumption, CorrectFuelCapacity));
        }
        [Test]
        public void Ctor_EmptyMake_ShouldThrowException()
        {
            Assert.Throws<ArgumentException>(() => lamborghini = new Car(string.Empty, CorrectModel, CorrectFuelConsumption, CorrectFuelCapacity));
        }
        [Test]
        public void Ctor_NullModel_ShouldThrowException()
        {
            Assert.Throws<ArgumentException>(() => lamborghini = new Car(CorrectMake, null, CorrectFuelConsumption, CorrectFuelCapacity));
        }
        [Test]
        public void Ctor_EmptyModel_ShouldThrowException()
        {
            Assert.Throws<ArgumentException>(() => lamborghini = new Car(CorrectMake, string.Empty, CorrectFuelConsumption, CorrectFuelCapacity));
        }
        [Test]
        public void Ctor_ZeroFuelConsumption_ShouldThrowException()
        {
            Assert.Throws<ArgumentException>(() => lamborghini = new Car(CorrectMake, CorrectModel, 0, CorrectFuelCapacity));
        }
        [Test]
        public void Ctor_NegativeFuelConsumption_ShouldThrowException()
        {
            Assert.Throws<ArgumentException>(() => lamborghini = new Car(CorrectMake, CorrectModel, -1, CorrectFuelCapacity));
        }
        [Test]
        public void Ctor_ZeroFuelCapacity_ShouldThrowException()
        {
            Assert.Throws<ArgumentException>(() => lamborghini = new Car(CorrectMake, CorrectModel, CorrectFuelConsumption, 0));
        }
        [Test]
        public void Ctor_NegativeFuelCapacity_ShouldThrowException()
        {
            Assert.Throws<ArgumentException>(() => lamborghini = new Car(CorrectMake, CorrectModel, CorrectFuelConsumption, -1));
        }
        [Test]
        public void Refuel_HappyPath_ShouldRefuelCorrectly()
        {
            lamborghini.Refuel(20);
            Assert.AreEqual(lamborghini.FuelAmount, 20);
        }
        [Test]
        public void Refuel_MoreThanCapacity_ShouldRefuelNoMoreThanCapacity()
        {
            lamborghini.Refuel(120);
            Assert.AreEqual(lamborghini.FuelAmount, lamborghini.FuelCapacity);
        }
        [Test]
        public void Refuel_NegativeAmount_ShouldThrowException()
        {
            Assert.Throws<ArgumentException>(() => lamborghini.Refuel(-1));
        }
        [Test]
        public void Refuel_ZeroAmount_ShouldThrowException()
        {
            Assert.Throws<ArgumentException>(() => lamborghini.Refuel(0));
        }
        [Test]
        public void Drive_HappyPath_ShouldDriveCorrectly()
        {
            lamborghini.Refuel(10);
            lamborghini.Drive(100);
            Assert.AreEqual(lamborghini.FuelAmount, 2.5);
        }
        [Test]
        public void Drive_NotEnoughFuel_ShouldThrowException()
        {
            Assert.Throws<InvalidOperationException>(() => lamborghini.Drive(100));
        }
    }
}