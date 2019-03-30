using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Shared.Models.Read;
using Shared.Models;
using Microsoft.Extensions.Configuration;
using System.IO;
using Shared.Utils;
using System.Data.SqlClient;
using System.Text;

namespace Server.DAL
{
    public class UnitMeasureDataAccess
    {
        public UnitMeasureDataAccess(IConfiguration configuration)
        {
            Configuration = configuration;
            _optionsBuilder.UseSqlServer(Helpers.GetSqlConnection());
        }

        IConfiguration Configuration { get; set; }
        private readonly DbContextOptionsBuilder<ApiContext> _optionsBuilder = new DbContextOptionsBuilder<ApiContext>();

        public ICollection<UnitMeasure> GetUnitMeasures()
        {
            var UnitMeasures = new List<UnitMeasure>();
            using (var context = new ApiContext(_optionsBuilder.Options))
            {
                var uniqueUnitMeasureNull = context.UnitMeasure.OrderBy(c => c.UnitMeasureCode).ToList();
                foreach (var UnitMeasureNull in uniqueUnitMeasureNull)
                {
                    UnitMeasures.Add(new UnitMeasure(UnitMeasureNull.UnitMeasureCode)
                    {
                        UnitMeasureCode = UnitMeasureNull.UnitMeasureCode,
                        Name = UnitMeasureNull.Name,
                        ModifiedDate =  UnitMeasureNull.ModifiedDate,
                    });
                }
            }
            return UnitMeasures;
        }
    }
}

