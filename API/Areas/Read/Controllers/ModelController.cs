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
    [Route("api/read/[controller]")]
    [ApiController]
    public class ModelController : Controller
    {
        readonly IEndpointInstance _endpointInstance;
        readonly ProductModelDataAccess _productModelDataAccess;



        //public ProductController(IEndpointInstance endpointInstance, IConfiguration configuration)
        public ModelController(IEndpointInstance endpointInstance, IConfiguration configuration)
        {
            _endpointInstance = endpointInstance;
            _productModelDataAccess = new ProductModelDataAccess(configuration);

            // _context = apiContext;
        }

        // GET api/Product
        [EnableCors("AllowAllOrigins")]
        [HttpGet]
        public IEnumerable<ProductModel> GetProductModels()
        {
            return _productModelDataAccess.GetProductModels();
        }

    }
}