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
    public class DeleteProductOnlineHandler : IHandleMessages<DeleteProductOnlineStatus>
    {
        readonly DbContextOptionsBuilder<ApiContext> _dbContextOptionsBuilder;
        public DeleteProductOnlineHandler(DbContextOptionsBuilder<ApiContext> dbContextOptionsBuilder)
        {
            _dbContextOptionsBuilder = dbContextOptionsBuilder;
        }

        static ILog log = LogManager.GetLogger<DeleteProductOnlineHandler>();

        public Task Handle(DeleteProductOnlineStatus message, IMessageHandlerContext context)
        {
            log.Info("Received DeleteProductOnline");

            var productOnlineStatus = new ProductOnlineStatus

            {
                Online = message.OnlineStatus,
                ProductID = message.ProductId,
                OnlineTimeStamp = message.DeleteProductOnlineTimeStamp,
                OnlineStatusID = message.OnlineStatusID
            };



            using (var unitOfWork = new ProductUnitOfWork(new ApiContext(_dbContextOptionsBuilder.Options)))
            {
                unitOfWork.ProductOnlineStatuses.Remove(productOnlineStatus);

                unitOfWork.Complete();
            }

            return Task.CompletedTask;
        }
    }
}