using Marketplace.Entities;
using Marketplace.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Marketplace.Web.Controllers
{
    public class AuctionsController : Controller
    {
        AuctionsService service = new AuctionsService();

        [HttpGet]
        public ActionResult Index()
        {
            
            var auctions = service.GetAllAuctions();

            if (Request.IsAjaxRequest())
            {
                return PartialView(auctions);
            }
            else
            {
                return View(auctions);
            }

        }

        [HttpGet]
        public ActionResult Create()
        {
            
            return PartialView();
            
        }

        [HttpPost]
        public ActionResult Create(Auction auction)
        {
            

            service.SaveAuction(auction);

            return RedirectToAction("Index");
           

        }

        [HttpGet]
        public ActionResult Edit(int ID)
        {
            

            var auction = service.GetAuctionByID(ID);


            return PartialView(auction);
        }

        [HttpPost]
        public ActionResult Edit(Auction auction)
        {
            

            service.UpdateAuction(auction);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(int ID)
        {
            

            var auction = service.GetAuctionByID(ID);


            return View(auction);
        }

        [HttpPost]
        public ActionResult Delete(Auction auction)
        {
           

            service.DeleteAuction(auction);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Details(int ID)
        {
            
            var auction = service.GetAuctionByID(ID);


            return View(auction);
        }
    }
}