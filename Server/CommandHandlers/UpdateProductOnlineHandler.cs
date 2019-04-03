using System.Threading.Tasks;
using Shared.Messages.Commands;
using Microsoft.EntityFrameworkCore;
using NServiceBus;
using NServiceBus.Logging;
using Server.DAL;
using Server.Data;
using Shared.Models.Write;
using Shared.Models.Read;
using Shared.Models.Insert;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Data;


namespace Server.CommandHandlers
{
    public class UpdateProductOnlineHandler : IHandleMessages<UpdateProductOnlineStatus>
    {
        readonly DbContextOptionsBuilder<ApiContext> _dbContextOptionsBuilder;
        public UpdateProductOnlineHandler(DbContextOptionsBuilder<ApiContext> dbContextOptionsBuilder)
        {
            _dbContextOptionsBuilder = dbContextOptionsBuilder;
        }

        static ILog log = LogManager.GetLogger<UpdateProductOnlineHandler>();

        public Task Handle(UpdateProductOnlineStatus message, IMessageHandlerContext context)
        {
            log.Info("Received UpdateProductOnline");

            var productOnlineStatus = new ProductOnlineStatus

            {
                Online = message.OnlineStatus,
                ProductID = message.ProductId,
                OnlineTimeStamp = message.UpdateProductOnlineTimeStamp,
                OnlineStatusID = message.OnlineStatusID
            };



            using (var unitOfWork = new ProductUnitOfWork(new ApiContext(_dbContextOptionsBuilder.Options)))
            {
                unitOfWork.ProductOnlineStatuses.Update(productOnlineStatus);

                unitOfWork.Complete();
            }

            return Task.CompletedTask;
        }
    }
}
