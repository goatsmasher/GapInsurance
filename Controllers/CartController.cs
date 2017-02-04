using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using stupid.Factory;

namespace stupid.Controllers
{
    public class CartController : Controller
    {
        private readonly CartFactory CartFactory;
        private readonly UserFactory UserFactory;
        public CartController(CartFactory cart, UserFactory user)
        {
            CartFactory = cart;
            UserFactory = user;
        }
        [HttpPost]
        [RouteAttribute("add_to_cart/{id}")]
        public IActionResult AddToCart(int id)
        {
            //Add the submitted item to the current users cart
            CartFactory.AddToCart(id, (int)HttpContext.Session.GetInt32("userid"));
            return RedirectToAction("ShowCart"); //this needs to redirect to either the users cart or keep them on them on current page with an ajax popup success notificaton
        }
        [HttpGet]
        [RouteAttribute("cart")]
        public IActionResult ShowCart()
        {
            if (HttpContext.Session.GetInt32("userid") == null)
            {
                return RedirectToAction("login", "User");
            }
            if (HttpContext.Session.GetInt32("userid") != null)
            {
                ViewBag.admin = UserFactory.GetUser((int)HttpContext.Session.GetInt32("userid")).admin;
            }
            ViewBag.my_cart = CartFactory.GetCart((int)HttpContext.Session.GetInt32("userid"));
            return View("Cart", "Product");
        }
        [HttpGet]
        [RouteAttribute("Checkout")]
        public IActionResult Checkout()
        {
            if (HttpContext.Session.GetInt32("userid") != null)
            {
                ViewBag.admin = UserFactory.GetUser((int)HttpContext.Session.GetInt32("userid")).admin;
            }
            //This will submit all the current items in the cart into database as owned by the users
            //CartFactory.Checkout();
            return RedirectToAction("MyAccount", "User");
        }
    }
}