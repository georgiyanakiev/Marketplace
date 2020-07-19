using Marketplace.Data;
using Marketplace.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.Services
{
   public class AuctionsService
    {
        public List<Auction> GetAllAuctions()
        {
            MarketplaceContext context = new MarketplaceContext();

            return context.Auctions.ToList();
        }
        public List<Auction> GetPromotedAuctions()
        {
            MarketplaceContext context = new MarketplaceContext();

            return context.Auctions.Take(4).ToList();
        }
        public Auction GetAuctionByID(int ID)
        {
            MarketplaceContext context = new MarketplaceContext();

            return context.Auctions.Find(ID);
        }
        public void SaveAuction(Auction auction)
        {
            MarketplaceContext context = new MarketplaceContext();

            context.Auctions.Add(auction);

            context.SaveChanges();
        }

        public void UpdateAuction(Auction auction)
        {
            MarketplaceContext context = new MarketplaceContext();

            context.Entry(auction).State = System.Data.Entity.EntityState.Modified;

            context.SaveChanges();
        }

        public void DeleteAuction(Auction auction)
        {
            MarketplaceContext context = new MarketplaceContext();

            context.Entry(auction).State = System.Data.Entity.EntityState.Deleted;

             context.SaveChanges();
        }
    }
}
