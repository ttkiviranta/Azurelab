using System;
using System.Collections.Generic;
using Shared.Models.Write;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models.Read
{
    public partial class UnitMeasure
    {
        public UnitMeasure()
        {
            ProductSizeUnitMeasureCodeNavigation = new HashSet<Product>();
            ProductWeightUnitMeasureCodeNavigation = new HashSet<Product>();
        }

        public string UnitMeasureCode { get; set; }
        public string Name { get; set; }
        public DateTime ModifiedDate { get; set; }

       
        public ICollection<Product> ProductSizeUnitMeasureCodeNavigation { get; set; }
        public ICollection<Product> ProductWeightUnitMeasureCodeNavigation { get; set; }
    }
}
