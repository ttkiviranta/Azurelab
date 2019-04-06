using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NServiceBus;
using NServiceBus.Logging;
using Server.Data;
using Server.DAL;
using Shared.Models.Write;
using Shared.Models.Read;
using Shared.Messages.Events;

namespace Server.EventHandlers
{
    public class UpdateProductLockedStatusHandler : IHandleMessages<UpdateProductLockedStatus>
    {
        readonly DbContextOptionsBuilder<ApiContext> _dbContextOptionsBuilder;

        public UpdateProductLockedStatusHandler(DbContextOptionsBuilder<ApiContext> dbContextOptionsBuilder)
        {
            _dbContextOptionsBuilder = dbContextOptionsBuilder;
        }

        static ILog log = LogManager.GetLogger<UpdateProductLockedStatusHandler>();

        public Task Handle(UpdateProductLockedStatus message, IMessageHandlerContext context)
        {

            log.Info("Received UpdateProductLockedStatus: " + message.LockedStatus);

            using (var unitOfWork = new ProductUnitOfWork(new ApiContext(_dbContextOptionsBuilder.Options)))
            {
                if (unitOfWork.ProductLockedStatuses.Get(message.ProductId) == null) return Task.CompletedTask;
            }
            var ProductLockedStatus = new ProductLockedStatus
            {
                Locked = message.LockedStatus,
                ProductID = message.ProductId,
                LockedTimeStamp = message.UpdateProductLockedTimeStamp,
                LockedStatusID = message.LockedStatusID
            };

            using (var unitOfWork = new ProductUnitOfWork(new ApiContext(_dbContextOptionsBuilder.Options)))
            {

                unitOfWork.ProductLockedStatuses.Update(ProductLockedStatus);
              
                unitOfWork.Complete();
            }
            return Task.CompletedTask;
        }
    }
}
