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
        public int GetCommentsCount()
        {
            MarketplaceContext context = new MarketplaceContext();

            return context.Comments.Count();
        }
        public int GetRolesCount()
        {
            MarketplaceContext context = new MarketplaceContext();

            return context.Roles.Count();
        }
        public int GetCategoriesCount()
        {
            MarketplaceContext context = new MarketplaceContext();

            return context.Categories.Count();
        }

        public List<Comment> GetCommentsByUser(string userID)
        {
            MarketplaceContext context = new MarketplaceContext();

            return context.Comments.Where(x => x.UserID == userID).OrderByDescending(x => x.TimeStamp).ToList();
        }

    }
}
