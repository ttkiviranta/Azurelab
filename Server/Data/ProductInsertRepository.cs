using Shared.Models.Insert;
using Server.DAL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Collections.Generic;

namespace Server.Data
{
    public class ProductInsertRepository : Repository<ProductInsert>, IProductInsertRepository
    {
        public ProductInsertRepository(ApiContext context) : base(context)
        {
            context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [Homelab].[Production].[Product]  ON;");
        }

        public ApiContext ApiContext => Context as ApiContext;
    }
}
