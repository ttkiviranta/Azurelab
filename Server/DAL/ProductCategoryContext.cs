using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shared.Models.Read;
using Microsoft.EntityFrameworkCore;

namespace Server.DAL
{
    public class ProductCategoryContext :DbContext 
    {
        public ProductCategoryContext(DbContextOptions options)
            : base(options)
        {
        }
     //   public DbSet<ProductCategory> ProductCategories { get; set; }
    }
}
