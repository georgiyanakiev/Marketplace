using Marketplace.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Marketplace.Web.ViewModels
{
    public class DashboardViewModel : PageViewModel
    {
        public int UserCount { get; set; }
        public int AuctionsCount { get; set; }
        public int BidsCount { get; set; }
        
    }

   public class UsersViewModel : PageViewModel
    {
        public string SearchTerm { get; set; }
        public string RoleID { get; set; }
        public List<IdentityRole> Roles { get; set; }
        public string FullName { get; set; }
        public int? PageNo { get; set; }
    }
   



    public class UsersListingViewModel : PageViewModel
    {
        public List<MarketplaceUser> Users { get; set; }
        public Pager Pager { get; set; }
        public string RoleID { get; set; }
        public string SearchTerm { get; set; }
        public int? PageNo { get;  set; }
    }

    public class UserDetailsViewModel : PageViewModel
    {
        public MarketplaceUser User { get; set; }
    }
    public class RolesViewModel : PageViewModel
    {
        public string SearchTerm { get; set; }
        public int? PageNo { get; set; }
    }


    public class RoleListingViewModel : PageViewModel
    {
        public List<IdentityRole> Roles { get; set; }
        public Pager Pager { get; set; } 
        public string SearchTerm { get; set; }
        
    }
}