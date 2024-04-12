using InfluencerManagerApp.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfluencerManagerApp.Models
{
    public abstract class Campaign : ICampaign
    {
        private string brand;
        private List<string> contributors;

        protected Campaign(string brand, double budget)
        {
            Brand = brand;
            Budget = budget;
            contributors = new List<string>();
        }

        public string Brand
        {
            get => brand;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Brand is required.");
                }
                brand = value;
            }
        }

        public double Budget {get; private set; }

        public IReadOnlyCollection<string> Contributors => contributors.AsReadOnly();

        public void Engage(IInfluencer influencer)
        {
            contributors.Add(influencer.Username);
            this.Budget -= influencer.CalculateCampaignPrice();
        }

        public void Gain(double amount)
        {
            Budget += amount;
        }
        public override string ToString()
        {
            return $"{this.GetType().Name} - Brand: {Brand}, Budget: {Budget}, Contributors: {contributors.Count}";
        }
    }
}
