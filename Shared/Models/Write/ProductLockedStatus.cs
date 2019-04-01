using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models.Write
{
    public class ProductLockedStatus
    {
        public ProductLockedStatus()
        {
            LockedTimeStamp = DateTime.Now.Ticks;
        }

        public ProductLockedStatus(long producId)
        {
            ProductID = producId;
        }

        [Key]
        public long ProductID { get; set; }
        public bool Locked { get; set; }
        public long LockedTimeStamp { get; set; }
    }
}
