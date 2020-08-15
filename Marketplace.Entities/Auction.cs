using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.Entities
{
     public class Auction : BaseEntity
    {   
        
        public virtual Category Category { get; set; }
        public int CategoryID { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = "Minimum length should be 3 characters.")]
        [MaxLength(150)]
        public string Title { get; set; }
        public string Description { get; set; }

        [Required]
        [Range(1, 1000000, ErrorMessage = "Actual amount must be within 1 - 1000000")]
        public decimal ActualAmount { get; set; }

       
        public DateTime? StartTime { get; set; }
        public Nullable<DateTime> EndTime { get; set; }

        public virtual List<AuctionPicture> AuctionPictures { get; set; }

        public List<Bid> Bids { get; set; }


    }
}
