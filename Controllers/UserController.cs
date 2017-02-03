using stupid.Factory;
using stupid.Models;
using stupid.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;

namespace stupid.Controllers
{
    public class UserController : Controller
    {
        private readonly UserFactory UserFactory;
        private readonly PackageFactory PackageFactory;
        public UserController(UserFactory user, PackageFactory package)
        {
            UserFactory = user;
            PackageFactory = package;
        }
        // GET: /Home/
        [HttpGet]
        [Route("login")]
        public IActionResult Index()
        {
            if (HttpContext.Session.GetInt32("userid") != null)
            {
                ViewBag.admin = UserFactory.GetUser((int)HttpContext.Session.GetInt32("userid")).admin;
            }
            return View("login");
        }
        [HttpPost]
        [RouteAttribute("login")]
        public IActionResult login(LoginViewModel user)
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
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            ViewBag.login_error = "Invalid Login Credentials";
            ModelState.Clear();
            return View("login");
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
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpGet]
        [RouteAttribute("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");

        }
        [HttpGet]
        [RouteAttribute("account")]
        public IActionResult MyAccount()
        {
            if (HttpContext.Session.GetInt32("userid") == null)
            {
                return RedirectToAction("login", "User");
            }
            if (HttpContext.Session.GetInt32("userid") != null)
            {
                ViewBag.admin = UserFactory.GetUser((int)HttpContext.Session.GetInt32("userid")).admin;
            }
            //take session id here and pass some viewbags with that information
            // ViewBag.this_user = UserFactory.GetUser((int)HttpContext.Session.GetInt32("userid"));
            // ViewBag.user_coverage = PackageFactory.GetCoverage((int)HttpContext.Session.GetInt32("userid"));
            return View("MyAccount");
        }
    }
}
