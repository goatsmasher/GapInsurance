using Microsoft.AspNetCore.Mvc;
using stupid.Factory;
using stupid.ViewModels;

namespace stupid.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductFactory ProductFactory;
        private readonly PackageFactory PackageFactory;
        public ProductController(ProductFactory product, PackageFactory package)
        {
            ProductFactory = product;
            PackageFactory = package; 
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
            ProductFactory.AddProduct(product);
            return RedirectToAction("Index", "Home");
        }
        [HttpGetAttribute]
        [RouteAttribute("product")]
        public IActionResult ProductPage(){
            return View("product");
        }
    }
}