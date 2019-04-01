﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Shared.Models.Read;
using Shared.Models.Write;
using Shared.Models;
using Microsoft.Extensions.Configuration;
using System.IO;
using Shared.Utils;
using System.Data.SqlClient;
using System.Text;

namespace Server.DAL
{
    public class ProductLockedStatusDataAccess
    {
        public ProductLockedStatusDataAccess(IConfiguration configuration)
        {
            Configuration = configuration;
            _optionsBuilder.UseSqlServer(Helpers.GetSqlConnection());
        }

        IConfiguration Configuration { get; set; }
        private readonly DbContextOptionsBuilder<ApiContext> _optionsBuilder = new DbContextOptionsBuilder<ApiContext>();

        public ICollection<ProductLockedStatus> GetLockedStatuses()
        {
            var GetLockedStatuses = new List<ProductLockedStatus>();
            using (var context = new ApiContext(_optionsBuilder.Options))
            {
                var uniqueProductLockedStatusReadNull = context.ProductLockedStatuses.OrderBy(c => c.ProductID).ToList();
                foreach (var ProductLockedStatusReadNull in uniqueProductLockedStatusReadNull)
                {
                    GetLockedStatuses.Add(new ProductLockedStatus(ProductLockedStatusReadNull.ProductID)
                    {
                        ProductID = ProductLockedStatusReadNull.ProductID,
                        Locked = ProductLockedStatusReadNull.Locked,
                        LockedTimeStamp = ProductLockedStatusReadNull.LockedTimeStamp
                    });
                }
            }
            return GetLockedStatuses;
        }

    }
}
