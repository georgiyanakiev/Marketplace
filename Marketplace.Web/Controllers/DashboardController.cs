
using Marketplace.Entities;
using Marketplace.Services;
using Marketplace.Web.ViewModels;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Marketplace.Web.Controllers
{
    public class DashboardController : Controller
    {
        DashboardService service = new DashboardService();

        private MarketplaceUserManager _userManager;

        public DashboardController()
        {
        }

        public DashboardController(MarketplaceUserManager userManager)
        {
            UserManager = userManager;
        }


        public MarketplaceUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<MarketplaceUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public ActionResult Index()
        {
            DashboardViewModel model = new DashboardViewModel();

            model.Page = Pages.Dashboard;

            model.UserCount = service.GetUserCount();
            model.AuctionsCount = service.GetAuctionCount();
            model.BidsCount = service.GetBidsCount();

            return View(model);
        }
        public ActionResult Users(string roleID, string searchTerm, int? pageNo)
        {
            UsersViewModel model = new UsersViewModel();

            model.PageTitle = "Users";
            model.PageDescription = "Users Listing Page";

            model.RoleID = roleID;
            model.SearchTerm = searchTerm;
            model.PageNo = pageNo;

            model.Roles = new List<IdentityRole>();

            return View(model);
        }

        public ActionResult UsersListing(string roleID, string searchTerm, int? pageNo)
        {

            var pageSize = 3;

            UsersListingViewModel model = new UsersListingViewModel();

            model.Users = UserManager.Users.ToList();

            model.RoleID = roleID;
            model.SearchTerm = searchTerm;
            model.PageNo = pageNo;

            model.Pager = new Pager(10, pageNo, pageSize);

            return PartialView(model);
        }

    }
}