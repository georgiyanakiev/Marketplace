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
        public Category GetCategoryByID(int ID)
        {
            MarketplaceContext context = new MarketplaceContext();

            return context.Categories.Find(ID);
        }
        public void SaveCategory(Category category)
        {
            MarketplaceContext context = new MarketplaceContext();

            context.Categories.Add(category);

            context.SaveChanges();
        }

        public void UpdateCategory(Category category)
        {
            MarketplaceContext context = new MarketplaceContext();

            context.Entry(category).State = System.Data.Entity.EntityState.Modified;

            context.SaveChanges();
        }

        public void DeleteCategory(Category category)
        {
            MarketplaceContext context = new MarketplaceContext();

            context.Entry(category).State = System.Data.Entity.EntityState.Deleted;

            context.SaveChanges();
        }
    }
}
