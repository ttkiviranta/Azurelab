﻿using System;
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
            var productReads = new List<ProductRead>();
           // ProductModel productModel = new ProductModel();

            using (var context = new ApiContext(_optionsBuilder.Options))
            {
                //    var uniqueProductReadNull =  context.Product.OrderBy(c => c.ProductId).ToList();
                var uniqueProductReadNull = context.Product.Include(p => p.ProductModel).Include(p => p.ProductSubcategory).OrderBy(p => p.ProductId).ToList();
                foreach (var productReadNull in uniqueProductReadNull)
                {
                    
                /*    if (productReadNull.ProductModel.Name != null)
                    {
                        foreach (var modelName in productReadNull.ProductModel.Name)
                        {
                            modelList.Add(new ProductModel(productReadNull.ProductModel.Name));
                        }
                     
                    }*/

                    //var modelList = context.Product.Where(w => (w.ProductModelId != null && w.ProductModelId == productReadNull.ProductModelId)).Select(s => new { s.ProductModel }).ToList();
                    //    var modelList = context.Product.Where(w => (w.ProductModelId != null && w.ProductModelId == productReadNull.ProductModelId)).Select(s => new { s.ProductModel}).ToList();
                    //    var nameList = context.ProductReadNulls.Where(w => (w.Name != null && w.CompanyId == ProductReadNull.CompanyId)).OrderBy(c => c.ChangeTimeStamp).Select(s => new { Name = s.Name ?? "", s.ChangeTimeStamp }).ToList();
                    //    var deletedList = context.ProductReadNulls.Where(w => (w.Deleted != null && w.CompanyId == ProductReadNull.CompanyId)).OrderBy(c => c.ChangeTimeStamp).Select(s => new { Deleted = s.Deleted ?? false, s.ChangeTimeStamp }).ToList();
                    //    bool IsDeleted = (!deletedList.Any()) ? false : deletedList.LastOrD
                    //    if (!IsDeleted)
                    {
                        productReads.Add(new ProductRead(productReadNull.ProductId)
                        {
                            Name = productReadNull.Name,
                            ProductId = productReadNull.ProductId,
                            ProductNumber = productReadNull.ProductNumber,

                            Rowguid = productReadNull.Rowguid,
                            Class = productReadNull.Class,
                            Color = productReadNull.Color,
                            DaysToManufacture = productReadNull.DaysToManufacture,
                            DiscontinuedDate = productReadNull.DiscontinuedDate,
                            FinishedGoodsFlag = productReadNull.FinishedGoodsFlag,
                            ListPrice = productReadNull.ListPrice,
                            MakeFlag = productReadNull.MakeFlag,
                            ModifiedDate = productReadNull.ModifiedDate,
                            ProductLine = productReadNull.ProductLine,
                            ProductModelId = productReadNull.ProductModelId,
                            ProductSubcategoryId = productReadNull.ProductSubcategoryId,
                            ReorderPoint = productReadNull.ReorderPoint,
                            SafetyStockLevel = productReadNull.SafetyStockLevel,
                            SellEndDate = productReadNull.SellEndDate,
                            SellStartDate = productReadNull.SellStartDate,
                            Size = productReadNull.Size,
                            SizeUnitMeasureCode = productReadNull.SizeUnitMeasureCode,
                            StandardCost = productReadNull.StandardCost,
                            Style = productReadNull.Style,
                            UserIdentifier = productReadNull.UserIdentifier,
                            Weight = productReadNull.Weight,
                            WeightUnitMeasureCode = productReadNull.WeightUnitMeasureCode,
                         //   ProductModelName = productReadNull.ProductSubcategory.Product.ToString(),
                         //   ProductSubcategoryName = productReadNull.ProductSubcategory.Name.ToString()
                            //   ProductModel = modelList.LastOrDefault().Name
                            //    ProductSubcategory = productReadNull.ProductSubcategory
                        });
                    }
                }

               return productReads;
            }
     
        }

        public ProductRead GetProduct(long productId)
        {
            ProductRead productRead = null;
            using (var context = new ApiContext(_optionsBuilder.Options))
            {
                var productReadNull = context.Product.Where(c => c.ProductId == productId);
                foreach (var ProductReadNull in productReadNull)
                {

                    productRead = new ProductRead(productId)
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
