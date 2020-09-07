
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
        AuctionsServices auctionsService = new AuctionsServices();

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
        public async Task<ActionResult> UsersDetails(string userID, bool isPartial = false)
        {

            UserDetailsViewModel model = new UserDetailsViewModel();

            var user = await UserManager.FindByIdAsync(userID);

            if(user != null)
            {
                model.User = user;
            }
            if(isPartial || Request.IsAjaxRequest())
            {
                return PartialView("_UsersDetails", model);
            }
            else
            {
                return View(model);
            }

        }

        public async Task<ActionResult> UsersRoles(string userID)
        {

            UserRolesViewModel model = new UserRolesViewModel();

            model.AvailableRoles = RoleManager.Roles.ToList();

            if(!string.IsNullOrEmpty(userID))
            {
               model.User = await UserManager.FindByIdAsync(userID);

                if(model.User != null)
                {
                    model.UserRoles = model.User.Roles.Select(userRole => model.AvailableRoles.FirstOrDefault(role => role.Id == userRole.RoleId)).ToList();
                }
            }

            
            return PartialView("_UsersRoles", model);
        }

        public async Task<ActionResult> AssignUserRole(string userID, string roleID)
        {
            if(!string.IsNullOrEmpty(userID) && !string.IsNullOrEmpty(roleID))
            {
                var user = await UserManager.FindByIdAsync(userID); 

                if (user != null)
                {
                    var role = await RoleManager.FindByIdAsync(roleID);

                    if(role != null)
                    {
                        await UserManager.AddToRolesAsync(userID, role.Name);
                    }
                   
                }
            }

            return RedirectToAction("UsersRoles", new {  userID = userID }); 
        }

        public async Task<ActionResult> DeleteUserRole(string userID, string roleID)
        {
            if (string.IsNullOrEmpty(userID) && !string.IsNullOrEmpty(roleID))
            {
                var user = await UserManager.FindByIdAsync(userID);

                if (user != null)
                {
                    var role = await RoleManager.FindByIdAsync(roleID);

                    if (role != null)
                    {
                        await UserManager.RemoveFromRoleAsync(userID, role.Name);
                    }

                }
            }

            return RedirectToAction("UsersRoles", new { userID = userID });
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
        public async Task<ActionResult> RoleDetails(string roleID)
        {
            if (!string.IsNullOrEmpty(roleID) && !string.IsNullOrEmpty(roleID))
            {
                var user = await UserManager.FindByIdAsync(roleID);

                if (user != null)
                {
                    var role = await RoleManager.FindByIdAsync(roleID);

                    if (role != null)
                    {
                        await UserManager.AddToRolesAsync(roleID, role.Name);
                    }

                }
            }

            return RedirectToAction("UsersRoles", new { roleID = roleID });
        }

        public async Task<ActionResult> RoleUsers(string roleID, int? pageNo)
        {
            if (string.IsNullOrEmpty(roleID) && !string.IsNullOrEmpty(roleID))
            {
                var user = await UserManager.FindByIdAsync(roleID);

                if (user != null)
                {
                    var role = await RoleManager.FindByIdAsync(roleID);

                    if (role != null)
                    {
                        await UserManager.RemoveFromRoleAsync(roleID, role.Name);
                    }

                }
            }

            return RedirectToAction("UsersRoles", new { roleID = roleID });
        }
        //[HttpPost]
        //public async Task<JsonResult> CreateRole(string roleName)
        //{
        //    if (string.IsNullOrEmpty(roleName) && !string.IsNullOrEmpty(roleName))
        //    {
        //        var user = await UserManager.FindByIdAsync(roleName);

        //        if (user != null)
        //        {
        //            var role = await RoleManager.FindByIdAsync(roleName);

        //            if (role != null)
        //            {
        //                await UserManager.RemoveFromRoleAsync(roleName, role.Name);
        //            }

        //        }
        //    }

        //    return RedirectToAction("UsersRoles", new { roleName = roleName });
        //}
        //[HttpPost]
        //public async Task<JsonResult> UpdateRoleDetails(string roleID, string roleName)
        //{
        //    if (string.IsNullOrEmpty(roleID) && !string.IsNullOrEmpty(roleID))
        //    {
        //        var user = await UserManager.FindByIdAsync(roleID);

        //        if (user != null)
        //        {
        //            var role = await RoleManager.FindByIdAsync(roleID);

        //            if (role != null)
        //            {
        //                await UserManager.RemoveFromRoleAsync(roleID, role.Name);
        //            }

        //        }
        //    }

        //    return RedirectToAction("UsersRoles", new { roleID = roleID });
        //}
        //[HttpPost]
        //public async Task<JsonResult> DeleteRoleDetails(string roleID)
        //{
        //    if (string.IsNullOrEmpty(roleID) && !string.IsNullOrEmpty(roleID))
        //    {
        //        var user = await UserManager.FindByIdAsync(roleID);

        //        if (user != null)
        //        {
        //            var role = await RoleManager.FindByIdAsync(roleID);

        //            if (role != null)
        //            {
        //                await UserManager.RemoveFromRoleAsync(roleID, role.Name);
        //            }

        //        }
        //    }

        //    return RedirectToAction("UsersRoles", new { roleID = roleID });
        //}

        public async Task<ActionResult> UsersComments(string userID)
        {

            UserCommentsViewModel model = new UserCommentsViewModel();

            if (!string.IsNullOrEmpty(userID))
            {
                model.User = await UserManager.FindByIdAsync(userID);

                if (model.User != null)
                {

                    model.UserComments = service.GetCommentsByUser(userID, (int)EntityEnums.Auction);

                    if (model.UserComments != null && model.UserComments.Count > 0)
                    {
                        var auctionIDs = model.UserComments.Select(x => x.RecordID).ToList();

                        model.CommentedAuctions = auctionsService.GetAuctionsByIDs(auctionIDs);
                    }

                }
            }

            return PartialView("_UsersComments", model);
        }
    }
}