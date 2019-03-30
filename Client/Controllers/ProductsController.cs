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

        List<Product> products;
        List<ProductSubcategory> subcategories;
        List<ProductCategory> categories;
        List<ProductModel> models;
        List<UnitMeasure> unitmeasures;

        public ProductsController(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        // GET: Products

        public async Task<IActionResult> Index()
        {


            ViewBag.ApiAddress = _httpContextAccessor.HttpContext.Session.GetString("ApiAddress");


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

        public async Task<IActionResult> Details(long? id)
        {
            ViewBag.ApiAddress = _httpContextAccessor.HttpContext.Session.GetString("ApiAddress");

            List<ProductSubcategory> subcategories;
            List<ProductCategory> categories;
            List<ProductModel> models;
            List<UnitMeasure> unitmeasures;

            var product = await Utils.Get<Product>("api/Product/" + id, _httpContextAccessor.HttpContext.Session.GetString("ApiAddress"));
            subcategories = await Utils.Get<List<ProductSubcategory>>("/api/read/Subcategory", _httpContextAccessor.HttpContext.Session.GetString("ApiAddress"));
            categories = await Utils.Get<List<ProductCategory>>("/api/read/Category", _httpContextAccessor.HttpContext.Session.GetString("ApiAddress"));
            models = await Utils.Get<List<ProductModel>>("/api/read/Model", _httpContextAccessor.HttpContext.Session.GetString("ApiAddress"));
            unitmeasures = await Utils.Get<List<UnitMeasure>>("/api/read/UnitMeasure", _httpContextAccessor.HttpContext.Session.GetString("ApiAddress"));

            ViewData["ProductSubcategoryId"] = new SelectList(subcategories, "ProductSubcategoryId", "Name");
            ViewData["ProductModelId"] = new SelectList(models, "ProductModelId", "Name");
            ViewData["SizeUnitMeasureCode"] = new SelectList(unitmeasures, "UnitMeasureCode", "Name");
            ViewData["WeightUnitMeasureCode"] = new SelectList(unitmeasures, "UnitMeasureCode", "Name");

            var productViewModel = new ProductViewModel { Product = product, ProductSubcategories = subcategories, ProductCategories = categories, ProductModels = models };
            return View(productViewModel);
           
        }

        // GET: Product/Create
        public async Task<IActionResult> Create()
        {

            List<ProductSubcategory> subcategories;
            List<ProductCategory> categories;
            List<ProductModel> models;
            List<UnitMeasure> unitmeasures;

            subcategories = await Utils.Get<List<ProductSubcategory>>("/api/read/Subcategory", _httpContextAccessor.HttpContext.Session.GetString("ApiAddress"));
            ViewData["ProductSubcategoryId"] = new SelectList(subcategories, "ProductSubcategoryId", "Name");

            categories = await Utils.Get<List<ProductCategory>>("/api/read/Category", _httpContextAccessor.HttpContext.Session.GetString("ApiAddress"));

            models = await Utils.Get<List<ProductModel>>("/api/read/Model", _httpContextAccessor.HttpContext.Session.GetString("ApiAddress"));
            ViewData["ProductModelId"] = new SelectList(models, "ProductModelId", "Name");

            unitmeasures = await Utils.Get<List<UnitMeasure>>("/api/read/UnitMeasure", _httpContextAccessor.HttpContext.Session.GetString("ApiAddress"));
            ViewData["SizeUnitMeasureCode"] = new SelectList(unitmeasures, "UnitMeasureCode", "Name");

             unitmeasures = await Utils.Get<List<UnitMeasure>>("/api/read/Model", _httpContextAccessor.HttpContext.Session.GetString("ApiAddress"));
            ViewData["WeightUnitMeasureCode"] = new SelectList(unitmeasures, "UnitMeasureCode", "Name");
        //    ViewData["id"] = -1;
            return View();
        }

        // POST: Product/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,Name,ProductNumber,MakeFlag,FinishedGoodsFlag,Color,SafetyStockLevel,ReorderPoint,StandardCost,ListPrice,Size,SizeUnitMeasureCode,WeightUnitMeasureCode,Weight,DaysToManufacture,ProductLine,Class,Style,ProductSubcategoryId,ProductModelId,SellStartDate,SellEndDate,DiscontinuedDate,Rowguid,ModifiedDate,UserIdentifier")] ProductInsert product)
        {
            if (!ModelState.IsValid) return View(product);

         /*     var newProduct = new ProductInsert();

              newProduct.Name = product.Name;


              newProduct.ProductId = product.ProductId;
              newProduct.ProductNumber = product.ProductNumber;

              newProduct.Rowguid = product.Rowguid;
              newProduct.Class = product.Class;
              newProduct.Color = product.Color;
              newProduct.DaysToManufacture = product.DaysToManufacture;
              newProduct.DiscontinuedDate = product.DiscontinuedDate;
              newProduct.FinishedGoodsFlag = product.FinishedGoodsFlag;
              newProduct.ListPrice = product.ListPrice;
              newProduct.MakeFlag = product.MakeFlag;
              newProduct.ModifiedDate = product.ModifiedDate;
              newProduct.ProductLine = product.ProductLine;
              newProduct.ProductModelId = product.ProductModelId;
              newProduct.ProductSubcategoryId = product.ProductSubcategoryId;
              newProduct.ReorderPoint = product.ReorderPoint;
              newProduct.SafetyStockLevel = product.SafetyStockLevel;
              newProduct.SellEndDate = product.SellEndDate;
              newProduct.SellStartDate = product.SellStartDate;
              newProduct.Size = product.Size;
              newProduct.SizeUnitMeasureCode = product.SizeUnitMeasureCode;
              newProduct.StandardCost = product.StandardCost;
              newProduct.Style = product.Style;
              newProduct.UserIdentifier = product.UserIdentifier;
              newProduct.Weight = product.Weight;
              newProduct.WeightUnitMeasureCode = product.WeightUnitMeasureCode;*/
            product.ProductId = -1;
            await Utils.Post<ProductInsert>("api/Product/" , product, _httpContextAccessor.HttpContext.Session.GetString("ApiAddress"));

            return RedirectToAction("Index", new { id = product.ProductId + "|pending create" + "|" + product.Name });
        }

        // GET: Product/Edit/5
        public async Task<IActionResult> Edit(long? id)
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

            unitmeasures = await Utils.Get<List<UnitMeasure>>("/api/read/Model", _httpContextAccessor.HttpContext.Session.GetString("ApiAddress"));
            ViewData["WeightUnitMeasureCode"] = new SelectList(unitmeasures, "UnitMeasureCode", "Name");

            //var productViewModel = new ProductViewModel { Product = product, ProductSubcategories = subcategories, ProductCategories = categories, ProductModels = models };
            return View(product);

        }

        // POST: Product/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long productId, [Bind("ProductId,Name,ProductNumber,MakeFlag,FinishedGoodsFlag,Color,SafetyStockLevel,ReorderPoint,StandardCost,ListPrice,Size,SizeUnitMeasureCode,WeightUnitMeasureCode,Weight,DaysToManufacture,ProductLine,Class,Style,ProductSubcategoryId,ProductModelId,SellStartDate,SellEndDate,DiscontinuedDate,Rowguid,ModifiedDate,UserIdentifier")] Product product)
        {
            if (!ModelState.IsValid) return View(product);
            var oldProduct = await Utils.Get<Product>("api/Product/" + productId, _httpContextAccessor.HttpContext.Session.GetString("ApiAddress"));

            oldProduct.Name = product.Name;


          //  oldProduct.ProductId = product.ProductId;
            oldProduct.ProductNumber = product.ProductNumber;

            oldProduct.Rowguid = product.Rowguid;
            oldProduct.Class = product.Class;
            oldProduct.Color = product.Color;
            oldProduct.DaysToManufacture = product.DaysToManufacture;
            oldProduct.DiscontinuedDate = product.DiscontinuedDate;
            oldProduct.FinishedGoodsFlag = product.FinishedGoodsFlag;
            oldProduct.ListPrice = product.ListPrice;
            oldProduct.MakeFlag = product.MakeFlag;
            oldProduct.ModifiedDate = product.ModifiedDate;
            oldProduct.ProductLine = product.ProductLine;
            oldProduct.ProductModelId = product.ProductModelId;
            oldProduct.ProductSubcategoryId = product.ProductSubcategoryId;
            oldProduct.ReorderPoint = product.ReorderPoint;
            oldProduct.SafetyStockLevel = product.SafetyStockLevel;
            oldProduct.SellEndDate = product.SellEndDate;
            oldProduct.SellStartDate = product.SellStartDate;
            oldProduct.Size = product.Size;
            oldProduct.SizeUnitMeasureCode = product.SizeUnitMeasureCode;
            oldProduct.StandardCost = product.StandardCost;
            oldProduct.Style = product.Style;
            oldProduct.UserIdentifier = product.UserIdentifier;
            oldProduct.Weight = product.Weight;
            oldProduct.WeightUnitMeasureCode = product.WeightUnitMeasureCode;

            await Utils.Put<Product>("api/Product/" + oldProduct.ProductId, oldProduct, _httpContextAccessor.HttpContext.Session.GetString("ApiAddress"));

            return RedirectToAction("Index", new { id = oldProduct.ProductId + "|pending update" + "|" + product.Name });
        }

        // GET: Company/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            ViewBag.ApiAddress = _httpContextAccessor.HttpContext.Session.GetString("ApiAddress");

            List<ProductSubcategory> subcategories;
            List<ProductCategory> categories;
            List<ProductModel> models;
            List<UnitMeasure> unitmeasures;

            var product = await Utils.Get<Product>("api/Product/" + id, _httpContextAccessor.HttpContext.Session.GetString("ApiAddress"));
            subcategories = await Utils.Get<List<ProductSubcategory>>("/api/read/Subcategory", _httpContextAccessor.HttpContext.Session.GetString("ApiAddress"));
            categories = await Utils.Get<List<ProductCategory>>("/api/read/Category", _httpContextAccessor.HttpContext.Session.GetString("ApiAddress"));
            models = await Utils.Get<List<ProductModel>>("/api/read/Model", _httpContextAccessor.HttpContext.Session.GetString("ApiAddress"));
            unitmeasures = await Utils.Get<List<UnitMeasure>>("/api/read/UnitMeasure", _httpContextAccessor.HttpContext.Session.GetString("ApiAddress"));

            ViewData["ProductSubcategoryId"] = new SelectList(subcategories, "ProductSubcategoryId", "Name");
            ViewData["ProductModelId"] = new SelectList(models, "ProductModelId", "Name");
            ViewData["SizeUnitMeasureCode"] = new SelectList(unitmeasures, "UnitMeasureCode", "Name");
            ViewData["WeightUnitMeasureCode"] = new SelectList(unitmeasures, "UnitMeasureCode", "Name");

            var productViewModel = new ProductViewModel { Product = product, ProductSubcategories = subcategories, ProductCategories = categories, ProductModels = models };
            return View(productViewModel);
        }

        // POST: Company/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            await Utils.Delete<Product>("api/Product/" + id, _httpContextAccessor.HttpContext.Session.GetString("ApiAddress"));
            return RedirectToAction("Index", new { id = id + "|pending delete" });
        }

        private async Task<bool> ProductExists(long id)
        {
            var products = await Utils.Get<List<Product>>("api/Product", _httpContextAccessor.HttpContext.Session.GetString("ApiAddress"));
            return products.Any(e => e.ProductId == id);
        }
    }
}