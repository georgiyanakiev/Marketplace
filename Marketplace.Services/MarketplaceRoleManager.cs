using Marketplace.Data;
using Marketplace.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.Services
{
    public class MarketplaceRoleManager : RoleManager<IdentityRole>
    {
        public MarketplaceRoleManager(IRoleStore<IdentityRole, string> roleStore) : base(roleStore)
        {


        }

        public static MarketplaceRoleManager Create(IdentityFactoryOptions<MarketplaceRoleManager> options, IOwinContext context)
        {
            return new MarketplaceRoleManager(new RoleStore<IdentityRole>(context.Get<MarketplaceContext>()));
        }
    }
}
