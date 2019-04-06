using NServiceBus;
using System;
using System.ComponentModel.DataAnnotations;

namespace Shared.Messages.Events
{
    public class UpdateProductLockedStatus
    {
        public UpdateProductLockedStatus()
        {
            DataId = Guid.NewGuid();
        }
        public Guid DataId { get; set; }
        [Key]
        public long ProductId { get; set; }
        public long UpdateProductLockedTimeStamp { get; set; }
        public bool LockedStatus { get; set; }
  //     [Key]
        public Guid LockedStatusID { get; set; }
    }
}
