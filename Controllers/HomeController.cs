using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace stupid.Controllers
{
    public class HomeController : Controller
    {
        // GET: /Home/
        [HttpGet]
        [Route("")]
        public IActionResult Index(int admin)
        {
            if (HttpContext.Session.GetInt32("userid") != null)
            {
                ViewBag.logged_in = (int)HttpContext.Session.GetInt32("userid");
            }
            return View();
        }
    }
}
