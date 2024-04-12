namespace Television.Tests
{
    using System;
    using System.Diagnostics;
    using NUnit.Framework;
    public class Tests
    {
        private TelevisionDevice tv;
        [SetUp]
        public void Setup()
        {
            tv = new TelevisionDevice("Samsung", 129.90, 40, 20);
        }

        [Test]
        public void Ctor_SetsDataCorrectly()
        {
            Assert.IsNotNull(tv);
            Assert.AreEqual(tv.Brand, "Samsung");
            Assert.AreEqual(tv.Price, 129.90);
            Assert.AreEqual(tv.ScreenWidth, 40);
            Assert.AreEqual(tv.ScreenHeigth, 20);
            Assert.AreEqual(tv.IsMuted, false);
            Assert.AreEqual(tv.CurrentChannel, 0);
        }
        [Test]
        public void SwitchOn_SwitchesTheTvOnAndOff()
        {
            string expected = $"Cahnnel {tv.CurrentChannel} - Volume {tv.Volume} - Sound On";
            string returned = tv.SwitchOn();
            Assert.AreEqual(expected, returned);
            tv.MuteDevice();
            expected = $"Cahnnel {tv.CurrentChannel} - Volume {tv.Volume} - Sound Off";
            returned = tv.SwitchOn();
            Assert.AreEqual (expected, returned);
        }
        [Test]
        public void ChangeChannel_ShouldChangeChannelCorrectly()
        {
            tv.ChangeChannel(5);
            Assert.AreEqual(tv.CurrentChannel, 5);
        }
        [Test]
        public void ChangeChannel_NegativeChannel_ShouldThrowException()
        {
            Assert.Throws<ArgumentException>(() => tv.ChangeChannel(-1));
        }
        [Test]
        public void VolumeChange_Up_ShouldIncreaseVolumeCorrectly()
        {
            string expected = $"Volume: {5 + 13}";
            string returned = tv.VolumeChange("UP", 5);
            Assert.AreEqual(expected, returned);
        }
        [Test]
        public void VolumeChange_UpNegativeUnit_ShouldIncreaseVolumeCorrectly()
        {
            string expected = $"Volume: {5 + 13}";
            string returned = tv.VolumeChange("UP", -5);
            Assert.AreEqual(expected, returned);
        }
        [Test]
        public void VolumeChange_UpHigherThan100_ShouldIncreaseVolumeCorrectly()
        {
            string expected = $"Volume: {100}";
            string returned = tv.VolumeChange("UP", 105);
            Assert.AreEqual(expected, returned);
        }
        [Test]
        public void VolumeChange_Down_ShouldLowerVolumeCorrectly()
        {
            string expected = $"Volume: {8}";
            string returned = tv.VolumeChange("DOWN", 5);
            Assert.AreEqual(expected, returned);
        }
        [Test]
        public void VolumeChange_DownNegativeUnit_ShouldLowerVolumeCorrectly()
        {
            string expected = $"Volume: {8}";
            string returned = tv.VolumeChange("DOWN", -5);
            Assert.AreEqual(expected, returned);
        }
        [Test]
        public void VolumeChange_DownLowerThan0_ShouldLowerVolumeCorrectly()
        {
            string expected = $"Volume: {0}";
            string returned = tv.VolumeChange("DOWN", 20);
            Assert.AreEqual(expected, returned);
        }
        [Test]
        public void MuteDevice_ShouldChangeIsMuted()
        {
            tv.MuteDevice();
            Assert.AreEqual(tv.IsMuted, true);
            tv.MuteDevice();
            Assert.AreEqual(tv.IsMuted, false);
        }
        [Test]
        public void ToString_ShouldReturnCorrectString()
        {
            string expected = $"TV Device: {tv.Brand}, Screen Resolution: {tv.ScreenWidth}x{tv.ScreenHeigth}, Price {tv.Price}$";
            string returned = tv.ToString();
            Assert.AreEqual(expected, returned);
        }
    }
}