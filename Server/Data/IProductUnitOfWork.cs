using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Data
{
    internal interface IProductUnitOfWork : IDisposable
    {
        int Complete();
        int ClearDatabase();
    }
}
