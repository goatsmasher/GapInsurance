using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using stupid.Factory;

namespace stupid.Controllers
{
    public class PackageController : Controller
    {
        private readonly PackageFactory PackageFactory;
        private readonly UserFactory UserFactory;
        public PackageController(PackageFactory package, UserFactory user)
        {
            PackageFactory = package;
            UserFactory = user;
        }
        [HttpGet]
        [RouteAttribute("packages")]
        public IActionResult Index()
        {
            if (HttpContext.Session.GetInt32("userid") != null)
            {
                ViewBag.admin = UserFactory.GetUser((int)HttpContext.Session.GetInt32("userid")).admin;
            }
            //Need to grab ALL packages to display
            //ViewBag.packages = PackageFactory.GetAll();
            return View();
        }
    }
}