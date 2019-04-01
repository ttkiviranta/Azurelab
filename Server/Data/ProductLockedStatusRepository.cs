using Shared.Models.Write;
using Server.DAL;


namespace Server.Data
{
    public class ProductLockedStatusRepository : Repository<ProductLockedStatus>, IProductLockedStatusRepository
    {
        public ProductLockedStatusRepository(ApiContext context) : base(context)
        {
        }

        public ApiContext ApiContext => Context as ApiContext;
    }
}
