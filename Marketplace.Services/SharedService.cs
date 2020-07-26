using Marketplace.Data;
using Marketplace.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.Services
{
    public class SharedService
    {
        public int SavePicture(Picture picture)
        {
            MarketplaceContext context = new MarketplaceContext();

            context.Pictures.Add(picture);
            context.SaveChanges();

            return picture.ID;
        }

    }
}
