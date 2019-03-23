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
    public class ProductCategoryDataAccess
    {
        public ProductCategoryDataAccess(IConfiguration configuration)
        {
            Configuration = configuration;
            _optionsBuilder.UseSqlServer(Helpers.GetSqlConnection());
        }

        IConfiguration Configuration { get; set; }
        private readonly DbContextOptionsBuilder<ApiContext> _optionsBuilder = new DbContextOptionsBuilder<ApiContext>();

        public ICollection<ProductCategory> GetProductCategories()
        {
            var ProductCategories = new List<ProductCategory>();
            using (var context = new ApiContext(_optionsBuilder.Options))
            {
                var uniqueProductCategoryNull = context.ProductCategory.OrderBy(c => c.ProductCategoryId).ToList();
                foreach (var ProductCategoryNull in uniqueProductCategoryNull)
                {
                    ProductCategories.Add(new ProductCategory(ProductCategoryNull.ProductCategoryId)
                    {
                        ProductCategoryId = ProductCategoryNull.ProductCategoryId,
                        Name = ProductCategoryNull.Name,
                        Rowguid = ProductCategoryNull.Rowguid,
                        ModifiedDate = ProductCategoryNull.ModifiedDate
                    });
                }
            }
            return ProductCategories;
        }

    }
}
