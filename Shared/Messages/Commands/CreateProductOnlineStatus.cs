using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Shared.Messages.Commands
{
    [Table("ProductOnlineStatuses", Schema = "Production")]
    public class CreateProductOnlineStatus

    {
        public CreateProductOnlineStatus()
        {
            DataId = Guid.NewGuid();
            OnlineStatusID = Guid.NewGuid();
        }
        public Guid DataId { get; set; }
        public long ProductId { get; set; }
        public long CreateProductOnlineTimeStamp { get; set; }
        public bool OnlineStatus { get; set; }
        [Key]
        public Guid OnlineStatusID { get; set; }
    }
}
