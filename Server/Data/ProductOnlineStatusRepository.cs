using Shared.Models.Write;
using Server.DAL;
using System.Collections.Generic;
using System;
using System.Linq;
namespace Server.Data
{
    public class ProductOnlineStatusRepository : Repository<ProductOnlineStatus>, IProductOnlineStatusRepository
    {
        public ProductOnlineStatusRepository(ApiContext context) : base(context)
        {
        }

        public ApiContext ApiContext => Context as ApiContext;
    }
}
