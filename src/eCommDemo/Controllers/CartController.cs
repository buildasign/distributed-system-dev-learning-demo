using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Mvc;
using AutoMapper;
using eComm.Domain;
using eComm.Domain.Models;
using eCommDemo.Common;
using eCommDemo.Models;
using WebGrease.Css.Extensions;

namespace eCommDemo.Controllers
{
    public class CartController : Controller
    {
        private readonly IMapper _mapper;

        public CartController(IMapper mapper)
        {
            _mapper = mapper;
        }

        // GET: Cart
        public ActionResult Index()
        {
            var cartToken = CookieUtil.GetCartToken();

            var url = $"cart/{cartToken}";

            var request = HttpUtil.CreateRequest(url, HttpMethod.Get);
            var cart = HttpUtil.Send<Cart>(request);


            var cartItemModels = _mapper.Map<IEnumerable<CartItem>, IEnumerable<CartItemModel>>(cart.Items).ToList();

            request = HttpUtil.CreateRequest("catalog/0", HttpMethod.Get);
            var catalog = HttpUtil.Send<IEnumerable<ListingData>>(request);

            cartItemModels.ForEach(x =>
            {
                x.Image = catalog.FirstOrDefault(i => i.SKU == x.SKU).Image;
            });


            return View(new CartModel(cartItemModels, cartToken));
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
        public CartModel(IList<CartItemModel> items, Guid tokenId)
        {
            Items = items;
            CartId = tokenId;
        }

        public Guid CartId { get; set; }
        public IList<CartItemModel> Items { get; set; }

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