using System;
namespace Shared.Messages.Events
{
    public class ClearDatabase
    {
        public ClearDatabase()
        {
            DataId = Guid.NewGuid();
        }
        public Guid DataId { get; set; }
    }
}
