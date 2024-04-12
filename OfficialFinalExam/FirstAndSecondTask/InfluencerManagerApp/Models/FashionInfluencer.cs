using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfluencerManagerApp.Models
{
    public class FashionInfluencer : Influencer
    {
        public FashionInfluencer(string username, int followers) : base(username, followers, 4)
        {
        }

        public override int CalculateCampaignPrice()
        {
            double price = (Followers * EngagementRate * 0.1);
            int roundedPrice = (int)price;
            return roundedPrice;
        }
    }
}
