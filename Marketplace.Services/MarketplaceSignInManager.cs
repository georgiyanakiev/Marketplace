using Marketplace.Entities;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.Services
{
    public class MarketplaceSignInManager : SignInManager<MarketplaceUser, string>
    {
        public MarketplaceSignInManager(MarketplaceUserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(MarketplaceUser user)
        {
            return user.GenerateUserIdentityAsync((MarketplaceUserManager)UserManager);
        }

        public static MarketplaceSignInManager Create(IdentityFactoryOptions<MarketplaceSignInManager> options, IOwinContext context)
        {
            return new MarketplaceSignInManager(context.GetUserManager<MarketplaceUserManager>(), context.Authentication);
        }
    }
}
