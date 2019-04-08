using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NServiceBus;
using Server.DAL;
using Shared.Messages.Commands;
using Shared.Models.Insert;
using Shared.Models.Read;
using Shared.Models.Write;
using Shared.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using System.Text;
using Newtonsoft.Json;
using Shared.Messages.Events;

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
        readonly IEndpointInstance _endpointInstancePriority;
    
        public ProductController(IEndpointInstance endpointInstance, IEndpointInstance endpointInstancePriority, IConfiguration configuration)
        {
            _endpointInstance = endpointInstance;
            _dataAccessWrite = new DataAccessWrite(configuration);
            _dataAccessRead = new DataAccessRead(configuration);
            _dataAccessInsert = new DataAccessInsert(configuration);
            _endpointInstancePriority = endpointInstancePriority;
        }

        private static bool ProductIdAlreadyInQueue(Dictionary<string, int> guidQueueLength, string productId)
        {
            return guidQueueLength.Any(a => a.Key == productId);
        }

        private static bool ProductHasBeenLocked40Seconds(ProductRead product)
        {
            return new DateTime(product.LockedTimeStamp).AddMilliseconds(40000) < DateTime.Now;
        }

        // GET api/Product
        [EnableCors("AllowAllOrigins")]
        [HttpGet]
        public async Task<IEnumerable<ProductRead>> GetProductsAsync()
        //  public IEnumerable<ProductRead> GetProducts()
        {
            var list = new List<ProductRead>();
            var products = _dataAccessRead.GetProducts();
            foreach (var product in products)
            {
                if (product.ProductLockedStatus.Locked)
                {
                    if (ProductHasBeenLocked40Seconds(product))
                    { //Lock timed out and can be ignored and set to false
                        var updateProductLockedStatus = new UpdateProductLockedStatus
                        {
                            LockedStatus = false,
                            LockedStatusID = product.ProductLockedStatus.LockedStatusID,
                            ProductId = product.ProductId,
                            UpdateProductLockedTimeStamp = DateTime.Now.Ticks
                        };
                        await _endpointInstancePriority.Publish(updateProductLockedStatus).ConfigureAwait(false);
                    }
                }
                list.Add(new ProductRead(product.ProductId)
                {
                    Class = product.Class,
                    Color = product.Color,
                    DaysToManufacture = product.DaysToManufacture,
                    DiscontinuedDate = product.DiscontinuedDate,
                    FinishedGoodsFlag = product.FinishedGoodsFlag,
                    ListPrice = product.ListPrice,
                    MakeFlag = product.MakeFlag,
                    ModifiedDate = product.ModifiedDate,
                    Name = product.Name,
                    ProductId = product.ProductId,
                    ProductLine = product.ProductLine,
                    ProductModelId = product.ProductModelId,
                    ProductNumber = product.ProductNumber,
                    ProductSubcategoryId = product.ProductSubcategoryId,
                    ReorderPoint = product.ReorderPoint,
                    SafetyStockLevel = product.SafetyStockLevel,
                    SellEndDate = product.SellEndDate,
                    SellStartDate = product.SellStartDate,
                    Size = product.Size,
                    SizeUnitMeasureCode = product.SizeUnitMeasureCode,
                    StandardCost = product.StandardCost,
                    Style = product.Style,
                    UserIdentifier = product.UserIdentifier,
                    Weight = product.Weight,
                    WeightUnitMeasureCode = product.WeightUnitMeasureCode,
                    Locked = product.Locked,
                    Online = product.Online
                });
            }
            return list;
        }

        static string GetProductIdFromMessage(CloudQueueMessage message)
        {
            var json = message.AsString;
            var byteOrderMarkUtf8 = Encoding.UTF8.GetString(Encoding.UTF8.GetPreamble());
            if (json.StartsWith(json))
            {
                json = json.Remove(0, byteOrderMarkUtf8.Length);
            }
            dynamic parsedJson = JsonConvert.DeserializeObject(json);
            var body = (string)parsedJson.Body;
            byte[] data = Convert.FromBase64String(body);
            string tmp = Encoding.UTF8.GetString(data);
            if (tmp.IndexOf("ProductId") > -1)
            {
                return tmp.Substring(tmp.IndexOf("ProductId") + 8, 36);
            }
            return Guid.Empty.ToString();
        }

        static async Task<Dictionary<string, int>> GetQueueLenghtForEachProduct()
        {
            Dictionary<string, int> queueLengthPerProduct = new Dictionary<string, int>();
            CloudStorageAccount storageAccount = QueueStorage.Common.CreateStorageAccountFromConnectionString(Helpers.GetStorageConnection());
            CloudQueueClient cloudQueueClient = storageAccount.CreateCloudQueueClient();
            CloudQueue queue = cloudQueueClient.GetQueueReference(Helpers.ServerEndpoint);
            IEnumerable<CloudQueueMessage> peekedMessages = await queue.PeekMessagesAsync(32);
            if (peekedMessages != null)
            {
                foreach (var msg in peekedMessages.ToList())
                {
                    var productId = GetProductIdFromMessage(msg);
                    if (ProductIdAlreadyInQueue(queueLengthPerProduct, productId))
                    {
                        queueLengthPerProduct[productId]++;
                    }
                    else
                    {
                        queueLengthPerProduct.Add(productId, 1);
                    }
                }
            }
            return queueLengthPerProduct;
        }


        [HttpGet("/api/read/productandqueuelength")]
        [EnableCors("AllowAllOrigins")]
        public async Task<IEnumerable<ProductRead>> GetProductsAndQueLengthAsync()
        {
            Dictionary<string, int> queueLengthForEachProduct = await GetQueueLenghtForEachProduct();
            var list = new List<ProductRead>();
            var products = _dataAccessRead.GetProducts();
            foreach (var product in products)
            {
                if (product.Locked)
                {
                    if (ProductHasBeenLocked40Seconds(product))
                    {  //Lock timed out and can be ignored and set to false
                        var updateProductLockedStatus = new UpdateProductLockedStatus
                        {
                            LockedStatus = false,
                            LockedStatusID = product.ProductLockedStatus.LockedStatusID,
                            ProductId = product.ProductId,
                            UpdateProductLockedTimeStamp = DateTime.Now.Ticks
                        };

                        await _endpointInstancePriority.Publish(updateProductLockedStatus).ConfigureAwait(false);
                    }
                }
                int tmpQueueLenghtForProduct = 0;
                if (ProductIdAlreadyInQueue(queueLengthForEachProduct, product.ProductId.ToString()))
                {
                    tmpQueueLenghtForProduct = queueLengthForEachProduct.FirstOrDefault(k => k.Key == product.ProductId.ToString()).Value;
                };

                list.Add(new ProductRead(product.ProductId)
                {
                    ProductId = product.ProductId,
                    Name = product.Name,
                    ModifiedDate = product.ModifiedDate,
                    UserIdentifier = product.UserIdentifier,
                    Locked = product.Locked,
                    Online = product.Online,
                    QueueLength = tmpQueueLenghtForProduct
                });
            }
            return list;
        }

        // GET api/Product/5
        [EnableCors("AllowAllOrigins")]
        [HttpGet("{id}")]
     //  public ProductRead GetProduct(long id)
        public async Task<ProductRead> GetProduct(long id)
        {
            var product = _dataAccessRead.GetProduct(id);
            if (product.ProductLockedStatus.Locked)
            {
                if (ProductHasBeenLocked40Seconds(product))
                { //Lock timed out and can be ignored and set to false
                    var updateProductLockedStatus = new UpdateProductLockedStatus
                    {

                        LockedStatus = false,
                        LockedStatusID = product.ProductLockedStatus.LockedStatusID,
                        ProductId = product.ProductId,
                        UpdateProductLockedTimeStamp = DateTime.Now.Ticks
                    };
                    await _endpointInstancePriority.Publish(updateProductLockedStatus).ConfigureAwait(false);
                }
            }
            // return _dataAccessRead.GetProduct(id);
            var ProductRead = new ProductRead(product.ProductId)
            {
                Class = product.Class,
                Color = product.Color,
                DaysToManufacture = product.DaysToManufacture,
                DiscontinuedDate = product.DiscontinuedDate,
                FinishedGoodsFlag = product.FinishedGoodsFlag,
                ListPrice = product.ListPrice,
                MakeFlag = product.MakeFlag,
                ModifiedDate = product.ModifiedDate,
                Name = product.Name,
                ProductId = product.ProductId,
                ProductLine = product.ProductLine,
                ProductModelId = product.ProductModelId,
                ProductNumber = product.ProductNumber,
                ProductSubcategoryId = product.ProductSubcategoryId,
                ReorderPoint = product.ReorderPoint,
                SafetyStockLevel = product.SafetyStockLevel,
                SellEndDate = product.SellEndDate,
                SellStartDate = product.SellStartDate,
                Size = product.Size,
                SizeUnitMeasureCode = product.SizeUnitMeasureCode,
                StandardCost = product.StandardCost,
                Style = product.Style,
                UserIdentifier = product.UserIdentifier,
                Weight = product.Weight,
                WeightUnitMeasureCode = product.WeightUnitMeasureCode,
                Locked = product.Locked,
                Online = product.Online
            };
            return ProductRead;
        }

        // POST api/Product
        [HttpPost]
         public async Task AddProduct([FromBody] ProductInsert productInsert)
        // public async Task AddProduct([Bind("ProductId,Name,ProductNumber,MakeFlag,FinishedGoodsFlag,Color,SafetyStockLevel,ReorderPoint,StandardCost,ListPrice,Size,SizeUnitMeasureCode,WeightUnitMeasureCode,Weight,DaysToManufacture,ProductLine,Class,Style,ProductSubcategoryId,ProductModelId,SellStartDate,SellEndDate,DiscontinuedDate,Rowguid,ModifiedDate,UserIdentifier")] ProductInsert productInsert)  //For Swagger testing.
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

        }

        // PUT api/Product/5
        [EnableCors("AllowAllOrigins")]
        [HttpPut("/api/Product/{id}")]
        public async Task UpdateProduct([FromBody] ProductRead productRead)
     //   public async Task UpdateProduct([Bind("ProductId,Name,ProductNumber,MakeFlag,FinishedGoodsFlag,Color,SafetyStockLevel,ReorderPoint,StandardCost,ListPrice,Size,SizeUnitMeasureCode,WeightUnitMeasureCode,Weight,DaysToManufacture,ProductLine,Class,Style,ProductSubcategoryId,ProductModelId,SellStartDate,SellEndDate,DiscontinuedDate,Rowguid,ModifiedDate,UserIdentifier,ProductLockedStatus,ProductOnlineStatus")] ProductRead productRead) //For Swagger testing.
        {
            var oldProduct = await GetProduct(productRead.ProductId);
            if (oldProduct == null) return;

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

            var updateProductLockedStatus = new UpdateProductLockedStatus
            {
                LockedStatus = true,
                LockedStatusID = oldProduct.ProductLockedStatus.LockedStatusID,
                ProductId = productRead.ProductId,
                UpdateProductLockedTimeStamp = DateTime.Now.Ticks
            };

            var updateProductOnlineStatus = new UpdateProductOnlineStatus
            {
                OnlineStatus = false,
                OnlineStatusID = oldProduct.ProductOnlineStatus.OnlineStatusID,
                ProductId = productRead.ProductId,
                UpdateProductOnlineTimeStamp = DateTime.Now.Ticks
            };

            await _endpointInstancePriority.Publish(updateProductLockedStatus).ConfigureAwait(false);
            await _endpointInstance.Send(Helpers.ServerEndpoint, updateProductOnlineStatus).ConfigureAwait(false);
            await _endpointInstance.Send(Helpers.ServerEndpoint, updateProduct).ConfigureAwait(false);
            
            
        }

        // DELETE api/Product/5
        [HttpDelete("{id}")]
        public async Task DeleteProduct(long id)
        {
            if (GetProduct(id) == null) return;
            var productId = id;
            var deleteProduct = new DeleteProduct
            {
                ProductId = productId
            };
            await _endpointInstance.Send(Helpers.ServerEndpoint, deleteProduct).ConfigureAwait(false);
        }
    }

}