using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models.Write
{
    public partial class ProductLockedStatus
    {
        public ProductLockedStatus()
        {
            //LockedTimeStamp = DateTime.Now.Ticks;
            Product = new HashSet<Product>();
        }

        public ProductLockedStatus(long productId)
        {
            ProductID = productId;
          //  LockedTimeStamp = DateTime.Now.Ticks;
            Product = new HashSet<Product>();
        }

        [Key]
        public long ProductID { get; set; }
        public bool Locked { get; set; }
        public long LockedTimeStamp { get; set; }
   //     [Key]
        public Guid LockedStatusID { get; set; }

     //   public virtual Product Product { get; set; }
        public ICollection<Product> Product  { get; set; }
    }
}
