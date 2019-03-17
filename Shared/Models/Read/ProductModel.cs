using System;
using System.Collections.Generic;
using Shared.Models.Write;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models.Read
{
    public partial class ProductModel
    {
        public ProductModel()
        {
            
            Product = new HashSet<Product>();
        }

        public int ProductModelId { get; set; }
        public string Name { get; set; }
        public string CatalogDescription { get; set; }
        public string Instructions { get; set; }
        public Guid Rowguid { get; set; }
        public DateTime ModifiedDate { get; set; }

       
        public ICollection<Product> Product { get; set; }
    }
}
