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

namespace API.Areas.Read.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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

        // GET: api/LockedStatus
        [HttpGet]
        public IEnumerable<ProductOnlineStatusRead> GetOnlineStatuses()
        {
            return _productOnlineStatusDataAccess.GetOnlineStatuses();
        }

        // GET: api/LockedStatus/5
        /*       [HttpGet("{id}", Name = "Get")]
               public string Get(int id)
               {
                   return "value";
               }*/


    }
}
