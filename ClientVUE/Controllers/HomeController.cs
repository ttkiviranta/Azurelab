using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Client.Models;
using Client.Models.ProductsViewModel;
using Client.Models.ProductViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;

namespace Client.Controllers
{
    public class HomeController : Controller
    {
        List<Product> products;

        [HttpGet]
        public IActionResult GetAll()
        {
            //hard coded data for brevity, this must be from a data store on the real world
            var posts = new List<Post>();
            posts.Add(new Post() { id = 1, first_name = "Mörkö", last_name = "Muumi" });
            posts.Add(new Post() { id = 2, first_name = "Mörkö2", last_name = "Muumi2" });
            posts.Add(new Post() { id = 3, first_name = "Mörkö3", last_name = "Muumi3" });
            posts.Add(new Post() { id = 4, first_name = "Mörkö4", last_name = "Muumi4" });
            return Ok(posts);
        }


        public class Post
        {
            public int id { get; set; }
            public string first_name { get; set; }
            public string last_name { get; set; }
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {

            try
            {
                products = await Utils.Get<List<Product>>("api/Product", "http://localhost:83");
            }

            catch
            {
                TempData["CustomError"] = "No contact with server! AzurelabAPI must be started before Client could start!";
                return NotFoundResult(products);
            }

            return Ok(products);
        }

        private IActionResult NotFoundResult(List<Product> products)
        {
            throw new NotImplementedException();
        }

        /*     public IActionResult Index()
                {
                    return View();
                }*/

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
