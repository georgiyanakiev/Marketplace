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

        public bool LeaveComment(Comment comment)
        {
            MarketplaceContext context = new MarketplaceContext();

            context.Comments.Add(comment);
            return context.SaveChanges() > 0;

           
        }

        public List<Comment> GetComments(int entityID, int recordID)
        {
            MarketplaceContext context = new MarketplaceContext();

            return context.Comments.Where(x => x.EntityID == entityID && x.RecordID == recordID).ToList();


        }
    }
}
