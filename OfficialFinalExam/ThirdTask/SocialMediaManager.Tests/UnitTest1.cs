using System;
using NUnit.Framework;
using System.Linq;
using System.Collections.Generic;

namespace SocialMediaManager.Tests
{
    public class Tests
    {
        private Influencer influencer;
        private InfluencerRepository influencerRepository;
        [SetUp]
        public void Setup()
        {
            influencer = new Influencer("Tafa", 5);
            influencerRepository = new InfluencerRepository();
        }

        [Test]
        public void Ctor_InfluencerDataSetsCorrectly()
        {
            Assert.IsNotNull(influencer);
            Assert.AreEqual(influencer.Username, "Tafa");
            Assert.AreEqual(influencer.Followers, 5);
        }
        [Test]
        public void Ctor_InfluencerRepositorySetsDataCorrectly()
        {
            Assert.IsNotNull(influencerRepository);
            Assert.AreEqual(influencerRepository.Influencers.Count, 0);
        }
        [Test]
        public void RegisterInfluencer_NullInfluencer_ShouldThrowException()
        {
            influencer = null;
            Assert.Throws<ArgumentNullException>(() => influencerRepository.RegisterInfluencer(influencer));
        }

        [Test]
        public void RegisterInfluencer_HappyPath_ShouldAddInfluencer()
        {
            string expected = "Successfully added influencer Tafa with 5";
            string returned = influencerRepository.RegisterInfluencer(influencer);
            Assert.AreEqual(influencerRepository.Influencers.Count, 1);
            Assert.AreEqual(returned, expected);
            Influencer secondInfluencer = new Influencer("Sasho", 20);
            influencerRepository.RegisterInfluencer(secondInfluencer);
            Assert.AreEqual(influencerRepository.Influencers.Count, 2);
        }

        [Test]
        public void RegisterInfluencer_ExistingInfluencer_ShouldThrowException()
        {
            influencerRepository.RegisterInfluencer(influencer);
            Assert.Throws<InvalidOperationException>(() => influencerRepository.RegisterInfluencer(influencer));
        }
        [Test]
        public void RemoveInfluencer_NullInfluencer_ShouldThrowException()
        {
            influencerRepository.RegisterInfluencer(influencer);
            Assert.Throws<ArgumentNullException>(() => influencerRepository.RegisterInfluencer(null));
            Assert.AreEqual(influencerRepository.Influencers.Count, 1);
        }
        [Test]
        public void RemoveInfluencer_WhiteSpaceInfluencer_ShouldThrowException()
        {
            influencerRepository.RegisterInfluencer(influencer);
            Assert.Throws<ArgumentNullException>(() => influencerRepository.RemoveInfluencer("  "));
            Assert.AreEqual(influencerRepository.Influencers.Count, 1);
        }
        [Test]
        public void RemoveInfluencer_HappyPath_ShouldRemoveInfluencer()
        {
            influencerRepository.RegisterInfluencer(influencer);
            Assert.AreEqual(influencerRepository.Influencers.Count, 1);
            bool returned = influencerRepository.RemoveInfluencer("Tafa");
            bool expected = true;
            Assert.AreEqual(expected, returned);
            Assert.AreEqual(influencerRepository.Influencers.Count, 0);
        }
        [Test]
        public void RemoveInfluencer_NotExistingInfluencer_ShouldReturnFalse()
        {
            influencerRepository.RegisterInfluencer(influencer);
            Assert.AreEqual(influencerRepository.Influencers.Count, 1);
            bool returned = influencerRepository.RemoveInfluencer("Sasho");
            bool expected = false;
            Assert.AreEqual(expected, returned);
            Assert.AreEqual(influencerRepository.Influencers.Count, 1);
        }
        [Test]
        public void GetInfluencerWithMostFollowers_ReturnsInfluencer()
        {
            Influencer secondInfluencer = new Influencer("Sasho",20);
            influencerRepository.RegisterInfluencer(influencer);
            influencerRepository.RegisterInfluencer(secondInfluencer);
            var returned = influencerRepository.GetInfluencerWithMostFollowers();
            Assert.AreEqual(returned, secondInfluencer);
            Assert.AreEqual(influencerRepository.Influencers.Count, 2);
        }
        [Test]
        public void GetInfluencer_ReturnsInfluencer()
        {
            influencerRepository.RegisterInfluencer(influencer);
            var returned = influencerRepository.GetInfluencer("Tafa");
            Assert.AreEqual(returned, influencer);
            Assert.AreEqual(influencerRepository.Influencers.Count, 1);
        }
    }
}