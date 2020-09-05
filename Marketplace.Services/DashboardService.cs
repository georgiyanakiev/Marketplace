using Marketplace.Data;
using Marketplace.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.Services
{
   public class DashboardService
    {
        public int GetUserCount()
        {
            MarketplaceContext context = new MarketplaceContext();

            return context.Users.Count();
        }
        public int GetAuctionCount()
        {
            MarketplaceContext context = new MarketplaceContext();

            return context.Auctions.Count();
        }
        public int GetBidsCount()
        {
            MarketplaceContext context = new MarketplaceContext();

            return context.Bids.Count();
        }
    }
}
