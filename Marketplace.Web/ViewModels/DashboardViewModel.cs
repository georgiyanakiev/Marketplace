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
        public int Categories { get; set; }
        public int Roles { get; set; }
        public int Comments { get; set; }

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

        public string ID  { get; set; }
        public string FullName { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
    }
    public class UserRolesViewModel : PageViewModel
    {
  
        public List<IdentityRole> AvailableRoles { get; set; }
        public List<IdentityRole> UserRoles { get; set; }
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
    public class RoleDetailsViewModel : PageViewModel
    {
        public IdentityRole Role { get; set; }

        public string ID { get; set; }
        public string Name { get; set; }

    }

    public class RoleUsersViewModel : PageViewModel
    {
        public List<MarketplaceUser> RoleUsers { get; set; }

        public Pager Pager { get; set; }
        public string RoleID { get; set; }
       

    }
    public class UserCommentsViewModel : PageViewModel
    {

        public List<Comment> UserComments { get; set; }
        public MarketplaceUser User { get; set; }
    }
    public class CommentablePageViewModel : PageViewModel
    {
        public List<Comment> Comments { get; set; }
        public List<Comment> UserComments { get; set; }
        public MarketplaceUser User { get; set; }
    }

}