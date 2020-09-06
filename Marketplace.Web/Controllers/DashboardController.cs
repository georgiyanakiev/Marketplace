
using Marketplace.Entities;
using Marketplace.Services;
using Marketplace.Web.ViewModels;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Marketplace.Web.Controllers
{
    public class DashboardController : Controller
    {
        DashboardService service = new DashboardService();

        private MarketplaceUserManager _userManager;
        private MarketplaceRoleManager _roleManager;

        public DashboardController()
        {
        }

        public DashboardController(MarketplaceUserManager userManager, MarketplaceRoleManager roleManager)
        {
            UserManager = userManager;
            RoleManager = roleManager;
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

        public MarketplaceRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? HttpContext.GetOwinContext().Get<MarketplaceRoleManager>();
            }
            private set
            {
                _roleManager = value;
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

            model.Roles = RoleManager.Roles.ToList();

            return View(model);
        }

        public ActionResult UsersListing(string roleID, string searchTerm, int? pageNo)
        {

            var pageSize = 10;

            UsersListingViewModel model = new UsersListingViewModel();

            model.RoleID = roleID;
            model.SearchTerm = searchTerm;
            model.PageNo = pageNo;

            //model.Users = UserManager.Users.ToList();

            var users = UserManager.Users;

            if (!string.IsNullOrEmpty(roleID))
            {
                
                users = users.Where(x => x.Roles.FirstOrDefault(y => y.RoleId == roleID) != null);
            }

            if (!string.IsNullOrEmpty(searchTerm))
            {
                users = users.Where(x => x.Email.ToLower().Contains(searchTerm.ToLower()));
            }

            pageNo = pageNo ?? 1;
            
            var skipCount = (pageNo.Value - 1) * pageSize;

            model.Users = users.OrderBy(x => x.Email).Skip(skipCount).Take(pageSize).ToList();

            model.Pager = new Pager(users.Count(), pageNo, pageSize);

            return PartialView(model);
        }

        [HttpGet]
        public async Task<ActionResult> UsersDetails(string userID)
        {

            UserDetailsViewModel model = new UserDetailsViewModel();

            var user = await UserManager.FindByIdAsync(userID);

            if(user != null)
            {
                model.User = user;
            }

            return View(model);
        }


        public ActionResult Roles(string roleID, string searchTerm, int? pageNo)
        {
            UsersViewModel model = new UsersViewModel();

            model.PageTitle = "Roles";
            model.PageDescription = "Roles Listing Page";

            model.RoleID = roleID;
            model.SearchTerm = searchTerm;
            model.PageNo = pageNo;

            model.Roles = RoleManager.Roles.ToList();

            return View(model);
        }
        public ActionResult RolesListing(string searchTerm, int? pageNo)
        {

            var pageSize = 10;

            RoleListingViewModel model = new RoleListingViewModel();

            
            model.SearchTerm = searchTerm;
            
            var roles = RoleManager.Roles;

            if (!string.IsNullOrEmpty(searchTerm))
            {
                roles = roles.Where(x => x.Name.ToLower().Contains(searchTerm.ToLower()));
            }

            pageNo = pageNo ?? 1;

          
            var skipCount = (pageNo.Value - 1) * pageSize;

            model.Roles = roles.OrderBy(x => x.Name).Skip(skipCount).Take(pageSize).ToList();

            model.Pager = new Pager(roles.Count(), pageNo, pageSize);

            return PartialView(model);
        }

    }
}