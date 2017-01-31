using Microsoft.AspNetCore.Mvc;
using stupid.Factory;

namespace stupid.Controllers
{
    public class CartController : Controller
    {
        private readonly CartFactory CartFactory;
        public CartController(CartFactory cart)
        {
            CartFactory = cart;
        }
        [HttpPost]
        [RouteAttribute("add_to_cart/{id}")]
        public IActionResult AddToCart(int id)
        {
            //Add the submitted item to the current users cart
            //CartFactory.Add(id);
            return RedirectToAction("Index"); //this needs to redirect to either the users cart or keep them on them on current page with an ajax popup success notificaton
        }
        [HttpGet]
        [RouteAttribute("cart")]
        public IActionResult ShowCart()
        {
            //Get a viewbag of everything currently in the users cart through 
            //ViewBag.cart = CartFactory.GetCart()
            return View("Cart");
        }
        [HttpGet]
        [RouteAttribute("Checkout")]
        public IActionResult Checkout(){
            //This will submit all the current items in the cart into database as owned by the users
            //CartFactory.Checkout();
            return RedirectToAction("MyAccount", "User");
        }
    }
}