using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shared.Models.Read;
using Microsoft.EntityFrameworkCore;

namespace Server.DAL
{
    public class ProductModelContext : DbContext
    {
        public ProductModelContext(DbContextOptions options)
            : base(options)
        {
            
        }
        public DbSet<ProductModel> ProductModels { get; set; }
    }
}
