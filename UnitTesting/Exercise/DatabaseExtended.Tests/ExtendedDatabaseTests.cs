namespace DatabaseExtended.Tests
{
    using ExtendedDatabase;
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class ExtendedDatabaseTests
    {
        private const string ValidUsernameFirstUser = "Errovia123";
        private const long ValidIdFirstUser = 100001;
        private const string ValidUsernameSecondUser = "Sylas97";
        private const long ValidIdSecondUser = 82183;
        private const int InvalidNumberOfPeople = 17;
        private const int MaxNumberOfPeople = 16;
        private Database database;

        [SetUp]
        public void SetUp()
        {
            Person errovia = new Person(ValidIdFirstUser, ValidUsernameFirstUser);
            Person sylas = new Person(ValidIdSecondUser, ValidUsernameSecondUser);
            Person[] persons = new Person[] { errovia, sylas };
            database = new Database(persons);
        }

        [Test]
        public void Ctor_WithEmptyValidParams_CreatesNewInstance()
        {
            Database database = new Database();
            Assert.That(database, Is.Not.Null);
            Assert.That(database.Count, Is.EqualTo(0));
        }
        [Test]
        public void Ctor_WithValidParams_CreatesNewInstance()
        {
            Assert.IsNotNull(database);
            Assert.AreEqual(database.Count, 2);
        }
        [Test]
        public void Ctor_TooManyPeople_ShouldThrowException()
        {
            Person[] tooManyPeople = new Person[17];
            for (int i = 0; i < InvalidNumberOfPeople; i++)
            {
                tooManyPeople[i] = new Person(ValidIdFirstUser + i, $"{ValidUsernameFirstUser}i");
            }
            Assert.Throws<ArgumentException>(() => new Database(tooManyPeople));
        }
        [Test]
        public void Add_ShouldAddPerson()
        {
            Person zilitu = new Person(18291, "Zilituu");
            database.Add(zilitu);
            Assert.AreEqual(database.Count, 3);
        }
        [Test]
        public void Add_FullDatabase_ShouldThrowException()
        {
            for (int i = database.Count; i < MaxNumberOfPeople; i++)
            {
                database.Add(new Person(ValidIdFirstUser + i, $"{ValidUsernameFirstUser + i}"));
            }
            Assert.Throws<InvalidOperationException>(() => database.Add(new Person(20000, "Dimitrichko")));
        }
        [Test]
        public void Add_ExistingUsername_ShouldThrowException()
        {
            Assert.Throws<InvalidOperationException>(() => database.Add(new Person(20000, ValidUsernameFirstUser)));
        }
        [Test]
        public void Add_ExistingId_ShouldThrowException()
        {
            Assert.Throws<InvalidOperationException>(() => database.Add(new Person(ValidIdFirstUser, "Dimitrichko")));
        }
        [Test]
        public void Remove_ShouldRemovePerson()
        {
            database.Remove();
            Assert.AreEqual(database.Count, 1);
        }
        [Test]
        public void Remove_EmptyDatabase_ShouldThrowException()
        {
            database.Remove();
            database.Remove();
            Assert.Throws<InvalidOperationException>(() => database.Remove());
        }
        [Test]
        public void FindByUsername_ShouldReturnPerson()
        {
            Person foundErrovia = database.FindByUsername(ValidUsernameFirstUser);
            Assert.IsNotNull(foundErrovia);
            Assert.AreEqual(foundErrovia.UserName, ValidUsernameFirstUser);
            Assert.AreEqual(foundErrovia.Id, ValidIdFirstUser);
        }

        [Test]
        public void FindByUsername_NoUsernameFound_ShouldThrowException()
        {
            Assert.Throws<InvalidOperationException>(() => database.FindByUsername("Dimitrichko"));
        }
        [Test]
        public void FindByUsername_NullUsername_ShouldThrowException()
        {
            Assert.Throws<ArgumentNullException>(() => database.FindByUsername(null));
        }
        [Test]
        public void FindById_ShouldReturnPerson()
        {
            Person errovia = database.FindById(ValidIdFirstUser);
            Assert.IsNotNull(errovia);
            Assert.AreEqual(errovia.Id, ValidIdFirstUser);
            Assert.AreEqual(errovia.UserName, ValidUsernameFirstUser);
        }
        [Test]
        public void FindById_NoIdFound_ShouldThrowException()
        {
            Assert.Throws<InvalidOperationException>(() => database.FindById(11111111));
        }
        [Test]
        public void FindById_NegativeId_ShouldThrowException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => database.FindById(-1));
        }
    }
}