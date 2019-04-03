using Server.DAL;
using System.Data.SqlClient;
//using Server.DataRead;
using Microsoft.EntityFrameworkCore;

namespace Server.Data
{
    public class ProductUnitOfWork : IProductUnitOfWork
    {
        readonly ApiContext _context;

        public ProductUnitOfWork(ApiContext context)
        {
            _context = context;
            Products = new ProductRepository(_context);
            ProductLockedStatuses = new ProductLockedStatusRepository(_context);
            ProductOnlineStatuses = new ProductOnlineStatusRepository(_context);
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public IProductRepository Products { get; private set; }
        public IProductLockedStatusRepository ProductLockedStatuses { get; private set; }
        public IProductOnlineStatusRepository ProductOnlineStatuses { get; private set; }

        public int Complete()
        {
            return _context.SaveChanges(); //Tähän virheen käsittely
        }

        public int ClearDatabase()
        {
            _context.RemoveRange(_context.Product);
            _context.RemoveRange(_context.ProductLockedStatuses);
            _context.RemoveRange(_context.ProductOnlineStatuses);
            _context.SaveChanges();
            return _context.SaveChanges();
        }
    }
}

