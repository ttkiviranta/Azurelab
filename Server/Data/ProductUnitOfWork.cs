using Server.DAL;
using System.Data.SqlClient;
using Server.DataRead;
using Microsoft.EntityFrameworkCore;

namespace Server.Data
{
    public class ProductUnitOfWork : IProductUnitOfWork
    {
        readonly ApiContext _context;
        
        public ProductUnitOfWork(ApiContext context)
        {
          //  context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [Homelab].[Production].[Product] ON;");
            _context = context;
         

            Products = new ProductRepository(_context);
     //       ProductInsert = new ProductInsertRepository(_context);
      //      ProductsReadNull = new ProductReadRepository(_context);
       //     ProductLockedStatuses = new ProductLockedStatusRepository(_context);
       //     ProductOnlineStatuses = new ProductOnlineStatusRepository(_context);
       //     ProductSpeeds = new ProductSpeedRepository(_context);
            
        }

        public void Dispose()
        {
            _context.Dispose();
        }

    //    public IProductRepository Products { get; private set; }
        public IProductRepository Products { get; set; }
        //  public IProductReadRepository ProductsReadNull { get; private set; }
    //      public IProductInsertRepository ProductInsert { get; private set; }
        //  public IProductLockedStatusRepository ProductLockedStatuses { get; private set; }
        //    public IProductOnlineStatusRepository ProductOnlineStatuses { get; private set; }
        //    public IProductSpeedRepository ProductSpeeds { get; private set; }

        public int Complete()
        {
        //    _context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [Homelab].[Production].[Product] ON;");
           
            return _context.SaveChanges(); //Tähän virheen käsittely
        //    _context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [Homelab].[Production].[Product]  OFF;");
        }

        public int ClearDatabase()
        {
      //      _context.RemoveRange(_context.ProductLockedStatuses);
      //      _context.RemoveRange(_context.ProductLockedStatusesRead);
      //      _context.RemoveRange(_context.ProductOnlineStatuses);
      //      _context.RemoveRange(_context.ProductOnlineStatusesRead);
    //        _context.RemoveRange(_context.ProductReadNulls);
     //       _context.RemoveRange(_context.ProductInsert);
            _context.RemoveRange(_context.Product);
            //      _context.RemoveRange(_context.ProductSpeeds);
            //     _context.RemoveRange(_context.ProductSpeedsRead);
            //     _context.EnsureSeedData();
            _context.SaveChanges();
            return _context.SaveChanges();
        }
    }
}

