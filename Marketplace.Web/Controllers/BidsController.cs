using Marketplace.Entities;
using Marketplace.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Marketplace.Web.Controllers
{   

    [Authorize]
    public class BidsController : Controller
    {

        BidsService service = new BidsService();

       [HttpPost]
        public JsonResult Bid(int ID)
        {
            JsonResult result = new JsonResult();
            
            if(User.Identity.IsAuthenticated)
            {
                Bid bid = new Bid();

                bid.AuctionID = ID;
                bid.UserID = User.Identity.GetUserId();
                bid.TimeStamp = DateTime.Now;
                bid.BidAmount = 10;

                var bidResult = service.AddBid(bid);

                if(bidResult)
                {
                    result.Data = new { Success = true };
                }
                else
                {
                    result.Data = new { Success = false, Message = "Unable to add bid to auction." };
                }
            }
            else
            {
                result.Data = new { Success = false, Message = "User needs to login for bids." };
            }
           


            return result;
        }
    }
}