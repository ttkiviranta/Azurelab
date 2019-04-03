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
using Shared.Models.Write;

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

        public ICollection<ProductOnlineStatus> GetOnlineStatuses()
        {
            var GetOnlineStatuses = new List<ProductOnlineStatus>();
            using (var context = new ApiContext(_optionsBuilder.Options))
            {
                var uniqueProductOnlineStatusReadNull = context.ProductOnlineStatuses.OrderBy(c => c.ProductID).ToList();
                foreach (var ProductOnlineStatusReadNull in uniqueProductOnlineStatusReadNull)
                {
                    GetOnlineStatuses.Add(new ProductOnlineStatus(ProductOnlineStatusReadNull.ProductID)
                    {
                        ProductID = ProductOnlineStatusReadNull.ProductID,
                        Online = ProductOnlineStatusReadNull.Online,
                        OnlineTimeStamp = ProductOnlineStatusReadNull.OnlineTimeStamp,
                        OnlineStatusID = ProductOnlineStatusReadNull.OnlineStatusID
                    });
                }
            }
            return GetOnlineStatuses;
        }

        public ProductOnlineStatus GetOnlineStatus(long productId)
        {
            ProductOnlineStatus productRead = null;
            using (var context = new ApiContext(_optionsBuilder.Options))
            {
                var productReadNull = context.ProductOnlineStatuses.Where(c => c.ProductID == productId);
                foreach (var ProductReadNull in productReadNull)
                {
                    productRead = new ProductOnlineStatus(productId)
                    {
                        ProductID = ProductReadNull.ProductID,
                        Online = ProductReadNull.Online,
                        OnlineTimeStamp = ProductReadNull.OnlineTimeStamp,
                        OnlineStatusID = ProductReadNull.OnlineStatusID
                    };
                }
                return productRead;
            }
        }
    }
}

