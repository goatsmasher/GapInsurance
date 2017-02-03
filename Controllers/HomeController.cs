using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using stupid.Factory;

namespace stupid.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserFactory UserFactory;
         public HomeController(UserFactory user)
        {
            UserFactory = user;
        }
        // GET: /Home/
        [HttpGet]
        [Route("")]
        public IActionResult Index(int admin)
        {
            if (HttpContext.Session.GetInt32("userid") != null){
                ViewBag.admin = UserFactory.GetUser((int)HttpContext.Session.GetInt32("userid")).admin;
            }
            return View();
        }
    }
}
