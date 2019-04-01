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
    public class ProductOnlineStatusDataAccess
    {
        public ProductOnlineStatusDataAccess(IConfiguration configuration)
        {
            Configuration = configuration;
            _optionsBuilder.UseSqlServer(Helpers.GetSqlConnection());
        }

        IConfiguration Configuration { get; set; }
        private readonly DbContextOptionsBuilder<ApiContext> _optionsBuilder = new DbContextOptionsBuilder<ApiContext>();

        public ICollection<ProductOnlineStatusRead> GetOnlineStatuses()
        {
            var GetOnlineStatuses = new List<ProductOnlineStatusRead>();
            using (var context = new ApiContext(_optionsBuilder.Options))
            {
                var uniqueProductOnlineStatusReadNull = context.ProductOnlineStatusesRead.OrderBy(c => c.ProductID).ToList();
                foreach (var ProductOnlineStatusReadNull in uniqueProductOnlineStatusReadNull)
                {
                    GetOnlineStatuses.Add(new ProductOnlineStatusRead(ProductOnlineStatusReadNull.ProductID)
                    {
                        ProductID = ProductOnlineStatusReadNull.ProductID,
                        Online = ProductOnlineStatusReadNull.Online,
                        OnlineTimeStamp = ProductOnlineStatusReadNull.OnlineTimeStamp
                    });
                }
            }
            return GetOnlineStatuses;
        }

    }
}

