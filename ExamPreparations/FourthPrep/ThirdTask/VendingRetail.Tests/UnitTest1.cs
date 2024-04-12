using NUnit.Framework;

namespace VendingRetail.Tests
{
    public class Tests
    {
        private CoffeeMat coffeeMat;
        
        [SetUp]
        public void Setup()
        {
            coffeeMat = new CoffeeMat(1000,2);
        }

        [Test]
        public void Ctor_SetsDataCorrectly()
        {
            Assert.IsNotNull(coffeeMat);
            Assert.AreEqual(coffeeMat.WaterCapacity, 1000);
            Assert.AreEqual(coffeeMat.ButtonsCount, 2);
            Assert.AreEqual(coffeeMat.Income, 0);
        }
        [Test]
        public void FillWaterTank_HappyPath()
        {
            string expected = "Water tank is filled with 1000ml";
            string returned = coffeeMat.FillWaterTank();
            Assert.AreEqual(expected, returned);
        }
        [Test]
        public void FillWaterTank_FullTank()
        {
            string expected = "Water tank is already full!";
            coffeeMat.FillWaterTank();
            string returned = coffeeMat.FillWaterTank();
            Assert.AreEqual(expected,returned);
        }
        [Test]
        public void AddDrink_HappyPath()
        {       
            bool expected = true;
            bool returned = coffeeMat.AddDrink("Monster", 12.5);
            Assert.AreEqual(expected,returned);
            returned = coffeeMat.AddDrink("Shark", 2.5);
            Assert.AreEqual(expected, returned);
        }
        [Test]
        public void AddDrink_ExistingDrink()
        {
            bool expected = false;
            coffeeMat.AddDrink("Monster", 12.5);
            bool returned = coffeeMat.AddDrink("Monster", 12.5);
            Assert.AreEqual(expected, returned);
        }
        [Test]
        public void AddDrink_DrinkCountReached()
        {
            bool expected = false;
            coffeeMat.AddDrink("Monster", 12.5);
            coffeeMat.AddDrink("Hell", 2.5);
            bool returned = coffeeMat.AddDrink("Shark", 15.5);
            Assert.AreEqual(expected, returned);
        }
        [Test]
        public void BuyDrink_LowTankLevel()
        {
            string expected = "CoffeeMat is out of water!";
            coffeeMat.AddDrink("Monster", 12.5);
            string returned = coffeeMat.BuyDrink("Monster");
            Assert.AreEqual(expected, returned);
            Assert.AreEqual(coffeeMat.Income, 0);
        }
        [Test]
        public void BuyDrink_NotAvailableDrink()
        {
            coffeeMat.FillWaterTank();
            string expected = "Monster is not available!";
            string returned = coffeeMat.BuyDrink("Monster");
            Assert.AreEqual(expected, returned);
            Assert.AreEqual(coffeeMat.Income, 0);
        }
        [Test]
        public void BuyDrink_BillToPay()
        {
            coffeeMat.FillWaterTank();
            coffeeMat.AddDrink("Monster", 12.5);
            string expected = "Your bill is 12.50$";
            string returned = coffeeMat.BuyDrink("Monster");
            Assert.AreEqual(expected, returned);
            Assert.AreEqual(coffeeMat.Income, 12.5);
            coffeeMat.BuyDrink("Monster");
            Assert.AreEqual(coffeeMat.Income, 25);
        }
        [Test]
        public void BuyDrink_ReducesWaterTank()
        {
            coffeeMat = new CoffeeMat(100, 1);
            coffeeMat.FillWaterTank();
            coffeeMat.AddDrink("Coffee", 100);
            coffeeMat.BuyDrink("Coffee");
            string expected = "CoffeeMat is out of water!";
            string returned = coffeeMat.BuyDrink("Coffee");
            Assert.AreEqual(expected,returned);
        }
        [Test]
        public void CollectIncome()
        {
            coffeeMat.FillWaterTank();
            coffeeMat.AddDrink("Monster", 12.5);
            coffeeMat.AddDrink("Shark", 12.5);
            double expected = 25;
            coffeeMat.BuyDrink("Monster");
            coffeeMat.BuyDrink("Shark");
            double returned = coffeeMat.CollectIncome();
            Assert.AreEqual(expected, returned);
            Assert.AreEqual(coffeeMat.Income, 0);
        }
    }
}