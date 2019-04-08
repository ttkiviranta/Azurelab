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
                OnlineStatus = productOnlineStatusRead.Online,
                OnlineStatusID = productOnlineStatusRead.OnlineStatusID,
                ProductId = productOnlineStatusRead.ProductID,
                UpdateProductOnlineTimeStamp = DateTime.Now.Ticks
            };
           
            await _endpointInstance.Send(Helpers.ServerEndpoint, updateProductOnlineStatus).ConfigureAwait(false);
        }

        // DELETE: api/ApiWithActions/5
        /*    [HttpDelete("{id}")]
            public void Delete(int id)
            {
            }*/

        ProductOnlineStatus GetOnlineStatus(long id)
        {
            return _productOnlineStatusDataAccess.GetOnlineStatus(id);
        }
    }
}
