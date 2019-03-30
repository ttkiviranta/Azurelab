using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shared.Models.Read;
using Microsoft.EntityFrameworkCore;

namespace Server.DAL
{
    public class ProductSubcategoryContext :DbContext
    {
        public ProductSubcategoryContext(DbContextOptions options)
           : base(options)
        {
        }
    }
}
