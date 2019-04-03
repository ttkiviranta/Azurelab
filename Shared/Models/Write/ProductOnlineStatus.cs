using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models.Write
{
    public class ProductOnlineStatus
    {
        public ProductOnlineStatus()
        {
            OnlineTimeStamp = DateTime.Now.Ticks;
        }

        public ProductOnlineStatus(long producId)
        {
            ProductID = producId;
        }

        [Key]
        public long ProductID { get; set; }
        public bool Online { get; set; }
        public long OnlineTimeStamp { get; set; }
        public Guid OnlineStatusID { get; set; }
    }
}
