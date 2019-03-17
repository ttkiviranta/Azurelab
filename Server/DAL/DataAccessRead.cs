using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Shared.Models.Read;
using Shared.Models;
using Microsoft.Extensions.Configuration;
using System.IO;
using Shared.Utils;
using System.Data.SqlClient;
using System.Text;

namespace Server.DAL
{
    public class DataAccessRead
    {
        public DataAccessRead(IConfiguration configuration)
        {
            Configuration = configuration;
            //_optionsBuilder.UseSqlite("DataSource=" + Helpers.GetDbLocation(Configuration["AppSettings:DbLocation"]) + "Car.db");
            _optionsBuilder.UseSqlServer(Helpers.GetSqlConnection());
        }

        
        IConfiguration Configuration { get; set; }

        private readonly DbContextOptionsBuilder<ApiContext> _optionsBuilder = new DbContextOptionsBuilder<ApiContext>();

        public ICollection<ProductRead> GetProducts()
        {
            var ProductReads = new List<ProductRead>();
            using (var context = new ApiContext(_optionsBuilder.Options))
            {
                var uniqueProductReadNull = context.Product.OrderBy(c => c.ProductId).ToList();
                foreach (var ProductReadNull in uniqueProductReadNull)
                {
                    //    var addressList = context.ProductReadNulls.Where(w => (w.Address != null && w.CompanyId == ProductReadNull.CompanyId)).OrderBy(c => c.ChangeTimeStamp).Select(s => new { Address = s.Address ?? "", s.ChangeTimeStamp }).ToList();
                    //    var nameList = context.ProductReadNulls.Where(w => (w.Name != null && w.CompanyId == ProductReadNull.CompanyId)).OrderBy(c => c.ChangeTimeStamp).Select(s => new { Name = s.Name ?? "", s.ChangeTimeStamp }).ToList();
                    //    var deletedList = context.ProductReadNulls.Where(w => (w.Deleted != null && w.CompanyId == ProductReadNull.CompanyId)).OrderBy(c => c.ChangeTimeStamp).Select(s => new { Deleted = s.Deleted ?? false, s.ChangeTimeStamp }).ToList();
                    //    bool IsDeleted = (!deletedList.Any()) ? false : deletedList.LastOrDefault().Deleted;
                    //    if (!IsDeleted)
                    {
                        ProductReads.Add(new ProductRead(ProductReadNull.Rowguid)
                        {
                            Name = ProductReadNull.Name,
                            ProductId = ProductReadNull.ProductId,
                            ProductNumber = ProductReadNull.ProductNumber,

                            Rowguid = ProductReadNull.Rowguid,
                            Class =ProductReadNull.Class,
                            Color =ProductReadNull.Color,
                            DaysToManufacture =ProductReadNull.DaysToManufacture,
                            DiscontinuedDate =ProductReadNull.DiscontinuedDate,
                            FinishedGoodsFlag =ProductReadNull.FinishedGoodsFlag,
                            ListPrice =ProductReadNull.ListPrice,
                            MakeFlag =ProductReadNull.MakeFlag,
                            ModifiedDate =ProductReadNull.ModifiedDate,
                            ProductLine =ProductReadNull.ProductLine,
                            ProductModelId =ProductReadNull.ProductModelId,
                            ProductSubcategoryId =ProductReadNull.ProductSubcategoryId,
                            ReorderPoint =ProductReadNull.ReorderPoint,
                            SafetyStockLevel =ProductReadNull.SafetyStockLevel,
                            SellEndDate =ProductReadNull.SellEndDate,
                            SellStartDate =ProductReadNull.SellStartDate,
                            Size =ProductReadNull.Size,
                            SizeUnitMeasureCode =ProductReadNull.SizeUnitMeasureCode,
                            StandardCost =ProductReadNull.StandardCost,
                            Style =ProductReadNull.Style,
                            UserIdentifier =ProductReadNull.UserIdentifier,
                            Weight =ProductReadNull.Weight,
                            WeightUnitMeasureCode =ProductReadNull.WeightUnitMeasureCode
                        });
                    }
                }

                return ProductReads;
            }
     
        }

        public ProductRead GetProduct(Guid rowquid)
        {
            ProductRead productRead = null;
            using (var context = new ApiContext(_optionsBuilder.Options))
            {
                var productReadNull = context.Product.Where(c => c.Rowguid == rowquid);
                foreach (var ProductReadNull in productReadNull)
                {

                    productRead = new ProductRead(rowquid)
                    {
                        Name = ProductReadNull.Name,
                        ProductId = ProductReadNull.ProductId,
                        ProductNumber = ProductReadNull.ProductNumber,

                        Rowguid = new Guid(),
                        Class = ProductReadNull.Class,
                        Color = ProductReadNull.Color,
                        DaysToManufacture = ProductReadNull.DaysToManufacture,
                        DiscontinuedDate = ProductReadNull.DiscontinuedDate,
                        FinishedGoodsFlag = ProductReadNull.FinishedGoodsFlag,
                        ListPrice = ProductReadNull.ListPrice,
                        MakeFlag = ProductReadNull.MakeFlag,
                        ModifiedDate = ProductReadNull.ModifiedDate,
                        ProductLine = ProductReadNull.ProductLine,
                        ProductModelId = ProductReadNull.ProductModelId,
                        ProductSubcategoryId = ProductReadNull.ProductSubcategoryId,
                        ReorderPoint = ProductReadNull.ReorderPoint,
                        SafetyStockLevel = ProductReadNull.SafetyStockLevel,
                        SellEndDate = ProductReadNull.SellEndDate,
                        SellStartDate = ProductReadNull.SellStartDate,
                        Size = ProductReadNull.Size,
                        SizeUnitMeasureCode = ProductReadNull.SizeUnitMeasureCode,
                        StandardCost = ProductReadNull.StandardCost,
                        Style = ProductReadNull.Style,
                        UserIdentifier = ProductReadNull.UserIdentifier,
                        Weight = ProductReadNull.Weight,
                        WeightUnitMeasureCode = ProductReadNull.WeightUnitMeasureCode
                    };

                }
                /* if (productReadNull != null)
                 {
                     productRead = new ProductRead(rowquid)
                     {
                         Name = productRead.Name,
                         ProductId = productRead.ProductId,
                         ProductNumber = productRead.ProductNumber,

                     };
                 }*/

                return productRead;
            }

        }

    }
}
