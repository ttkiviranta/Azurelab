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
    public class DeleteProductLockedHandler : IHandleMessages<DeleteProductLockedStatus>
    {
        readonly DbContextOptionsBuilder<ApiContext> _dbContextOptionsBuilder;
        public DeleteProductLockedHandler(DbContextOptionsBuilder<ApiContext> dbContextOptionsBuilder)
        {
            _dbContextOptionsBuilder = dbContextOptionsBuilder;
        }

        static ILog log = LogManager.GetLogger<DeleteProductLockedHandler>();

        public Task Handle(DeleteProductLockedStatus message, IMessageHandlerContext context)
        {
            log.Info("Received DeleteProductLocked");

            var productLockedStatus = new ProductLockedStatus

            {
                Locked = message.LockedStatus,
                ProductID = message.ProductId,
                LockedTimeStamp = message.DeleteProductLockedTimeStamp,
                LockedStatusID = message.LockedStatusID
            };



            using (var unitOfWork = new ProductUnitOfWork(new ApiContext(_dbContextOptionsBuilder.Options)))
            {
                unitOfWork.ProductLockedStatuses.Remove(productLockedStatus);

                unitOfWork.Complete();
            }

            return Task.CompletedTask;
        }
    }
}