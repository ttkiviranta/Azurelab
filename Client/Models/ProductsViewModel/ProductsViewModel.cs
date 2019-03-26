using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Models.ProductsViewModel
{
    public class ProductsViewModel 
    {
        public List<Product> Products { get; set; }
        public List<ProductModel> ProductModels { get; set; }
        public List<ProductCategory> ProductCategories { get; set; }
        public List<ProductSubcategory> ProductSubcategories { get; set; }
    }
}
