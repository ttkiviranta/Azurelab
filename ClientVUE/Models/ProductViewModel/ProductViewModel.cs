using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Models.ProductViewModel
{
    public class ProductViewModel
    {
       
        public Product Product { get; set; }
        public List<ProductModel> ProductModels { get; set; }
        public List<ProductCategory> ProductCategories { get; set; }
        public List<ProductSubcategory> ProductSubcategories { get; set; }
        public List <UnitMeasure> UnitMeasures { get; set; }
    }
}
