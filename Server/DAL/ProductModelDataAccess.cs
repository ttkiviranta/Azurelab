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
    public class ProductModelDataAccess
    {
        public ProductModelDataAccess(IConfiguration configuration)
        {
            Configuration = configuration;
            _optionsBuilder.UseSqlServer(Helpers.GetSqlConnection());
        }

        IConfiguration Configuration { get; set; }
        private readonly DbContextOptionsBuilder<ApiContext> _optionsBuilder = new DbContextOptionsBuilder<ApiContext>();

        public ICollection<ProductModel> GetProductModels()
        {
            var ProductModels = new List<ProductModel>();
            using (var context = new ApiContext(_optionsBuilder.Options))
            {
                var uniqueProductModelNull = context.ProductModel.OrderBy(c => c.ProductModelId).ToList();
                foreach (var ProductModelNull in uniqueProductModelNull)
                {
                    ProductModels.Add(new ProductModel(ProductModelNull.ProductModelId)
                    {
                        ProductModelId = ProductModelNull.ProductModelId,
                        Name = ProductModelNull.Name,
                        CatalogDescription = ProductModelNull.CatalogDescription,
                        Instructions =ProductModelNull.Instructions,
                        Rowguid = ProductModelNull.Rowguid,
                        ModifiedDate = ProductModelNull.ModifiedDate
                        
                    });
                }
            }
            return ProductModels;
        }

    }
}
