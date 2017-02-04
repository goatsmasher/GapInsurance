using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using stupid.Factory;
using stupid.Models;
using stupid.ViewModels;

namespace stupid.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductFactory ProductFactory;
        private readonly PackageFactory PackageFactory;
        private readonly CartFactory CartFactory;
        private readonly UserFactory UserFactory;
        private IHostingEnvironment hostingEnv;
        public ProductController(IHostingEnvironment env, ProductFactory product, PackageFactory package, UserFactory user, CartFactory cart)
        {
            ProductFactory = product;
            CartFactory = cart;
            PackageFactory = package;
            UserFactory = user;
            this.hostingEnv = env;
        }
        [HttpGet]
        [RouteAttribute("browse")]
        public IActionResult Browse()
        {
            if (HttpContext.Session.GetInt32("userid") != null)
            {
                ViewBag.admin = UserFactory.GetUser((int)HttpContext.Session.GetInt32("userid")).admin;
            }
            ViewBag.all_products = ProductFactory.GetAll();
            return View("allproducts");
        }
        [HttpGet]
        [RouteAttribute("add_product")]
        public IActionResult NewProduct()
        {
            if (HttpContext.Session.GetInt32("userid") != null)
            {
                ViewBag.admin = UserFactory.GetUser((int)HttpContext.Session.GetInt32("userid")).admin;
            }
            return View("addproduct");
        }
        [HttpPostAttribute]
        [RouteAttribute("AddProduct")]
        public IActionResult AddProduct(ProductViewModel product)
        {
            long size = 0;
            if (product.img_src != null)
            {
                var filename = ContentDispositionHeaderValue
                                .Parse(product.img_src.ContentDisposition)
                                .FileName
                                .Trim('"');
                string myfile = filename;
                filename = hostingEnv.WebRootPath + $@"\images\products\{filename}";
                size += product.img_src.Length;
                using (FileStream fs = System.IO.File.Create(filename))
                {
                    product.img_src.CopyTo(fs);
                    fs.Flush();
                    Product myproduct = new Product
                    {
                        name = product.name,
                        cost = product.cost,
                        category = product.category,
                        description = product.description,
                        img_src = myfile
                    };
                    ProductFactory.AddProduct(myproduct);
                }
            }
            return RedirectToAction("Browse");
        }
        [HttpGetAttribute]
        [RouteAttribute("product/{id}")]
        public IActionResult ProductPage(int id)
        {
            if (HttpContext.Session.GetInt32("userid") != null)
            {
                ViewBag.admin = UserFactory.GetUser((int)HttpContext.Session.GetInt32("userid")).admin;
            }
            Random ran = new Random();
            ViewBag.ratings = ran.Next(1, 6); //random number of stars
            ViewBag.random = ran.Next(1, 1001); //random number of reviews
            ViewBag.current_product = ProductFactory.GetProduct(id);
            ViewBag.related_coverages = ProductFactory.Related_Coverages();
            return View("product");
        }
        [HttpPost]
        [RouteAttribute("add_to_cart")]
        public IActionResult add_to_cart(int product_id)
        {
            int user = (int)HttpContext.Session.GetInt32("userid");
            int to_cart = product_id;
            CartFactory.AddToCart(to_cart, user);
            return RedirectToAction("ShowCart", "Cart", to_cart);
        }
    }
}