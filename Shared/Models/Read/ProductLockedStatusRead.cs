using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Shared.Models.Write;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models.Read
{
    public partial class ProductLockedStatusRead
    {
        public ProductLockedStatusRead()
        {
            LockedTimeStamp = DateTime.Now.Ticks;
          //  Product = new HashSet<Product>();
        }

        public ProductLockedStatusRead(long productId)
        {
            ProductID = productId;
        }

        [Key]
        public long ProductID { get; set; }
        public bool Locked { get; set; }
        public long LockedTimeStamp { get; set; }
        public Guid LockedStatusID { get; set; }
    }
}
