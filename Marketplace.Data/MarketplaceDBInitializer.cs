using Marketplace.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.Data
{
    public class MarketplaceDBInitializer : CreateDatabaseIfNotExists<MarketplaceContext>
    {
        protected override void Seed(MarketplaceContext context)
        {

            SeedRoles(context);
            SeedUsers(context);
        }

        public void SeedRoles(MarketplaceContext context)
        {
            List<IdentityRole> rolesInMarketPlace = new List<IdentityRole>();

            rolesInMarketPlace.Add(new IdentityRole() { Name = "Administrator" });
            rolesInMarketPlace.Add(new IdentityRole() { Name = "Moderator" });
            rolesInMarketPlace.Add(new IdentityRole() { Name = "User" });

            var rolesStore = new RoleStore<IdentityRole>(context);
            var rolesManager = new RoleManager<IdentityRole>(rolesStore);

            foreach (IdentityRole role in rolesInMarketPlace)
            {
                if (!rolesManager.RoleExists(role.Name))
                {
                    var result = rolesManager.Create(role);
                    if (result.Succeeded) continue;
                }
            }

        }

        public void SeedUsers(MarketplaceContext context)
        {

            var usersStore = new UserStore<MarketplaceUser>(context);
            var usersManager = new UserManager<MarketplaceUser>(usersStore);

            MarketplaceUser admin = new MarketplaceUser();
            admin.Email = "admin@email.com";
            admin.UserName = "admin";
            var password = "admin123";

            if (usersManager.FindByEmail(admin.Email) == null)
            {
                var result = usersManager.Create(admin, password);

                if (result.Succeeded)
                {
                    //add necessary roles to admin
                    usersManager.AddToRole(admin.Id, "Administrator");
                    usersManager.AddToRole(admin.Id, "Moderator");
                    usersManager.AddToRole(admin.Id, "User");
                }
            }

        }
    }
}
