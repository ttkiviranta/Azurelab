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
using Shared.Messages.Events;
using Shared.Utils;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace API.Areas.Write.Controllers
{
    [Route("api/write/[controller]")]
    //  [ApiController]
    public class OnlineStatusController : ControllerBase
    {
        readonly IEndpointInstance _endpointInstance;
        readonly IEndpointInstance _endpointInstancePriority;
        readonly ProductOnlineStatusDataAccess _productOnlineStatusDataAccess;
       

        public OnlineStatusController(IEndpointInstance endpointInstance, IEndpointInstance endpointInstancePriority, IConfiguration configuration)
        {
            _endpointInstance = endpointInstance;
            _endpointInstancePriority = endpointInstancePriority;
            _productOnlineStatusDataAccess = new ProductOnlineStatusDataAccess(configuration);
        }


        [HttpPut("/api/write/Product/online/{id}")]
        [EnableCors("AllowAllOrigins")]
        public async Task UpdateProductOnline(int id, [FromBody] ProductOnlineStatusRead productOnlineStatusRead)
     //   public async Task UpdateProductOnline([Bind("ProductID, Online, OnlineTimeStamp, OnlineStatusID")] ProductOnlineStatusRead productOnlineStatusRead)
        {
            var oldProductOnlineStatus = GetOnlineStatus(productOnlineStatusRead.ProductID);
            if (oldProductOnlineStatus == null) return;
            var updateProductOnlineStatus = new UpdateProductOnlineStatus
            {
                // OnlineStatus = false,
                OnlineStatus = productOnlineStatusRead.Online,
                OnlineStatusID = productOnlineStatusRead.OnlineStatusID,
                ProductId = productOnlineStatusRead.ProductID,
                UpdateProductOnlineTimeStamp = DateTime.Now.Ticks
            };
           
            await _endpointInstance.Send(Helpers.ServerEndpoint, updateProductOnlineStatus).ConfigureAwait(false);
        }

        [HttpPost]
        [EnableCors("AllowAllOrigins")]
        public async Task AddProductOnline(int id, [FromBody] ProductOnlineStatus productOnlineStatus)
      //  public async Task AddProductOnline([Bind("ProductID, Online, OnlineTimeStamp, OnlineStatusID")] ProductOnlineStatus productOnlineStatus)
        {
            var createProductOnlineStatus = new CreateProductOnlineStatus
            {
                OnlineStatus = productOnlineStatus.Online,
                OnlineStatusID = Guid.NewGuid(),
                ProductId = productOnlineStatus.ProductID,
                CreateProductOnlineTimeStamp = DateTime.Now.Ticks
            };
            await _endpointInstance.Send(Helpers.ServerEndpoint, createProductOnlineStatus).ConfigureAwait(false);
        }

        // DELETE api/Product/5
        [HttpDelete("{id}")]
        public async Task DeleteProductOnlineStatus(long id)
        {
            if (GetOnlineStatus(id) == null) return;
            var productId = id;
            var deleteProductOnlineStatus = new DeleteProductOnlineStatus
            {
                ProductId = productId
            };
            await _endpointInstance.Send(Helpers.ServerEndpoint, deleteProductOnlineStatus).ConfigureAwait(false);
        }

        ProductOnlineStatus GetOnlineStatus(long id)
        {
            return _productOnlineStatusDataAccess.GetOnlineStatus(id);
        }
    }
}
