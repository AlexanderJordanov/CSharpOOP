namespace Railway.Tests
{
    using NUnit.Framework;
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Security.Cryptography.X509Certificates;

    public class Tests
    {
        private RailwayStation station;
        [SetUp]
        public void Setup()
        {
            station = new RailwayStation("station");
        }

        [Test]
        public void Ctor_SetsDataCorrectly()
        {
            Assert.AreEqual(station.Name, "station");
            Assert.IsNotNull(station.DepartureTrains);
            Assert.IsNotNull(station.ArrivalTrains);
            Assert.AreEqual(station.ArrivalTrains.Count, 0);
            Assert.AreEqual(station.DepartureTrains.Count, 0);
        }
        [TestCase(null)]
        [TestCase(" ")]
        public void Ctor_IncorrectName_ShouldThrowException(string name)
        {
            Assert.Throws<ArgumentException>(() => station = new RailwayStation(name));
        }
        [Test]
        public void NewArrivalOnBoard_ShouldAddTrain()
        {
            string trainInfo = "sofiq-varna";
            station.NewArrivalOnBoard(trainInfo);
            Assert.AreEqual(station.ArrivalTrains.Count, 1);
            Assert.AreEqual(station.ArrivalTrains.Peek(), trainInfo);
        }
        [Test]
        public void TrainHasArrived_AddsTrainToDeparture()
        {
            string trainInfo = "sofiq-varna";
            station.NewArrivalOnBoard(trainInfo);
            string expectedMessage = $"{trainInfo} is on the platform and will leave in 5 minutes.";
            string returnedMessage = station.TrainHasArrived(trainInfo);
            Assert.AreEqual(expectedMessage, returnedMessage);
            Assert.AreEqual(station.ArrivalTrains.Count, 0);
            Assert.AreEqual(station.DepartureTrains.Count, 1);
        }
        [Test]
        public void TrainHasArrived_NotLastTrain_ShouldNotRemoveNorAdd()
        {
            string firstTrain = "sofiq-varna";
            string secondTrain = "sofiq-burgas";
            station.NewArrivalOnBoard(firstTrain);
            station.NewArrivalOnBoard(secondTrain);
            string expectedMessage = $"There are other trains to arrive before {secondTrain}.";
            string returnedMessage = station.TrainHasArrived(secondTrain);
            Assert.AreEqual(expectedMessage, returnedMessage);
            Assert.AreEqual(station.ArrivalTrains.Count, 2);
        }
        [Test]
        public void TrainHasLeft_ShouldRemoveTrain()
        {
            string train = "sofiq-varna";
            station.NewArrivalOnBoard(train);
            station.TrainHasArrived(train);
            bool expected = true;
            bool returned = station.TrainHasLeft(train);
            Assert.AreEqual(expected, returned);
            Assert.AreEqual(station.DepartureTrains.Count, 0);
        }
        [Test]
        public void TrainHasLeft_ShouldRemoveNotTrain()
        {
            string firstTrain = "sofiq-varna";
            string secondTrain = "sofiq-burgas";
            station.NewArrivalOnBoard(firstTrain);
            station.NewArrivalOnBoard(secondTrain);
            station.TrainHasArrived(firstTrain);
            station.TrainHasArrived(secondTrain);
            bool expected = false;
            bool returned = station.TrainHasLeft(secondTrain);
            Assert.AreEqual(expected,returned);
            Assert.AreEqual(station.DepartureTrains.Count, 2);
        }
    }
}