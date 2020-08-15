using Marketplace.Services;
using Marketplace.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Marketplace.Web.Controllers
{
    public class HomeController : Controller
    {

        BidsServices service = new BidsServices();
        public ActionResult Index()
        {
            AuctionsViewModel vModel = new AuctionsViewModel();

            vModel.PageTitle = "Marketplace";
            vModel.PageDescription = "Auctions Page";


            vModel.AllAuctions = service.GetAllAuctions();

            vModel.PromotedAuctions = service.GetPromotedAuctions();

           
            return View(vModel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}