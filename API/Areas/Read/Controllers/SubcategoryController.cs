﻿using Microsoft.AspNetCore.Cors;
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
    [Route("api/read/[controller]")]
    [ApiController]
    public class SubcategoryController : Controller
    {
        readonly IEndpointInstance _endpointInstance;
        readonly ProductSubcategoryDataAccess _productSubcategoryDataAccess;
 
        public SubcategoryController(IEndpointInstance endpointInstance, IConfiguration configuration)
        {
            _endpointInstance = endpointInstance;
            _productSubcategoryDataAccess = new ProductSubcategoryDataAccess(configuration);
        }

        // GET api/Product
        [EnableCors("AllowAllOrigins")]
        [HttpGet]
        public IEnumerable<ProductSubcategory> GetProductSubcategories()
        {
            return _productSubcategoryDataAccess.GetProductSubcategories();
        }

    }
}