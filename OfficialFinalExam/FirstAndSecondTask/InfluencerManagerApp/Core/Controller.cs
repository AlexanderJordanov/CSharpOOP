using InfluencerManagerApp.Core.Contracts;
using InfluencerManagerApp.Models;
using InfluencerManagerApp.Models.Contracts;
using InfluencerManagerApp.Repositories;
using InfluencerManagerApp.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfluencerManagerApp.Core
{
    public class Controller : IController
    {
        private IRepository<IInfluencer> influencers;
        private IRepository<ICampaign> campaigns;
        public Controller()
        {
            influencers = new InfluencerRepository();
            campaigns = new CampaignRepository();
        }


        public string RegisterInfluencer(string typeName, string username, int followers)
        {
            if (typeName != "BusinessInfluencer" &&  typeName != "FashionInfluencer" && typeName != "BloggerInfluencer")
            {
                return $"{typeName} has not passed validation.";
            }
            if (influencers.FindByName(username) != null) 
            {
                return $"{username} is already registered in {influencers.GetType().Name}.";
            }
            IInfluencer influencer = null;
            if (typeName == "BusinessInfluencer")
            {
                influencer = new BusinessInfluencer(username,followers);
            }
            else if (typeName == "FashionInfluencer")
            {
                influencer = new FashionInfluencer(username, followers);
            }
            else if (typeName == "BloggerInfluencer")
            {
                influencer = new BloggerInfluencer(username, followers);
            }
            influencers.AddModel(influencer);
            return $"{username} registered successfully to the application.";
        }


        public string BeginCampaign(string typeName, string brand)
        {
            if (typeName != "ProductCampaign" && typeName != "ServiceCampaign")
            {
                return $"{typeName} is not a valid campaign in the application.";
            }
            if (campaigns.FindByName(brand) != null)
            {
                return $"{brand} campaign cannot be duplicated.";
            }
            ICampaign campaign = null;
            if (typeName == "ProductCampaign")
            {
                campaign = new ProductCampaign(brand);
            }
            else if (typeName == "ServiceCampaign")
            {
                campaign = new ServiceCampaign(brand);
            }
            campaigns.AddModel(campaign);
            return $"{brand} started a {typeName}.";
        }


        public string AttractInfluencer(string brand, string username)
        {
            IInfluencer influencer = influencers.FindByName(username);
            if (influencer == null)
            {
                return $"{influencers.GetType().Name} has no {username} registered in the application.";
            }
            ICampaign campaign = campaigns.FindByName(brand);
            if (campaign == null)
            {
                return $"There is no campaign from {brand} in the application.";
            }
            if (influencer.Participations.Contains(brand))
            {
                return $"{username} is already engaged for the {brand} campaign.";
            }
            if (campaign.GetType().Name == "ProductCampaign" && influencer.GetType().Name == "BloggerInfluencer")
            {
                return $"{username} is not eligible for the {brand} campaign.";
            }
            if (campaign.GetType().Name == "ServiceCampaign" && influencer.GetType().Name == "FashionInfluencer")
            {
                return $"{username} is not eligible for the {brand} campaign.";
            }
            if (campaign.Budget < influencer.CalculateCampaignPrice())
            {
                return $"The budget for {brand} is insufficient to engage {username}.";
            }
            influencer.EarnFee(influencer.CalculateCampaignPrice());
            influencer.EnrollCampaign(brand);
            campaign.Engage(influencer);
            return $"{username} has been successfully attracted to the {brand} campaign.";
        }


        public string FundCampaign(string brand, double amount)
        {
            ICampaign campaign = campaigns.FindByName(brand);
            if (campaign == null)
            {
                return "Trying to fund an invalid campaign.";
            }
            if (amount <= 0) 
            {
                return "Funding amount must be greater than zero.";
            }
            campaign.Gain(amount);
            return $"{brand} campaign has been successfully funded with {amount} $";
        }


        public string CloseCampaign(string brand)
        {
            ICampaign campaign = campaigns.FindByName(brand);
            if (campaign == null)
            {
                return "Trying to close an invalid campaign.";
            }
            if (campaign.Budget <= 10000)
            {
                return $"{brand} campaign cannot be closed as it has not met its financial targets.";
            }
            foreach(var contributor in campaign.Contributors)
            {
                IInfluencer influencer = influencers.FindByName(contributor);
                influencer.EarnFee(2000);
                influencer.EndParticipation(brand);
            }
            campaigns.RemoveModel(campaign);
            return $"{brand} campaign has reached its target.";
        }


        public string ConcludeAppContract(string username)
        {
            IInfluencer influencer = influencers.FindByName(username);
            if (influencer == null)
            {
                return $"{username} has still not signed a contract.";
            }
            if (influencer.Participations.Count > 0)
            {
                return $"{username} cannot conclude the contract while enrolled in active campaigns.";
            }
            influencers.RemoveModel(influencer);
            return $"{username} concluded their contract.";
        }

        public string ApplicationReport()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var influencer in influencers.Models.OrderByDescending(i => i.Income).ThenByDescending(i => i.Followers))
            {
                sb.AppendLine(influencer.ToString());
                if (influencer.Participations.Count > 0)
                {
                    sb.AppendLine("Active Campaigns:");
                    foreach (var campBrand in influencer.Participations.OrderBy(p => p))
                    {
                        ICampaign campaign = campaigns.FindByName(campBrand);
                        sb.Append("--");
                        sb.Append(campaign.ToString());
                        sb.AppendLine();
                    }
                    
                }             
            }
            return sb.ToString().TrimEnd();
        }       
    }
}
