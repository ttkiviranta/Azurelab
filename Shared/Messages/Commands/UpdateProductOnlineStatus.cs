using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Shared.Messages.Commands
{
    [Table("ProductOnlineStatuses", Schema = "Production")]
    public class UpdateProductOnlineStatus
   
    {
        public UpdateProductOnlineStatus()
        {
            DataId = Guid.NewGuid();
        }
        public Guid DataId { get; set; }
        public long ProductId { get; set; }
        public long UpdateProductOnlineTimeStamp { get; set; }
        public bool OnlineStatus { get; set; }
        [Key]
        public Guid OnlineStatusID { get; set; }
    }
}
