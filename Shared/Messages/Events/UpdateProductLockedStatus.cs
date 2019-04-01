using NServiceBus;
using System;
namespace Shared.Messages.Events
{
    public class UpdateProductLockedStatus
    {
        public UpdateProductLockedStatus()
        {
            DataId = Guid.NewGuid();
        }
        public Guid DataId { get; set; }
        public Guid CompanyId { get; set; }
        public long UpdateProductLockedTimeStamp { get; set; }
        public bool LockedStatus { get; set; }
    }
}
