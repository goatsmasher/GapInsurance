using System.IO;
using Microsoft.AspNetCore.Hosting;
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
        private IHostingEnvironment hostingEnv;
        public ProductController(IHostingEnvironment env, ProductFactory product, PackageFactory package)
        {
            ProductFactory = product;
            PackageFactory = package;
            this.hostingEnv = env; 
        }
        [HttpGet]
        [RouteAttribute("browse")]
        public IActionResult Browse() {
            
            ViewBag.products = ProductFactory.GetAll();
            return View("allproducts");
        }
        [HttpGet]
        [RouteAttribute("add_product")]
        public IActionResult NewProduct(){
            return View("addproduct");
        }
        [HttpPostAttribute]
        [RouteAttribute("add_product")]
        public IActionResult AddProduct(ProductViewModel product){
            long size = 0;
            if (product.img_src != null){
                var filename = ContentDispositionHeaderValue
                                .Parse(product.img_src.ContentDisposition)
                                .FileName
                                .Trim('"');
                string myfile = filename;
                filename = hostingEnv.WebRootPath + $@"\images\products\{filename}";
                size += product.img_src.Length;
                using (FileStream fs = System.IO.File.Create(filename)){
                    product.img_src.CopyTo(fs);
                    fs.Flush();
                    Product myproduct = new Product {
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
        public IActionResult ProductPage(int id){
            ViewBag.current_product = ProductFactory.GetProduct(id);
            ViewBag.related_coverages = ProductFactory.Related_Coverages();
            return View("product");
        }
    }
}