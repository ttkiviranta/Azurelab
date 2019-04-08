using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Models
{
    public partial class ProductOnlineStatus
    {
        public ProductOnlineStatus()
        {
            Product = new HashSet<Product>();
        }
        public long ProductID { get; set; }
        public bool Online { get; set; }
        public long OnlineTimeStamp { get; set; }
        public Guid OnlineStatusID { get; set; }
        public ICollection<Product> Product { get; set; }
    }
}
