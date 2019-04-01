﻿using Server.DAL;
using Shared.Models.Write;

namespace Server.Data
{
    public interface IProductOnlineStatusRepository : IRepository<ProductOnlineStatus>
    {
        ApiContext ApiContext { get; }
    }
}
