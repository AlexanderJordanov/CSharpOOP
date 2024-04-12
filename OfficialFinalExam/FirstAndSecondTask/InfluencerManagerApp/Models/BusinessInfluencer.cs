using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace InfluencerManagerApp.Models
{
    public class BusinessInfluencer : Influencer
    {
        public BusinessInfluencer(string username, int followers) : base(username, followers, 3)
        {
        }

        public override int CalculateCampaignPrice()
        {
            double price = (Followers * EngagementRate * 0.15);
            int roundedPrice = (int)price;
            return roundedPrice;
        }
    }
}
