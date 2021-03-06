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
    public class CategoryController : Controller
    {
        readonly IEndpointInstance _endpointInstance;
        readonly ProductCategoryDataAccess _productCategoryDataAccess;
        public CategoryController(IEndpointInstance endpointInstance, IConfiguration configuration)
        {
            _endpointInstance = endpointInstance;
            _productCategoryDataAccess = new ProductCategoryDataAccess(configuration);

        }

        // GET api/Product
        [EnableCors("AllowAllOrigins")]
        [HttpGet]
        public IEnumerable<ProductCategory> GetProductCategories()
        {
            return _productCategoryDataAccess.GetProductCategories();
        }

    }
}