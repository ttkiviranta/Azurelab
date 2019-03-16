using Shared.Models.Write;
using Server.DAL;
using Microsoft.EntityFrameworkCore;

namespace Server.Data
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(DbContext context) : base(context)
        {
        }

        public ApiContext ApiContext => Context as ApiContext;

    }
}
