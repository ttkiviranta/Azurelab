using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NServiceBus;
using Server.DAL;
using Shared.Messages.Commands;
using Shared.Models.Insert;
using Shared.Models.Read;
using Shared.Utils;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace API.Controllers
{

    [Route("")]
    public class DefaultController : Controller
    {


        [Route(""), HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        public RedirectResult RedirectToSwaggerUi()
        {
            return Redirect("/swagger/");
        }
    }

    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        readonly IEndpointInstance _endpointInstance;
        readonly DataAccessWrite _dataAccessWrite;
        readonly DataAccessRead _dataAccessRead;
        readonly DataAccessInsert _dataAccessInsert;


        //public ProductController(IEndpointInstance endpointInstance, IConfiguration configuration)
        public ProductController(IEndpointInstance endpointInstance, IConfiguration configuration)
        {
            _endpointInstance = endpointInstance;
            _dataAccessWrite = new DataAccessWrite(configuration);
            _dataAccessRead = new DataAccessRead(configuration);
            _dataAccessInsert = new DataAccessInsert(configuration);
            // _context = apiContext;
        }

        // GET api/Product
        [EnableCors("AllowAllOrigins")]
        [HttpGet]
        public IEnumerable<ProductRead> GetProducts()
        {
            return _dataAccessRead.GetProducts();
        }

        // GET api/Product/5
        [HttpGet("{id}")]
        public ProductRead GetProduct(long id)
        {
            return _dataAccessRead.GetProduct(id);
        }


        // POST api/Product
        [HttpPost]


        //   public async Task AddProduct([FromHeader] ProductInsert productInsert)
        public async Task AddProduct([Bind("ProductId,Name,ProductNumber,MakeFlag,FinishedGoodsFlag,Color,SafetyStockLevel,ReorderPoint,StandardCost,ListPrice,Size,SizeUnitMeasureCode,WeightUnitMeasureCode,Weight,DaysToManufacture,ProductLine,Class,Style,ProductSubcategoryId,ProductModelId,SellStartDate,SellEndDate,DiscontinuedDate,Rowguid,ModifiedDate,UserIdentifier")] ProductInsert productInsert)
        {

            if (ModelState.IsValid)
            {
                var createProduct = new CreateProduct

                {

                    Rowguid = new Guid(),
                    Class = productInsert.Class,
                    Color = productInsert.Color,
                    DaysToManufacture = productInsert.DaysToManufacture,
                    DiscontinuedDate = productInsert.DiscontinuedDate,
                    FinishedGoodsFlag = productInsert.FinishedGoodsFlag,
                    ListPrice = productInsert.ListPrice,
                    MakeFlag = productInsert.MakeFlag,
                    ModifiedDate = productInsert.ModifiedDate,
                    Name = productInsert.Name,
                    ProductId = productInsert.ProductId,
                    ProductLine = productInsert.ProductLine,
                    ProductModelId = productInsert.ProductModelId,
                    ProductNumber = productInsert.ProductNumber,
                    ProductSubcategoryId = productInsert.ProductSubcategoryId,
                    ReorderPoint = productInsert.ReorderPoint,
                    SafetyStockLevel = productInsert.SafetyStockLevel,
                    SellEndDate = productInsert.SellEndDate,
                    SellStartDate = productInsert.SellStartDate,
                    Size = productInsert.Size,
                    SizeUnitMeasureCode = productInsert.SizeUnitMeasureCode,
                    StandardCost = productInsert.StandardCost,
                    Style = productInsert.Style,
                    UserIdentifier = productInsert.UserIdentifier,
                    Weight = productInsert.Weight,
                    WeightUnitMeasureCode = productInsert.WeightUnitMeasureCode
                };

                await _endpointInstance.Send(Helpers.ServerEndpoint, createProduct).ConfigureAwait(false);
                //         await _endpointInstance.Send(Helpers.ServerEndpoint, createProduct).ConfigureAwait(false);
                //    var result =  _dataAccessInsert.InsertProduct(productInsert);



            }
        }


        // PUT api/Product/5
        [EnableCors("AllowAllOrigins")]
        [HttpPut("/api/Product/")]
        //   [HttpPut("/api/Product/{id}")]
        // public async Task EditProduct([FromBody] ProductRead product)
        public async Task EditProduct([Bind("ProductId,Name,ProductNumber,MakeFlag,FinishedGoodsFlag,Color,SafetyStockLevel,ReorderPoint,StandardCost,ListPrice,Size,SizeUnitMeasureCode,WeightUnitMeasureCode,Weight,DaysToManufacture,ProductLine,Class,Style,ProductSubcategoryId,ProductModelId,SellStartDate,SellEndDate,DiscontinuedDate,Rowguid,ModifiedDate,UserIdentifier")] ProductRead productRead)
        {
           // var oldProduct = GetProduct(productRead.Rowguid.ToString());
          //  if (oldProduct == null) return;
            var updateProduct = new UpdateProduct
            {
                Rowguid = productRead.Rowguid,
                Class = productRead.Class,
                Color = productRead.Color,
                DaysToManufacture = productRead.DaysToManufacture,
                DiscontinuedDate = productRead.DiscontinuedDate,
                FinishedGoodsFlag = productRead.FinishedGoodsFlag,
                ListPrice = productRead.ListPrice,
                MakeFlag = productRead.MakeFlag,
                ModifiedDate = productRead.ModifiedDate,
                Name = productRead.Name,
                ProductId = productRead.ProductId,
                ProductLine = productRead.ProductLine,
                ProductModelId = productRead.ProductModelId,
                ProductNumber = productRead.ProductNumber,
                ProductSubcategoryId = productRead.ProductSubcategoryId,
                ReorderPoint = productRead.ReorderPoint,
                SafetyStockLevel = productRead.SafetyStockLevel,
                SellEndDate = productRead.SellEndDate,
                SellStartDate = productRead.SellStartDate,
                Size = productRead.Size,
                SizeUnitMeasureCode = productRead.SizeUnitMeasureCode,
                StandardCost = productRead.StandardCost,
                Style = productRead.Style,
                UserIdentifier = productRead.UserIdentifier,
                Weight = productRead.Weight,
                WeightUnitMeasureCode = productRead.WeightUnitMeasureCode
            };

            await _endpointInstance.Send(Helpers.ServerEndpoint, updateProduct).ConfigureAwait(false);
        }

        [EnableCors("AllowAllOrigins")]
        [HttpDelete("/api/Product/")]
        //   [HttpPut("/api/Product/{id}")]
        // public async Task EditProduct([FromBody] ProductRead product)
        public async Task DeleteProduct([Bind("ProductId,Name,ProductNumber,MakeFlag,FinishedGoodsFlag,Color,SafetyStockLevel,ReorderPoint,StandardCost,ListPrice,Size,SizeUnitMeasureCode,WeightUnitMeasureCode,Weight,DaysToManufacture,ProductLine,Class,Style,ProductSubcategoryId,ProductModelId,SellStartDate,SellEndDate,DiscontinuedDate,Rowguid,ModifiedDate,UserIdentifier")] ProductRead productRead)
        {
            // var oldProduct = GetProduct(productRead.Rowguid.ToString());
            //  if (oldProduct == null) return;
            var deleteProduct = new DeleteProduct
            {
                Rowguid = productRead.Rowguid,
                Class = productRead.Class,
                Color = productRead.Color,
                DaysToManufacture = productRead.DaysToManufacture,
                DiscontinuedDate = productRead.DiscontinuedDate,
                FinishedGoodsFlag = productRead.FinishedGoodsFlag,
                ListPrice = productRead.ListPrice,
                MakeFlag = productRead.MakeFlag,
                ModifiedDate = productRead.ModifiedDate,
                Name = productRead.Name,
                ProductId = productRead.ProductId,
                ProductLine = productRead.ProductLine,
                ProductModelId = productRead.ProductModelId,
                ProductNumber = productRead.ProductNumber,
                ProductSubcategoryId = productRead.ProductSubcategoryId,
                ReorderPoint = productRead.ReorderPoint,
                SafetyStockLevel = productRead.SafetyStockLevel,
                SellEndDate = productRead.SellEndDate,
                SellStartDate = productRead.SellStartDate,
                Size = productRead.Size,
                SizeUnitMeasureCode = productRead.SizeUnitMeasureCode,
                StandardCost = productRead.StandardCost,
                Style = productRead.Style,
                UserIdentifier = productRead.UserIdentifier,
                Weight = productRead.Weight,
                WeightUnitMeasureCode = productRead.WeightUnitMeasureCode
            };

            await _endpointInstance.Send(Helpers.ServerEndpoint, deleteProduct).ConfigureAwait(false);
        }

    }

}