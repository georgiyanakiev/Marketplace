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

        public DbSet<AuctionPicture> AuctionPictures { get; set; }
        public DbSet<Auction> Auctions { get; set; }
        public DbSet<Picture> Pictures { get; set; }
        public DbSet<Category> Categories { get; set; }

    }
}
