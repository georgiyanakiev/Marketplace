using Marketplace.Data;
using Marketplace.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.Services
{
   public class CategoriesService
    {
        public List<Category> GetAllCategories()
        {
            MarketplaceContext context = new MarketplaceContext();

            return context.Categories.ToList();
        }
        
    }
}
