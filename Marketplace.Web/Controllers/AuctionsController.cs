﻿using Marketplace.Entities;
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
       
        AuctionsServices auctionsService = new AuctionsServices();
        CategoriesService categoriesService = new CategoriesService();
        SharedService sharedService = new SharedService();


        public ActionResult Index(int? categoryID, string searchTerm, int? pageNo)
        {
            AuctionsListingViewModel model = new AuctionsListingViewModel();

            model.Page = Pages.Auctions;
            model.PageTitle = "Auctions";
            model.PageDescription = "Auctions Listing Page";

            model.CategoryID = categoryID;
            model.SearchTerm = searchTerm;
            model.PageNo = pageNo ?? 1;

            model.Categories = categoriesService.GetAllCategories();

            return View(model);

        }

        public ActionResult Listing(int? categoryID, string searchTerm, int? pageNo)
        {

            var pageSize = 3;

            AuctionsListingViewModel model = new AuctionsListingViewModel();

            model.Auctions = auctionsService.SearchAuctions(categoryID, searchTerm, pageNo, pageSize);

            var totalAuctions = auctionsService.GetAuctionCount(categoryID, searchTerm);


            model.Pager = new Pager(totalAuctions,pageNo, pageSize);

            return PartialView(model);
        }
        //public ActionResult SearchAuctions(int? categoryID, string searchTerm, int? pageNo)
        //{

        //    var pageSize = 5;

        //    AuctionsListingViewModel model = new AuctionsListingViewModel();

        //    model.Auctions = auctionsService.SearchAuctions(categoryID, searchTerm, pageNo, pageSize);

        //    var totalAuctions = auctionsService.GetAuctionCount(categoryID, searchTerm);


        //    model.Pager = new Pager(totalAuctions, pageNo, pageSize);

        //    return PartialView(model);
        //}

        [HttpGet]
        public ActionResult Create()
        {
            CreateAuctionViewModel model = new CreateAuctionViewModel();

            model.Categories = categoriesService.GetAllCategories();

            return PartialView(model);
        }   

        [HttpPost]
        public JsonResult Create(CreateAuctionViewModel model)
        {
            JsonResult result = new JsonResult();


            if (ModelState.IsValid)
            {

                Auction auction = new Auction();

                auction.Title = model.Title;
                auction.CategoryID = model.CategoryID;
                auction.Summary = model.Summary;
                auction.Description = model.Description;
                auction.ActualAmount = model.ActualAmount;
                auction.StartTime = model.StartTime;
                auction.EndTime = model.EndTime;

                //check if we have AuctionPictureIds posted back from form
                if (!string.IsNullOrEmpty(model.AuctionPictures))
                {

                    //LINQ
                    var pictureIDs = model.AuctionPictures
                        .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                        .Select(ID => int.Parse(ID)).ToList();


                    auction.AuctionPictures = new List<AuctionPicture>();
                    auction.AuctionPictures.AddRange(pictureIDs.Select(x => new AuctionPicture() { PictureID = x }).ToList());
                }

                auctionsService.SaveAuction(auction);

                result.Data = new { Success = true };

            }
            else
            {
                result.Data = new { Success = false, Error = "Unable to save Auction. Please enter valid values." };
            }

            return result;
        }

        [HttpGet]
        public ActionResult Edit(int ID)
        {
            CreateAuctionViewModel model = new CreateAuctionViewModel();

            var auction = auctionsService.GetAuctionByID(ID);

            model.ID = auction.ID;
            model.Title = auction.Title;
            model.CategoryID = auction.CategoryID;
            model.Summary = auction.Summary;
            model.Description = auction.Description;
            model.ActualAmount = auction.ActualAmount;
            model.StartTime = auction.StartTime;
            model.EndTime = auction.EndTime;


            model.Categories = categoriesService.GetAllCategories();
            model.AuctionPicturesList = auction.AuctionPictures;


            return PartialView(model);
           
        }

        [HttpPost]
        public ActionResult Edit(CreateAuctionViewModel model)
        {

            Auction auction = new Auction();
            auction.ID = model.ID;
            auction.Title = model.Title;
            auction.CategoryID = model.CategoryID;
            auction.Summary = model.Summary;
            auction.Description = model.Description;
            auction.ActualAmount = model.ActualAmount;
            auction.StartTime = model.StartTime;
            auction.EndTime = model.EndTime;

            //check if we have AuctionPictureIds posted back from form
            if (!string.IsNullOrEmpty(model.AuctionPictures))
            {

                //LINQ
                var pictureIDs = model.AuctionPictures
                    .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(ID => int.Parse(ID)).ToList();


                auction.AuctionPictures = new List<AuctionPicture>();
                auction.AuctionPictures.AddRange(pictureIDs.Select(x => new AuctionPicture() { AuctionID = auction.ID, PictureID = x }).ToList());
            }
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

            if (model.Auction == null) return HttpNotFound();


            model.BidsAmount = model.Auction.ActualAmount + model.Auction.Bids.Sum(x => x.BidAmount);
            
            var latestBidder = model.Auction.Bids.OrderByDescending(x => x.TimeStamp).FirstOrDefault();

            model.LatestBider = latestBidder != null ? latestBidder.User : null;

            model.PageTitle = "Auctions Details: " + model.Auction.Title;
            model.PageDescription = model.Auction.Summary;


            model.EntityID = (int)EntityEnums.Auction;
            model.RecordID = model.Auction.ID;
            model.Comments = sharedService.GetComments(model.EntityID, model.RecordID);

            return View(model);
        }

        public JsonResult UpdateAuctions(string AuctionIDs)
        {
            JsonResult result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;


            result.Data = auctionsService.GetAllAuctions().Select(x => new { ID = x.ID, BidAmount = x.ActualAmount + x.BidAmount });

            return result;
        }
    }
}