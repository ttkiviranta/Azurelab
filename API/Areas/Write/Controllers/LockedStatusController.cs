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
    public class LockedStatusController : ControllerBase
    {
        readonly IEndpointInstance _endpointInstance;
        readonly IEndpointInstance _endpointInstancePriority;
        readonly ProductLockedStatusDataAccess _productLockedStatusDataAccess;

        public LockedStatusController(IEndpointInstance endpointInstance, IEndpointInstance endpointInstancePriority, IConfiguration configuration)
        {
            _endpointInstance = endpointInstance;
            _endpointInstancePriority = endpointInstancePriority;
            _productLockedStatusDataAccess = new ProductLockedStatusDataAccess(configuration);
        }

        
        [HttpPut("/api/write/Product/locked/{id}")]
        [EnableCors("AllowAllOrigins")]
        public async Task UpdateProductLocked(int id, [FromBody] ProductLockedStatusRead productLockedStatusRead)
    //    public async Task UpdateProductLocked([Bind("ProductID, Locked, LockedTimeStamp,LockedStatusID") ] ProductLockedStatus productLockedStatusRead)
        {
            var oldProductLockedStatus = GetLockedStatus(productLockedStatusRead.ProductID);
            if (oldProductLockedStatus == null) return;
            var updateProductLockedStatus = new UpdateProductLockedStatus
            {
                LockedStatus = productLockedStatusRead.Locked,
                LockedStatusID = productLockedStatusRead.LockedStatusID,
                ProductId = productLockedStatusRead.ProductID,
                UpdateProductLockedTimeStamp = DateTime.Now.Ticks
            };
            await _endpointInstancePriority.Publish(updateProductLockedStatus).ConfigureAwait(false);
        }
        [HttpPost]
        [EnableCors("AllowAllOrigins")]
        public async Task AddProductLocked(int id, [FromBody] ProductLockedStatus productLockedStatus)
        //public async Task AddProductLocked([Bind("ProductID, Locked, LockedTimeStamp, LockedStatusID")] ProductLockedStatus productLockedStatus)
        {
            var createProductLockedStatus = new CreateProductLockedStatus
            {
                LockedStatus = productLockedStatus.Locked ,
                LockedStatusID = Guid.NewGuid(),
                ProductId = productLockedStatus.ProductID,
                CreateProductLockedTimeStamp = DateTime.Now.Ticks
            };
            await _endpointInstance.Send(Helpers.ServerEndpoint, createProductLockedStatus).ConfigureAwait(false);
        }

        // DELETE api/Product/5
        [HttpDelete("{id}")]
        public async Task DeleteProductLockedStatus(long id)
        {
            if (GetLockedStatus(id) == null) return;
            var productId = id;
            var deleteProductLockedStatus = new DeleteProductLockedStatus
            {
                ProductId = productId
            };
            await _endpointInstance.Send(Helpers.ServerEndpoint, deleteProductLockedStatus).ConfigureAwait(false);
        }

        ProductLockedStatus GetLockedStatus(long id)
        {
            return _productLockedStatusDataAccess.GetLockedStatus(id);
        }
    }
}
