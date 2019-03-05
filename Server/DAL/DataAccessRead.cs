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
                            ProductId = ProductReadNull.ProductId,
                            ProductNumber = ProductReadNull.ProductNumber,

                        });
                    }
                }

                return ProductReads;
            }
     
        }  
    }
}
