using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models.Read
{
    public class ProductLockedStatusRead
    {
        public ProductLockedStatusRead()
        {
            LockedTimeStamp = DateTime.Now.Ticks;
        }

        public ProductLockedStatusRead(long producId)
        {
            ProductID = producId;
        }

        [Key]
        public long ProductID { get; set; }
        public bool Locked { get; set; }
        public long LockedTimeStamp { get; set; }
        public Guid LockedStatusID { get; set; }
    }
}
