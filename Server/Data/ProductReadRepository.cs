using Shared.Models.Read;
using Server.DAL;
using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Server.Data
{
    public class ProductReadRepository : Repository<ProductReadNull>, IProductReadRepository
    {
        public ProductReadRepository(ApiContext context) : base(context)
        {
       
        }

        public ApiContext ApiContext => Context as ApiContext;
    }
}
