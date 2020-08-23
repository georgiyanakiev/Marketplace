using Marketplace.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Marketplace.Web.ViewModels
{
    public class AuctionsListingViewModel : PageViewModel
    {
        public List<Auction> Auctions { get; set; }
        public int? CategoryID { get; set; }
        public string SearchTerm { get; set; }

        public Pager Pager { get; set; }
        public int? PageNo { get;  set; }
        public List<Category> Categories { get; set; }
    }

    public class AuctionsViewModel : PageViewModel
    {
        public List<Auction> AllAuctions { get; set; }
        public List<Auction> PromotedAuctions { get; set; }
    }

    public class AuctionDetailsViewModel : PageViewModel
    {
        public Auction Auction { get; set; }

        public List<Comment> Comments { get; set; }
        public decimal BidsAmount { get; set; }
        public MarketplaceUser LatestBider { get; set; }
        public int EntityID { get; internal set; }
    }

    public class CreateAuctionViewModel : PageViewModel
    {
        
        public int ID { get; set; }
        public int CategoryID { get; set; }

        [Required]
        [MinLength(15, ErrorMessage = "Minimum length should be 15 characters.")]
        [MaxLength(150)]
        public string Title { get; set; }
        public string Description { get; set; }

        [Required]
        [Range(1, 1000000, ErrorMessage = "Actual amount must be within 1 - 1000000")]
        public decimal ActualAmount { get; set; }


        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }


        public string AuctionPictures { get; set; }

        public List<Category> Categories { get; set; }
        
        
        public List<AuctionPicture> AuctionPicturesList { get; set; }
    }
}