using Marketplace.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Marketplace.Web.ViewModels
{
    public class AuctionsViewModel : PageViewModel
    {
        public List<Auction> AllAuctions { get; set; }
        public List<Auction> PromotedAuctions { get; set; }
    }
}