using Microsoft.EntityFrameworkCore;
using Shared.Models.Write;
using Shared.Models.Read;
using System.Threading.Tasks;

namespace Server.DAL
{
    public class ApiContext : DbContext
    {
        public ApiContext(DbContextOptions options)
          : base(options)
        {
        }

     //   public virtual DbSet<Audit> Audit { get; set; }
        public virtual DbSet<Product> Products { get; set; }
     /*   public virtual DbSet<ProductCategory> ProductCategory { get; set; }
        public virtual DbSet<ProductModel> ProductModel { get; set; }
        public virtual DbSet<ProductSubcategory> ProductSubcategory { get; set; }
        public virtual DbSet<UnitMeasure> UnitMeasure { get; set; }*/
    }
}
