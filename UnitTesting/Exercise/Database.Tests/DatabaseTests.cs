namespace Database.Tests
{
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class DatabaseTests
    {
        private Database _database;
        [SetUp]
        public void SetUp()
        {
            _database = new Database(new int[15]);
        }
        [Test]
        public void Ctor_ShouldSetDataCorrectly()
        {
            Assert.That(_database.Count, Is.EqualTo(15));
        }
        [Test]
        public void Add_ShouldAddElement()
        {
            _database.Add(16);
            Assert.AreEqual(16, _database.Count);
        }
        [Test]
        public void Add_ShouldThrowException()
        {
            _database.Add(16);
            Assert.Throws<InvalidOperationException>(() => _database.Add(17));
        }
        [Test]
        public void Remove_ShouldRemoveElement()
        {
            _database.Remove();
            Assert.AreEqual(_database.Count, 14);
        }
        [Test]
        public void Remove_ShouldThrowException()
        {
            _database = new Database(new int[1]);
            _database.Remove();
            Assert.Throws<InvalidOperationException>(()=> _database.Remove());
        }
        [Test]
        public void Fetch_ShouldCreateArray()
        {
            int[] fetchArray = _database.Fetch();
            Assert.AreEqual(fetchArray.Length, _database.Count);
        }
    }
}
