using Microsoft.EntityFrameworkCore;
using Shared.Models.Write;
using Shared.Models.Read;
using Shared.Models.Insert;
using System.Threading.Tasks;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Server.DAL
{
    //  [Schema = "Production"]
    [Table("Product", Schema = "Production")]

    public partial class ApiContext : DbContext
    {



        //   public virtual DbSet<Audit> Audit { get; set; }
        public virtual DbSet<ProductRead> ProductRead { get; set; }
        public virtual DbSet<ProductInsert> ProductInsert { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<ProductReadNull> ProductReadNulls { get; set; }
        public virtual DbSet<ProductCategory> ProductCategory { get; set; }
        public virtual DbSet<ProductModel> ProductModel { get; set; }
        public virtual DbSet<ProductSubcategory> ProductSubcategory { get; set; }
        public virtual DbSet<UnitMeasure> UnitMeasure { get; set; }

        public virtual DbSet<ProductOnlineStatus> ProductOnlineStatuses { get; set; }
        public virtual DbSet<ProductOnlineStatusRead> ProductOnlineStatusesRead { get; set; }
        public virtual DbSet<ProductLockedStatus> ProductLockedStatuses { get; set; }
        public virtual DbSet<ProductLockedStatusRead> ProductLockedStatusesRead { get; set; }


        public ApiContext(DbContextOptions options)
             : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("Production");
            modelBuilder.Entity<Product>().HasKey(s => s.ProductId);
            modelBuilder.Entity<Product>(entity =>
          {
              entity.ToTable("Product", "Production");

              entity.Property(e => e.ProductId).HasColumnName("ProductID");

              entity.Property(e => e.Class).HasColumnType("nchar(2)");

              entity.Property(e => e.Color).HasMaxLength(15);

              entity.Property(e => e.DiscontinuedDate).HasColumnType("datetime");

              entity.Property(e => e.FinishedGoodsFlag)
                  .HasColumnType("Flag")
                  .HasDefaultValueSql("((1))");

              entity.Property(e => e.ListPrice).HasColumnType("money");

              entity.Property(e => e.MakeFlag)
                  .HasColumnType("Flag")
                  .HasDefaultValueSql("((1))");

              entity.Property(e => e.ModifiedDate)
                  .HasColumnType("datetime")
                  .HasDefaultValueSql("getdate()");

              entity.Property(e => e.Name)
                  .IsRequired()
                  .HasColumnType("Name")
                  .HasMaxLength(4000);

              entity.Property(e => e.ProductLine).HasColumnType("nchar(2)");

              entity.Property(e => e.ProductModelId).HasColumnName("ProductModelID");

              entity.Property(e => e.ProductNumber)
                  .IsRequired()
                  .HasMaxLength(25);

              entity.Property(e => e.ProductSubcategoryId).HasColumnName("ProductSubcategoryID");

              entity.Property(e => e.Rowguid)
                  .HasColumnName("rowguid")
                  .HasDefaultValueSql("(newid())");

              entity.Property(e => e.SellEndDate).HasColumnType("datetime");

              entity.Property(e => e.SellStartDate).HasColumnType("datetime");

              entity.Property(e => e.Size).HasMaxLength(5);

              entity.Property(e => e.SizeUnitMeasureCode).HasColumnType("nchar(3)");

              entity.Property(e => e.StandardCost).HasColumnType("money");

              entity.Property(e => e.Style).HasColumnType("nchar(2)");

              entity.Property(e => e.UserIdentifier)
                  .HasMaxLength(20)
                  .HasColumnName("UserIdentifier")
                  .HasDefaultValueSql("Tim...");


              entity.Property(e => e.Weight).HasColumnType("decimal(8, 2)");

              entity.Property(e => e.WeightUnitMeasureCode).HasColumnType("nchar(3)");

              entity.HasOne(d => d.ProductModel)
                  .WithMany(p => p.Product)
                  .HasForeignKey(d => d.ProductModelId);

              entity.HasOne(d => d.ProductSubcategory)
                  .WithMany(p => p.Product)
                  .HasForeignKey(d => d.ProductSubcategoryId);

              entity.HasOne(d => d.SizeUnitMeasureCodeNavigation)
                  .WithMany(p => p.ProductSizeUnitMeasureCodeNavigation)
                  .HasForeignKey(d => d.SizeUnitMeasureCode);

              entity.HasOne(d => d.WeightUnitMeasureCodeNavigation)
                  .WithMany(p => p.ProductWeightUnitMeasureCodeNavigation)
                  .HasForeignKey(d => d.WeightUnitMeasureCode);
          });
            modelBuilder.Entity<ProductModel>(entity =>
            {
                entity.ToTable("ProductModel", "Production");

                entity.Property(e => e.ProductModelId).HasColumnName("ProductModelID");

                entity.Property(e => e.CatalogDescription).HasColumnType("xml");

                entity.Property(e => e.Instructions).HasColumnType("xml");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("Name")
                    .HasMaxLength(4000);

                entity.Property(e => e.Rowguid)
                    .HasColumnName("rowguid")
                    .HasDefaultValueSql("(newid())");
            });

            modelBuilder.Entity<ProductCategory>(entity =>
            {
                entity.ToTable("ProductCategory", "Production");

                entity.Property(e => e.ProductCategoryId).HasColumnName("ProductCategoryID");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("Name")
                    .HasMaxLength(4000);

                entity.Property(e => e.Rowguid)
                    .HasColumnName("rowguid")
                    .HasDefaultValueSql("(newid())");
            });

            modelBuilder.Entity<ProductSubcategory>(entity =>
            {
                entity.ToTable("ProductSubcategory", "Production");

                entity.Property(e => e.ProductSubcategoryId).HasColumnName("ProductSubcategoryID");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("Name")
                    .HasMaxLength(4000);

                entity.Property(e => e.ProductCategoryId).HasColumnName("ProductCategoryID");

                entity.Property(e => e.Rowguid)
                    .HasColumnName("rowguid")
                    .HasDefaultValueSql("(newid())");

                entity.HasOne(d => d.ProductCategory)
                    .WithMany(p => p.ProductSubcategory)
                    .HasForeignKey(d => d.ProductCategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<UnitMeasure>(entity =>
            {
                entity.HasKey(e => e.UnitMeasureCode);

                entity.ToTable("UnitMeasure", "Production");

                entity.Property(e => e.UnitMeasureCode)
                    .HasColumnType("nchar(3)")
                    .ValueGeneratedNever();

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("Name")
                    .HasMaxLength(4000);
            });
        }

    }
}