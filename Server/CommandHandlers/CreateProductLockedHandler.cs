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
    public class CreateProductLockedHandler : IHandleMessages<CreateProductLockedStatus>
    {
        readonly DbContextOptionsBuilder<ApiContext> _dbContextOptionsBuilder;
        public CreateProductLockedHandler(DbContextOptionsBuilder<ApiContext> dbContextOptionsBuilder)
        {
            _dbContextOptionsBuilder = dbContextOptionsBuilder;
        }

        static ILog log = LogManager.GetLogger<CreateProductLockedHandler>();

        public Task Handle(CreateProductLockedStatus message, IMessageHandlerContext context)
        {
            log.Info("Received CreateProductLocked");

            var productLockedStatus = new ProductLockedStatus

            {
                Locked = message.LockedStatus,
                ProductID = message.ProductId,
                LockedTimeStamp = message. CreateProductLockedTimeStamp,
                LockedStatusID = message.LockedStatusID
            };



            using (var unitOfWork = new ProductUnitOfWork(new ApiContext(_dbContextOptionsBuilder.Options)))
            {
                unitOfWork.ProductLockedStatuses.Add(productLockedStatus);

                unitOfWork.Complete();
            }

            return Task.CompletedTask;
        }
    }
}
