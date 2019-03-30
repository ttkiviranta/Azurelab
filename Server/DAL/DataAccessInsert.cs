using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Shared.Models.Insert;
using Shared.Messages.Commands;
using Shared.Models;
using Microsoft.Extensions.Configuration;
using System.IO;
using Shared.Utils;

using Microsoft.AspNetCore.Mvc;

namespace Server.DAL
{
    public class DataAccessInsert
    {
        public DataAccessInsert(IConfiguration configuration)
        {
            Configuration = configuration;
            _optionsBuilder.UseSqlServer(Helpers.GetSqlConnection());
        }
        IConfiguration Configuration { get; set; }

        private readonly DbContextOptionsBuilder<ApiContext> _optionsBuilder = new DbContextOptionsBuilder<ApiContext>();

        public CreateProduct InsertProduct(ProductInsert product)
        {
            CreateProduct productInsert = null;
            using (var db = new ProductContext(_optionsBuilder.Options))
            {
                var item = new ProductInsert
                {
                    Rowguid = new Guid(),
                    Class = product.Class,
                    Color = product.Color,
                    DaysToManufacture = product.DaysToManufacture,
                    DiscontinuedDate = product.DiscontinuedDate,
                    FinishedGoodsFlag = product.FinishedGoodsFlag,
                    ListPrice = product.ListPrice,
                    MakeFlag = product.MakeFlag,
                    ModifiedDate = product.ModifiedDate,
                    Name = product.Name,
                    ProductId = product.ProductId,
                    ProductLine = product.ProductLine,
                    ProductModelId = product.ProductModelId,
                    ProductNumber = product.ProductNumber,
                    ProductSubcategoryId = product.ProductSubcategoryId,
                    ReorderPoint = product.ReorderPoint,
                    SafetyStockLevel = product.SafetyStockLevel,
                    SellEndDate = product.SellEndDate,
                    SellStartDate = product.SellStartDate,
                    Size = product.Size,
                    SizeUnitMeasureCode = product.SizeUnitMeasureCode,
                    StandardCost = product.StandardCost,
                    Style = product.Style,
                    UserIdentifier = product.UserIdentifier,
                    Weight = product.Weight,
                    WeightUnitMeasureCode = product.WeightUnitMeasureCode
                };

                db.Products.Add(item);
                db.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [Homelab].[Production].[Product] ON;");
                db.SaveChanges();
                db.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [Homelab].[Production].[Product] OFF;");
                return productInsert;
            }
        }
    }
}
