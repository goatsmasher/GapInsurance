using Microsoft.AspNetCore.Mvc;
using stupid.Factory;

namespace stupid.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductFactory ProductFactory;
        public ProductController(ProductFactory product)
        {
            ProductFactory = product;
        }
        [HttpGet]
        [RouteAttribute("browse")]
        public IActionResult Browse() {
            
            //ViewBag.packages = ProductFactory.GetAll();
            return View("allproducts");
        }
        [HttpGet]
        [RouteAttribute("add_product")]
        public IActionResult NewProduct(){
            return View("addproduct");
        }
        [HttpGetAttribute]
        [RouteAttribute("product")]
        public IActionResult ProductPage(){
            return View("product");
        }
    }
}