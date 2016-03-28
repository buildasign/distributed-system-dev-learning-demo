using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Mvc;
using eComm.Domain;
using eComm.Domain.Models;
using eCommDemo.Common;

namespace eCommDemo.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        public ActionResult Index()
        {
            var cartToken = CookieUtil.GetCartToken();

            var url = $"cart/{cartToken}/item";

            var request = HttpUtil.CreateRequest(url, HttpMethod.Get);
            var cart = HttpUtil.Send<Cart>(request);

            var model = new CartModel(cart);

            return View(model);
        }

        [HttpPost]
        public ActionResult AddToCart(string sku)
        {
            var cartItem = new CreateCartItem
            {
                Quantity = 1,
                SKU = sku
            };

            var cartToken = CookieUtil.GetCartToken();
            var url = $"cart/{cartToken}/item";
            var request = HttpUtil.CreateRequest(url, HttpMethod.Post, cartItem);
            var itemId = HttpUtil.Send<string>(request);

            return Json(new
            {
                Success = true,
                Id = itemId
            });
        }
    }

    public class CartModel
    {
        public CartModel(Cart cart)
        {
            Items = cart.Items;
            CartId = cart.Id;
        }

        public int CartId { get; set; }
        public IList<CartItem> Items { get; set; }

        public decimal Subtotal
        {
            get { return Items.Sum(ci => ci.Price); }
        }

        public decimal Discounts
        {
            get { return Items.Sum(ci => ci.Discount); }
        }

        public decimal Total
        {
            get { return Subtotal - Discounts; }
        }
    }
}