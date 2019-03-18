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
 
    public class UpdateProductHandler : IHandleMessages<UpdateProduct>
    {
        readonly DbContextOptionsBuilder<ApiContext> _dbContextOptionsBuilder;
        public UpdateProductHandler(DbContextOptionsBuilder<ApiContext> dbContextOptionsBuilder)
        {
            _dbContextOptionsBuilder = dbContextOptionsBuilder;
        }

        static ILog log = LogManager.GetLogger<UpdateProductHandler>();

        public Task Handle(UpdateProduct message, IMessageHandlerContext context)
        {
            log.Info("Received UpdateProduct");

            var product = new Product
            {
                Class = message.Class,
                Color = message.Color,
                DaysToManufacture = message.DaysToManufacture,
                DiscontinuedDate = message.DiscontinuedDate,
                FinishedGoodsFlag = message.FinishedGoodsFlag,
                ListPrice = message.ListPrice,
                MakeFlag = message.MakeFlag,
                ModifiedDate = message.ModifiedDate,
                Name = message.Name,
                ProductId = message.ProductId,
                ProductLine = message.ProductLine,
                ProductModelId = message.ProductModelId,
                ProductNumber = message.ProductNumber,
                ProductSubcategoryId = message.ProductSubcategoryId,
                ReorderPoint = message.ReorderPoint,
                Rowguid = message.Rowguid,
                SafetyStockLevel = message.SafetyStockLevel,
                SellEndDate = message.SellEndDate,
                SellStartDate = message.SellStartDate,
                Size = message.Size,
                SizeUnitMeasureCode = message.SizeUnitMeasureCode,
                StandardCost = message.StandardCost,
                Style = message.Style,
                UserIdentifier = message.UserIdentifier,
                Weight = message.Weight,
                WeightUnitMeasureCode = message.WeightUnitMeasureCode

            };

            

            using (var unitOfWork = new ProductUnitOfWork(new ApiContext(_dbContextOptionsBuilder.Options)))
            {
                unitOfWork.Products.Update(product);
               
                unitOfWork.Complete();
            }
        
            return Task.CompletedTask;
        }
    }
}
