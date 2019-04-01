using Server.DAL;
using Shared.Models.Write;

namespace Server.Data
{
    public interface IProductLockedStatusRepository : IRepository<ProductLockedStatus>
    {
        ApiContext ApiContext { get; }
    }
}
