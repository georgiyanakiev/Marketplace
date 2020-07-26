using Marketplace.Entities;
using Marketplace.Services;
using Marketplace.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Marketplace.Web.Controllers
{
    public class AuctionsController : Controller
    {
        AuctionsService auctionsService = new AuctionsService();

        CategoriesService categoriesService = new CategoriesService();

        [HttpGet]
        public ActionResult Index()
        {
            AuctionsListingViewModel model = new AuctionsListingViewModel();

            model.PageTitle = "Auctions";
            model.PageDescription = "Auctions Listing Page";

            return View(model);

        }

        public ActionResult Listing()
        {
            AuctionsListingViewModel model = new AuctionsListingViewModel();

            model.Auctions = auctionsService.GetAllAuctions();
            return PartialView(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            CreateAuctionViewModel model = new CreateAuctionViewModel();

            model.Categories = categoriesService.GetAllCategories();

            return PartialView(model);
        }   

        [HttpPost]
        public ActionResult Create(CreateAuctionViewModel model)
        {

            Auction auction = new Auction();
            auction.Title = model.Title;
            auction.CategoryID = model.CategoryID;
            auction.Description = model.Description;
            auction.ActualAmount = model.ActualAmount;
            auction.StartTime = model.StartTime;
            auction.EndTime = model.EndTime;
            //LINQ
            var pictureIDs = model.AuctionPictures
                .Split(new char[] { ',' },StringSplitOptions.RemoveEmptyEntries)
                .Select(ID=> int.Parse(ID)).ToList();


            auction.AuctionPictures = new List<AuctionPicture>();
            auction.AuctionPictures.AddRange(pictureIDs.Select(x=> new AuctionPicture() { PictureID = x }).ToList());

            //foreach (var picID in pictureIDs)
            //{
            //    var auctionPicture = new AuctionPicture();
            //    auctionPicture.PictureID = picID;

            //    auction.AuctionPictures.Add(auctionPicture);
            //}


            auctionsService.SaveAuction(auction);

            return RedirectToAction("Listing");
           
        }

        [HttpGet]
        public ActionResult Edit(int ID)
        {
            
            var auction = auctionsService.GetAuctionByID(ID);

            return PartialView(auction);
        }

        [HttpPost]
        public ActionResult Edit(Auction auction)
        {
            

            auctionsService.UpdateAuction(auction);

            return RedirectToAction("Listing");
        }

        [HttpPost]
        public ActionResult Delete(Auction auction)
        {
           

            auctionsService.DeleteAuction(auction);

            return RedirectToAction("Listing");
        }

        [HttpGet]
        public ActionResult Details(int ID)
        {
            AuctionDetailsViewModel model = new AuctionDetailsViewModel();

            model.Auction = auctionsService.GetAuctionByID(ID);

            model.PageTitle = "Auctions Details: " + model.Auction.Title;
            model.PageDescription = model.Auction.Description.Substring(0, 10);

            return View(model);
        }
    }
}