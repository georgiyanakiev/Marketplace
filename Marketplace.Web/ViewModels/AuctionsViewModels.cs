using Marketplace.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Marketplace.Web.ViewModels
{
    public class AuctionsListingViewModel : PageViewModel
    {
        public List<Auction> Auctions { get; set; }
    }

    public class AuctionsViewModel : PageViewModel
    {
        public List<Auction> AllAuctions { get; set; }
        public List<Auction> PromotedAuctions { get; set; }
    }

    public class AuctionDetailsViewModel : PageViewModel
    {
        public Auction Auction { get; set; }
        
    }

    public class CreateAuctionViewModel : PageViewModel
    {
        
        public int ID { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public decimal ActualAmount { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string AuctionPictures { get; set; }

        public List<Category> Categories { get; set; }
        public int CategoryID { get; set; }
        public List<AuctionPicture> AuctionPicturesList { get; set; }
    }
}