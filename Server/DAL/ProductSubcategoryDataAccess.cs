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
    public class ProductSubcategoryDataAccess
    {
        public ProductSubcategoryDataAccess(IConfiguration configuration)
        {
            Configuration = configuration;
            _optionsBuilder.UseSqlServer(Helpers.GetSqlConnection());
        }

        IConfiguration Configuration { get; set; }
        private readonly DbContextOptionsBuilder<ApiContext> _optionsBuilder = new DbContextOptionsBuilder<ApiContext>();

        public ICollection<ProductSubcategory> GetProductSubcategories()
        {
            var ProductSubcategories = new List<ProductSubcategory>();
            using (var context = new ApiContext(_optionsBuilder.Options))
            {
                var uniqueProductSubcategoryNull = context.ProductSubcategory.OrderBy(c => c.ProductSubcategoryId).ToList();
                foreach (var ProductSubcategoryNull in uniqueProductSubcategoryNull)
                {
                    ProductSubcategories.Add(new ProductSubcategory(ProductSubcategoryNull.ProductSubcategoryId)
                    {
                        ProductSubcategoryId = ProductSubcategoryNull.ProductSubcategoryId,
                        ProductCategoryId = ProductSubcategoryNull.ProductCategoryId,
                        Name = ProductSubcategoryNull.Name,
                        Rowguid = ProductSubcategoryNull.Rowguid,
                        ModifiedDate = ProductSubcategoryNull.ModifiedDate
                    });
                }
            }
            return ProductSubcategories;
        }
       
    }
}

