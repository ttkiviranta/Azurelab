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
namespace API.Areas.Write.Controllers
{
/*    [Route("api/write/[controller]")]
    [ApiController]
    public class OnlineStatusController : ControllerBase
    {
      
        // POST: api/OnlineStatus
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/OnlineStatus/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }*/
}
