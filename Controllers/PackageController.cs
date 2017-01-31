using Microsoft.AspNetCore.Mvc;
using stupid.Factory;

namespace stupid.Controllers
{
    public class PackageController : Controller
    {
        private readonly PackageFactory PackageFactory;
        public PackageController(PackageFactory package)
        {
            PackageFactory = package;
        }
        [HttpGet]
        [RouteAttribute("packages")]
        public IActionResult Index() {
            //Need to grab ALL packages to display
            //ViewBag.packages = PackageFactory.GetAll();
            return View();
        }
    }
}