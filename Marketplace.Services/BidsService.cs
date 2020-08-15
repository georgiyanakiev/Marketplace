using Marketplace.Data;
using Marketplace.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.Services
{
   public class BidsService
    {
        public bool AddBid(Bid bid)
        {
            MarketplaceContext context = new MarketplaceContext();

            context.Bids.Add(bid);
            return context.SaveChanges() > 0;
        }
        
    }
}
