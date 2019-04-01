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
using System.Threading.Tasks;

namespace API.Areas.Read.Controllers
{
    [Route("api/read/[controller]")]
    [ApiController]
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

        // GET: api/LockedStatus
        [HttpGet]
        public IEnumerable<ProductLockedStatus> GetLockedStatuses()
        {
            return _productLockedStatusDataAccess.GetLockedStatuses();
        }

        // GET: api/LockedStatus/5
 /*       [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }*/

      
    }
}
