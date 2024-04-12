using NUnit.Framework;

namespace VehicleGarage.Tests
{
    public class Tests
    {
        private Vehicle firstVehicle;
        private Vehicle secondVehicle;
        private Garage garage;
        [SetUp]
        public void Setup()
        {
            firstVehicle = new Vehicle("Mazda", "RX7", "123456");
            secondVehicle = new Vehicle("BMW", "E46", "654321");
            garage = new Garage(2);
        }

        [Test]
        public void VehicleCtor_SetsDataCorrectly()
        {
            Assert.IsNotNull(firstVehicle);
            Assert.AreEqual(firstVehicle.Brand, "Mazda");
            Assert.AreEqual(firstVehicle.Model, "RX7");
            Assert.AreEqual(firstVehicle.LicensePlateNumber, "123456");
            Assert.AreEqual(firstVehicle.BatteryLevel, 100);
            Assert.AreEqual(firstVehicle.IsDamaged, false);
            Assert.IsNotNull(secondVehicle);
            Assert.AreEqual(secondVehicle.Brand, "BMW");
            Assert.AreEqual(secondVehicle.Model, "E46");
            Assert.AreEqual(secondVehicle.LicensePlateNumber, "654321");
            Assert.AreEqual(secondVehicle.BatteryLevel, 100);
            Assert.AreEqual(secondVehicle.IsDamaged, false);
        }
        [Test]
        public void GarageCtor_SetsDataCorrectly()
        {
            Assert.IsNotNull(garage);
            Assert.IsNotNull(garage.Vehicles);
            Assert.AreEqual(garage.Capacity, 2);
        }
        [Test]
        public void AddVehicle_HappyPath()
        {
            bool expected = true;
            bool returned = garage.AddVehicle(firstVehicle);
            Assert.AreEqual(expected, returned);
            Assert.AreEqual(garage.Vehicles.Count, 1);
        }
        [Test]
        public void AddVehicle_ExistingVehicle()
        {
            bool expected = false;
            garage.AddVehicle(firstVehicle);
            bool returned = garage.AddVehicle(firstVehicle);
            Assert.AreEqual(expected, returned);
            Assert.AreEqual(garage.Vehicles.Count, 1);
        }
        [Test]
        public void AddVehicle_CapacityReached()
        {
            bool expected = false;
            garage.AddVehicle(firstVehicle);
            garage.AddVehicle(secondVehicle);
            Vehicle thirdVehicle = new Vehicle("Honda", "CRV", "777777");
            bool returned = garage.AddVehicle(thirdVehicle);
            Assert.AreEqual(expected, returned);
            Assert.AreEqual(garage.Vehicles.Count, 2);
        }
        [Test]
        public void ChargeVehicle_ReturnsChargedVehicles()
        {
            int expected = 1;
            firstVehicle.BatteryLevel = 50;
            garage.AddVehicle(firstVehicle);
            int returned = garage.ChargeVehicles(1000);
            Assert.AreEqual(expected,returned);
            Assert.AreEqual(firstVehicle.BatteryLevel, 100);
        }
        [Test]
        public void ChargeVehicle_LowerLevelThanVehicleLevel()
        {
            int expected = 0;
            garage.AddVehicle(firstVehicle);
            int returned = garage.ChargeVehicles(50);
            Assert.AreEqual(expected, returned);
            Assert.AreEqual(firstVehicle.BatteryLevel, 100);
        }
        [Test]
        public void DriceVehicle_ShouldWorkCorrectly_ShouldLowerVehicleBattery()
        {
            garage.AddVehicle(firstVehicle);
            garage.DriveVehicle("123456", 20, false);
            Assert.AreEqual(firstVehicle.BatteryLevel, 80);
            Assert.AreEqual(firstVehicle.IsDamaged, false);
        }
        [Test]
        public void DriveVehicle_ShouldNotWork_IsDamagedTrue()
        {
            firstVehicle.IsDamaged = true;
            garage.AddVehicle(firstVehicle);
            garage.DriveVehicle("123456",20,false);
            Assert.AreEqual(firstVehicle.BatteryLevel, 100);
        }
        [Test]
        public void DriveVehicle_ShouldNotWork_DrainageHigherThan100()
        {
            garage.AddVehicle(firstVehicle);
            garage.DriveVehicle("123456", 101, false);
            Assert.AreEqual(firstVehicle.BatteryLevel, 100);
            Assert.AreEqual(firstVehicle.IsDamaged, false);
        }
        [Test]
        public void DriveVehicle_ShouldNotWork_DrainageHigherThanVehicleBattery()
        {
            firstVehicle.BatteryLevel = 50;
            garage.AddVehicle(firstVehicle);
            garage.DriveVehicle("123456", 80, false);
            Assert.AreEqual(firstVehicle.BatteryLevel, 50);
            Assert.AreEqual(firstVehicle.IsDamaged, false);
        }
        [Test]
        public void DriveVehicle_ShouldChangeIsDamaged()
        {
            garage.AddVehicle(firstVehicle);
            garage.DriveVehicle("123456",20, true);
            Assert.AreEqual(firstVehicle.BatteryLevel, 80);
            Assert.AreEqual(firstVehicle.IsDamaged, true);
        }
        [Test]
        public void RepairVehicles_ShouldWorkCorrectly()
        {
            string expected = "Vehicles repaired: 2";
            firstVehicle.IsDamaged = true;
            secondVehicle.IsDamaged = true;
            garage.AddVehicle(firstVehicle);
            garage.AddVehicle(secondVehicle);
            string returned = garage.RepairVehicles();
            Assert.AreEqual(expected,returned);
            Assert.IsFalse(firstVehicle.IsDamaged);
            Assert.IsFalse(secondVehicle.IsDamaged);
        }
    }
}