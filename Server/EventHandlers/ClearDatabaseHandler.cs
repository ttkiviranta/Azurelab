using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NServiceBus;
using NServiceBus.Logging;
using Server.Data;
using Server.DAL;
using Shared.Messages.Events;

namespace Server.EventHandlers
{
    public class ClearDatabaseHandler : IHandleMessages<ClearDatabase>
    {
        readonly DbContextOptionsBuilder<ApiContext> _dbContextOptionsBuilder;
        public ClearDatabaseHandler(DbContextOptionsBuilder<ApiContext> dbContextOptionsBuilder)
        {
            _dbContextOptionsBuilder = dbContextOptionsBuilder;
        }

        static ILog log = LogManager.GetLogger<ClearDatabaseHandler>();

        public Task Handle(ClearDatabase message, IMessageHandlerContext context)
        {
            log.Info("Received ClearDatabase");
 /*           using (var unitOfWork = new CarUnitOfWork(new ApiContext(_dbContextOptionsBuilder.Options)))
            {
                unitOfWork.ClearDatabase();
            }

            // publish an event that a company had been deleted?*/
            return Task.CompletedTask;
        }
    }
}