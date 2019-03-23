using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shared.Models.Read;
using Microsoft.EntityFrameworkCore;

namespace Server.DAL
{
    public class UnitMeasureContext : DbContext
    {
        public UnitMeasureContext(DbContextOptions options)
           : base(options)
        {
        }
        public DbSet<UnitMeasure> UnitMeasures { get; set; }
    }
}
