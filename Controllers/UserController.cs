using stupid.Factory;
using stupid.Models;
using stupid.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using stupid.ViewModels.LoginViewModel;
using Microsoft.AspNetCore.Identity;

namespace stupid.Controllers
{
    public class UserController : Controller
    {
        private readonly UserFactory UserFactory;
        public UserController(UserFactory user)
        {
            UserFactory = user;
        }
        // GET: /Home/
        [HttpGet]
        [Route("login")]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [RouteAttribute("login")]
        public IActionResult Login(LoginViewModel user)
        {
            if (ModelState.IsValid)
            {
                User this_user = UserFactory.Login(user.email);
                var Hasher = new PasswordHasher<User>();
                if (this_user != null)
                {
                    if (0 != Hasher.VerifyHashedPassword(this_user, this_user.password, user.password))
                    {
                        HttpContext.Session.SetInt32("userid", this_user.id);
                        return RedirectToAction("CurrentAuctions", "Auction");
                    }
                }
            }
            ViewBag.login_error = "Invalid Login Credentials";
            ModelState.Clear();
            return View("Index");
        }

        [HttpGet]
        [RouteAttribute("register")]
        public IActionResult Register()
        {
            return View("Register");
        }

        [HttpPost]
        [RouteAttribute("register")]
        public IActionResult Register(RegisterViewModel user)
        {
            if (ModelState.IsValid)
            {
                User this_user = UserFactory.AddWithReturn(user);
                HttpContext.Session.SetInt32("userid", this_user.id);
                return RedirectToAction("CurrentAuctions", "Auction");
            }
            return View("Index");
        }

        [HttpGet]
        [RouteAttribute("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");

        }
        [HttpGet]
        [RouteAttribute("myaccount")]
        public IActionResult MyAccount()
        {
            //take session id here and pass some viewbags with that information
            // ViewBag.this_user = UserFactory.GetUser(id);
            // ViewBag.user_coverage = PackageFactory.GetCoverage(id);
            return View("MyAccount");
        }
    }
}
