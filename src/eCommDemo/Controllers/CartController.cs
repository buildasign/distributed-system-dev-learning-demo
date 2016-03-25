using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eCommDemo.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        public ActionResult Index()
        {
            var model = new CartModel();
            model.Items = new List<CartItem>
            {
                new CartItem {SKU = "CNV-123", Description = "Arizona 16x20", Price = 100m, Discount = 0, Quantity = 1, Image = "/Images/Listings/arizona.jpg"}
            };
            return View(model);
        }
    }

    public class CartModel
    {
        public CartModel()
        {
            Items = new List<CartItem>();
            CartId = Guid.NewGuid();
        }

        public Guid CartId { get; set; }
        public List<CartItem> Items { get; set; }

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

    public class CartItem
    {
        public string SKU { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
    }
}