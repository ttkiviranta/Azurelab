using System;
namespace Shared.Messages.Events
{
    //TODO
    public class ClearDatabase
    {
        public ClearDatabase()
        {
            DataId = Guid.NewGuid();
        }
        public Guid DataId { get; set; }
    }
}
