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
        public List<Auction> SearchAuctions(int? categoryID, string searchTerm, int? pageNo, int pageSize)
        {
            MarketplaceContext context = new MarketplaceContext();

            var auctions = context.Auctions.AsQueryable();

            if(categoryID.HasValue && categoryID.Value > 0)
            {
                auctions = auctions.Where(x => x.CategoryID == categoryID.Value);
            }

            if (!string.IsNullOrEmpty(searchTerm))
            {
                auctions = auctions.Where(x => x.Title.ToLower().Contains(searchTerm.ToLower()));
            }

            pageNo = pageNo ?? 1;
            //pageNo = pageNo.HasValue ? pageNo.Value : 1;

            var skipCount = (pageNo.Value - 1) * pageSize;

            return auctions.OrderByDescending(x =>x.CategoryID).Skip(skipCount).Take(pageSize).ToList();
        }

        public int GetAuctionCount(int? categoryID, string searchTerm)
        {
            MarketplaceContext context = new MarketplaceContext();

            var auctions = context.Auctions.AsQueryable();

            if (categoryID.HasValue && categoryID.Value > 0)
            {
                auctions = auctions.Where(x => x.CategoryID == categoryID.Value);
            }

            if (!string.IsNullOrEmpty(searchTerm))
            {
                auctions = auctions.Where(x => x.Title.ToLower().Contains(searchTerm.ToLower()));
            }

            return auctions.Count();
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

            var existingAuction = context.Auctions.Find(auction.ID);

            context.AuctionPictures.RemoveRange(existingAuction.AuctionPictures);

            context.Entry(existingAuction).CurrentValues.SetValues(auction);

            context.AuctionPictures.AddRange(auction.AuctionPictures);

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
