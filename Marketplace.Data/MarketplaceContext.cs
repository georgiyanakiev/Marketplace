using Marketplace.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.Data
{
    public class MarketplaceContext : DbContext
    {
        public MarketplaceContext() : base("name=MarketplaceConnectionString")
        {
        }


        public DbSet<Auction> Auctions { get; set; }
    }
}
