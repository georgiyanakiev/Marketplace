﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.Entities
{
   public class Bid :BaseEntity
    {
        public int AuctionID { get; set; }
        public virtual Auction Auction { get; set; }

        public string UserID { get; set; }
        public virtual MarketplaceUser User { get; set; }

        public decimal BidAmount { get; set; }
        public DateTime TimeStamp { get; set; }


    }
}
