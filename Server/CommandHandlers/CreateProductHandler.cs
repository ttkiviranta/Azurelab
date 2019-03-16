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
    public class CreateProductHandler: IHandleMessages<CreateProduct>
    {
        readonly DbContextOptionsBuilder<ApiContext> _dbContextOptionsBuilder;
        public CreateProductHandler(DbContextOptionsBuilder<ApiContext> dbContextOptionsBuilder)
        {
            _dbContextOptionsBuilder = dbContextOptionsBuilder;
        }

        static ILog log = LogManager.GetLogger<CreateProductHandler>();

        public Task Handle(CreateProduct message, IMessageHandlerContext context)
        {
            log.Info("Received CreateProduct");

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

            var productIdParam = new SqlParameter("@ProductIdParam", SqlDbType.BigInt);
            var nameParam = new SqlParameter("@NameParam", SqlDbType.NVarChar);
            var productNumberParam = new SqlParameter("@ProductNumberParam", SqlDbType.NVarChar);
            var makeFlagParam = new SqlParameter("@MakeFlagParam", SqlDbType.Bit);
            var finishedGoodsFlagParam = new SqlParameter("@FinishedGoodsFlagParam", SqlDbType.Bit);
            var colorParam = new SqlParameter("@ColorParam", SqlDbType.NVarChar);
            var safetyStockLevelParam = new SqlParameter("@SafetyStockLevelParam", SqlDbType.SmallInt);
            var reorderPointParam = new SqlParameter("@ReorderPointParam", SqlDbType.SmallInt);
            var standardCostParam = new SqlParameter("@StandardCostParam", SqlDbType.Money);
            var listPriceParam = new SqlParameter("@ListPriceParam", SqlDbType.Money);
            var sizeParam = new SqlParameter("@SizeParam", SqlDbType.NVarChar);
            var sizeUnitMeasureCodeParam = new SqlParameter("@SizeUnitMeasureCodeParam", SqlDbType.NVarChar);
            var weightUnitMeasureCodeParam = new SqlParameter("@WeightUnitMeasureCodeParam", SqlDbType.NVarChar);
            var weightParam = new SqlParameter("@WeightParam", SqlDbType.Decimal);
            var daysToManufactureParam = new SqlParameter("@DaysToManufactureParam", SqlDbType.Int);
            var productLineParam = new SqlParameter("@ProductLineParam", SqlDbType.NChar);
            var classParam = new SqlParameter("@ClassParam", SqlDbType.NChar);
            var styleParam = new SqlParameter("@StyleParam", SqlDbType.NChar);
            var productSubcategoryIdParam = new SqlParameter("@ProductSubcategoryIdParam", SqlDbType.Int);
            var productModelIdParam = new SqlParameter("@ProductModelIdParam", SqlDbType.Int);
            var sellStartDateParam = new SqlParameter("@SellStartDateParam", SqlDbType.DateTime);
            var sellEndDateParam = new SqlParameter("@SellEndDateParam", SqlDbType.DateTime);
            var discontinuedDateParam = new SqlParameter("@DiscontinuedDateParam", SqlDbType.DateTime);
            var rowguidParam = new SqlParameter("@RowguidParam", SqlDbType.UniqueIdentifier);
            var modifiedDateParam = new SqlParameter("@ModifiedDateParam", SqlDbType.DateTime);
            var userIdentifierParam = new SqlParameter("@UserIdentifierParam", SqlDbType.NVarChar);
            var productId = DateTime.Now.ToString("yyyyMMddhhmmss").ToString();

            long productID;
            var time = DateTime.Now.ToString("yyyyMMddhhmmss").ToString();
            //  var rowGuid = Guid.NewGuid().ToString();
            long.TryParse(time, out productID);

            productIdParam.Value = productId; //product.ProductId;
            nameParam.Value = product.Name;
            productNumberParam.Value = product.ProductNumber;
            makeFlagParam.Value = product.MakeFlag;
            finishedGoodsFlagParam.Value = product.FinishedGoodsFlag;
            colorParam.Value = product.Color;
            if (colorParam.Value == null)
                colorParam.Value = DBNull.Value;
            safetyStockLevelParam.Value = product.SafetyStockLevel;
            reorderPointParam.Value = product.ReorderPoint;
            standardCostParam.Value = product.StandardCost;
            listPriceParam.Value = product.ListPrice;
            sizeParam.Value = product.Size;
            if (sizeParam.Value == null)
                sizeParam.Value = DBNull.Value;
            sizeUnitMeasureCodeParam.Value = product.SizeUnitMeasureCode;
            if (sizeUnitMeasureCodeParam.Value == null)
                sizeUnitMeasureCodeParam.Value = DBNull.Value;
            weightUnitMeasureCodeParam.Value = product.WeightUnitMeasureCode;
            if (weightUnitMeasureCodeParam.Value == null)
                weightUnitMeasureCodeParam.Value = DBNull.Value;
            weightParam.Value = product.Weight;
            if (weightParam.Value == null)
                weightParam.Value = DBNull.Value;
            daysToManufactureParam.Value = product.DaysToManufacture;
            productLineParam.Value = product.ProductLine;
            if (productLineParam.Value == null)
                productLineParam.Value = DBNull.Value;
            classParam.Value = product.Class;
            if (classParam.Value == null)
                classParam.Value = DBNull.Value;
            styleParam.Value = product.Style;
            if (styleParam.Value == null)
                styleParam.Value = DBNull.Value;
            productSubcategoryIdParam.Value = product.ProductSubcategoryId;
            productModelIdParam.Value = product.ProductModelId;
            sellStartDateParam.Value = product.SellStartDate;
            sellEndDateParam.Value = product.SellEndDate;
            discontinuedDateParam.Value = product.DiscontinuedDate;
            rowguidParam.Value = Guid.NewGuid(); //product.Rowguid;  
            modifiedDateParam.Value = DateTime.Today; //product.ModifiedDate;
            userIdentifierParam.Value = product.UserIdentifier;
            if (userIdentifierParam.Value == null)
                userIdentifierParam.Value = DBNull.Value;

            using (var unitOfWork = new ProductUnitOfWork(new ApiContext(_dbContextOptionsBuilder.Options)))
            {
                unitOfWork.Products.Add(product);
             //   unitOfWork.Complete(); 
                var db = new ApiContext(_dbContextOptionsBuilder.Options);
                    db.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [Homelab].[Production].[Product]  ON;"); //not work rigth way!!!
                    // Not working because previous command not efected!!!
                // quick and dirty...
                db.Database.ExecuteSqlCommandAsync("EXEC usp_InsertProduct @ProductIdParam,@NameParam,@ProductNumberParam,@MakeFlagParam,@FinishedGoodsFlagParam,@ColorParam,@SafetyStockLevelParam,@ReorderPointParam,@StandardCostParam,@ListPriceParam,@SizeParam,@SizeUnitMeasureCodeParam,@WeightUnitMeasureCodeParam,@WeightParam,@DaysToManufactureParam,@ProductLineParam,@ClassParam,@StyleParam,@ProductSubcategoryIdParam,@ProductModelIdParam,@SellStartDateParam,@SellEndDateParam,@DiscontinuedDateParam,@RowguidParam,@ModifiedDateParam,@UserIdentifierParam",
                          parameters: new[] { productIdParam, nameParam, productNumberParam, makeFlagParam, finishedGoodsFlagParam, colorParam, safetyStockLevelParam, reorderPointParam, standardCostParam,
                                       listPriceParam, sizeParam, sizeUnitMeasureCodeParam, weightUnitMeasureCodeParam, weightParam, daysToManufactureParam, productLineParam, classParam,
                                       styleParam, productSubcategoryIdParam, productModelIdParam, sellStartDateParam, sellEndDateParam,  discontinuedDateParam, rowguidParam,
                                       modifiedDateParam, userIdentifierParam });
                unitOfWork.Complete();
            }
            // Publish an event that a car was created?
          
            return Task.CompletedTask;
        }
    }
}
