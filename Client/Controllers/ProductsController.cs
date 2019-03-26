namespace Client.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Client.Models;
    using Client.Models.ProductsViewModel;
    using Client.Models.ProductViewModel;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc.Rendering;

    //   [Produces("application/json")]
    //   [Route("api/Products")]

    public class ProductsController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ProductsController(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        // GET: Products

        public async Task<IActionResult> Index()
        {


            ViewBag.ApiAddress = _httpContextAccessor.HttpContext.Session.GetString("ApiAddress");

            List<Product> products;
            List<ProductSubcategory> subcategories;
            List<ProductCategory> categories;
            List<ProductModel> models;

            try
            {
                products = await Utils.Get<List<Product>>("api/Product", _httpContextAccessor.HttpContext.Session.GetString("ApiAddress"));
                subcategories = await Utils.Get<List<ProductSubcategory>>("/api/read/Subcategory", _httpContextAccessor.HttpContext.Session.GetString("ApiAddress"));
                categories = await Utils.Get<List<ProductCategory>>("/api/read/Category", _httpContextAccessor.HttpContext.Session.GetString("ApiAddress"));
                models = await Utils.Get<List<ProductModel>>("/api/read/Model", _httpContextAccessor.HttpContext.Session.GetString("ApiAddress"));
            }
            catch
            {
                TempData["CustomError"] = "No contact with server! AzurelabAPI must be started before Client could start!";
                return View(new ProductsViewModel { Products = new List<Product>() });
            }

            var productsViewModel = new ProductsViewModel { Products = products, ProductSubcategories = subcategories, ProductCategories = categories, ProductModels = models };
            return View(productsViewModel);
        }

        public async Task<IActionResult> Details(int? id)
        {
            ViewBag.ApiAddress = _httpContextAccessor.HttpContext.Session.GetString("ApiAddress");

            List<ProductSubcategory> subcategories;
            List<ProductCategory> categories;
            List<ProductModel> models;

            var product = await Utils.Get<Product>("api/Product/" + id, _httpContextAccessor.HttpContext.Session.GetString("ApiAddress"));
            subcategories = await Utils.Get<List<ProductSubcategory>>("/api/read/Subcategory", _httpContextAccessor.HttpContext.Session.GetString("ApiAddress"));
            categories = await Utils.Get<List<ProductCategory>>("/api/read/Category", _httpContextAccessor.HttpContext.Session.GetString("ApiAddress"));
            models = await Utils.Get<List<ProductModel>>("/api/read/Model", _httpContextAccessor.HttpContext.Session.GetString("ApiAddress"));
        
            var productViewModel = new ProductViewModel { Product = product, ProductSubcategories = subcategories, ProductCategories = categories, ProductModels = models };
            return View(productViewModel);
           
        }

        public async Task<IActionResult> Edit(int? id)
        {
            ViewBag.ApiAddress = _httpContextAccessor.HttpContext.Session.GetString("ApiAddress");

            List<ProductSubcategory> subcategories;
            List<ProductCategory> categories;
            List<ProductModel> models;
            List<UnitMeasure> unitmeasures;
            //List<WeightMeasure> unitmeasures;


            var product = await Utils.Get<Product>("api/Product/" + id, _httpContextAccessor.HttpContext.Session.GetString("ApiAddress"));

            subcategories = await Utils.Get<List<ProductSubcategory>>("/api/read/Subcategory", _httpContextAccessor.HttpContext.Session.GetString("ApiAddress"));
            ViewData["ProductSubcategoryId"] = new SelectList(subcategories, "ProductSubcategoryId", "Name");

            categories = await Utils.Get<List<ProductCategory>>("/api/read/Category", _httpContextAccessor.HttpContext.Session.GetString("ApiAddress"));
          
            models = await Utils.Get<List<ProductModel>>("/api/read/Model", _httpContextAccessor.HttpContext.Session.GetString("ApiAddress"));
            ViewData["ProductModelId"] = new SelectList(models, "ProductModelId", "Name");

            unitmeasures = await Utils.Get<List<UnitMeasure>>("/api/read/UnitMeasure", _httpContextAccessor.HttpContext.Session.GetString("ApiAddress"));
            ViewData["SizeUnitMeasureCode"] = new SelectList(unitmeasures, "UnitMeasureCode", "Name"); 

           // unitmeasures = await Utils.Get<List<UnitMeasure>>("/api/read/Model", _httpContextAccessor.HttpContext.Session.GetString("ApiAddress"));
            ViewData["WeightUnitMeasureCode"] = new SelectList(unitmeasures, "UnitMeasureCode", "Name");

            // var productViewModel = new ProductViewModel { Product = product, ProductSubcategories = subcategories, ProductCategories = categories, ProductModels = models };
            return View(product);

        }


        public IActionResult Delete(int? id)
        {
            // var product = await Utils.Get<List<Product>>("api/Product/" + id.ToString(), _httpContextAccessor.HttpContext.Session.GetString("ApiAddress"));            
            // return View(product);
            return View();
        }

    }
}