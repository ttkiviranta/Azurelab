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
        //  public async Task UpdateProductLocked(int id, [FromBody] ProductLockedStatusRead productLockedStatusRead)
        public async Task UpdateProductLocked([Bind("ProductID, Locked, LockedTimeStamp,LockedStatusID") ] ProductLockedStatusRead productLockedStatusRead)
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

        // DELETE: api/ApiWithActions/5
    /*    [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }*/

        ProductLockedStatus GetLockedStatus(long id)
        {
            return _productLockedStatusDataAccess.GetLockedStatus(id);
        }
    }
}
