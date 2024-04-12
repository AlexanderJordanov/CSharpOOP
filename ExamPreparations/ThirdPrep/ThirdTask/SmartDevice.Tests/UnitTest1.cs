namespace SmartDevice.Tests
{
    using NUnit.Framework;
    using System;
    using System.Text;

    public class Tests
    {
        private Device device;
        [SetUp]
        public void Setup()
        {
            device = new Device(50);
        }

        [Test]
        public void Ctor_SetsDataCorrectly()
        {
            Assert.IsNotNull(device);
            Assert.AreEqual(device.Photos, 0);
            Assert.IsNotNull(device.Applications);
            Assert.AreEqual(device.MemoryCapacity, 50);
            Assert.AreEqual(device.AvailableMemory, 50);
        }
        [Test]
        public void TakePhoto_LowerThanAvailableMemory()
        {
            bool expected = true;
            bool returned = device.TakePhoto(10);
            Assert.AreEqual(expected, returned);
            Assert.AreEqual(device.AvailableMemory, 40);
            Assert.AreEqual(device.Photos,1);
        }
        [Test]
        public void TakePhoto_HigherThanAvailableMemory()
        {
            bool expected = false;
            bool returned = device.TakePhoto(60);
            Assert.AreEqual(expected, returned);
            Assert.AreEqual(device.AvailableMemory, 50);
            Assert.AreEqual(device.Photos, 0);
        }
        [Test]
        public void InstallApp_LowerThanAvailableMemory()
        {
            string expected = $"SA is installed successfully. Run application?";
            string returned = device.InstallApp("SA", 20);
            Assert.AreEqual(expected, returned);
            Assert.AreEqual(device.AvailableMemory, 30);
            Assert.AreEqual(device.Applications.Count, 1);
        }
        [Test]
        public void InstallApp_HigherThanAvailableMemory_ThrowsException()
        {
            Assert.Throws<InvalidOperationException>(() => device.InstallApp("SA", 60));
        }
        [Test]
        public void FormatDevice_ReturnsDeviceToInitialState()
        {
            device.TakePhoto(5);
            device.InstallApp("SA", 15);
            device.FormatDevice();
            Assert.AreEqual(device.Photos, 0);
            Assert.AreEqual(device.Applications.Count, 0);
            Assert.AreEqual(device.AvailableMemory, 50);
        }
        [Test]
        public void GetDeviceStatus_ReturnsCorrectString()
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendLine($"Memory Capacity: {device.MemoryCapacity} MB, Available Memory: {device.AvailableMemory} MB");
            stringBuilder.AppendLine($"Photos Count: {device.Photos}");
            stringBuilder.AppendLine($"Applications Installed: {string.Join(", ", device.Applications)}");
            string expected = stringBuilder.ToString().TrimEnd();
            string returned = device.GetDeviceStatus();
            Assert.AreEqual(expected, returned);
        }
    }
}