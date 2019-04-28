using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Shared.Messages.Commands
{
    [Table("ProductOnlineStatuses", Schema = "Production")]
    public class DeleteProductOnlineStatus

    {
        public DeleteProductOnlineStatus()
        {
            DataId = Guid.NewGuid();
        }
        public Guid DataId { get; set; }
        public long ProductId { get; set; }
        public long DeleteProductOnlineTimeStamp { get; set; }
        public bool OnlineStatus { get; set; }
        [Key]
        public Guid OnlineStatusID { get; set; }
    }
}