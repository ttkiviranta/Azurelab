using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Shared.Messages.Commands
{
    [Table("ProductLockedStatuses", Schema = "Production")]
    public class CreateProductLockedStatus

    {
        public CreateProductLockedStatus()
        {
            DataId = Guid.NewGuid();
            LockedStatusID = Guid.NewGuid();
        }
        public Guid DataId { get; set; }
        public long ProductId { get; set; }
        public long CreateProductLockedTimeStamp { get; set; }
        public bool LockedStatus { get; set; }
        [Key]
        public Guid LockedStatusID { get; set; }
    }
}
