using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Models
{
    public partial class ProductLockedStatus
    {
        public ProductLockedStatus()
        {
            Product = new HashSet<Product>();
        }
        public long ProductID { get; set; }
        public bool Locked { get; set; }
        public long LockedTimeStamp { get; set; }
        public Guid LockedStatusID { get; set; }
        public ICollection<Product> Product { get; set; }
    }
}
