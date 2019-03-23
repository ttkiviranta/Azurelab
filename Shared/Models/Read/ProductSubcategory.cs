using System;
using System.Collections.Generic;
using Shared.Models.Write;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models.Read
{
    public partial class ProductSubcategory
    {
        public ProductSubcategory()
        {
            Product = new HashSet<Product>();
        }

        public ProductSubcategory(int productSubcategoryId)
        {
            ProductSubcategoryId = productSubcategoryId;
        }

        public int ProductSubcategoryId { get; set; }
        public int ProductCategoryId { get; set; }
        public string Name { get; set; }
        public Guid Rowguid { get; set; }
        public DateTime ModifiedDate { get; set; }

        public ProductCategory ProductCategory { get; set; }
        public ICollection<Product> Product { get; set; }
    }
}
