namespace Client.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Client.Models;
    using Client.Models.ProductViewModel;
    using Microsoft.AspNetCore.Http;

    [Produces("application/json")]
    [Route("api/Products")]

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
                return View(new ProductViewModel { Products = new List<Product>() });
            }

             //   subcategories = await Utils.Get<List<ProductSubcategory>>("/api/read/Subcategory", _httpContextAccessor.HttpContext.Session.GetString("ApiAddress"));
            //    categories = await Utils.Get<List<ProductCategory>>("/api/read/Category", _httpContextAccessor.HttpContext.Session.GetString("ApiAddress"));
            //    models = await Utils.Get<List<ProductModel>>("/api/read/Model", _httpContextAccessor.HttpContext.Session.GetString("ApiAddress"));

            var productViewModel = new ProductViewModel { Products = products, ProductSubcategories = subcategories, ProductCategories = categories, ProductModels = models };
            return View(productViewModel);
        }
    }
}