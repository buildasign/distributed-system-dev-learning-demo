using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Web.Mvc;
using eComm.Domain;
using eComm.Domain.Models;
using eCommDemo.Common;
using eCommDemo.DependencyResolution;
using eCommDemo.Messages;

namespace eCommDemo.Controllers
{
    public class CheckoutController : BaseController
    {
        private readonly IEnterpriseBus _bus;
        private readonly ICreateOrderMapper _mapper;

        public CheckoutController(IEnterpriseBus bus, ICreateOrderMapper mapper)
        {
            _bus = bus;
            _mapper = mapper;
        }

        [HttpGet]
        // GET: Checkout
        public ActionResult Index()
        {
            var model = new CheckoutModel {Name = "John Smith", Address = "22 Acacia Ave", State = "TX", Zip = "78758", City = "Austin", Email = "john@test.com", CcNumber = "4111111111111111", CcExpDate = "6/2025", CcCvv = "123"};
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(CheckoutModel id)
        {
            var cartToken = CookieUtil.GetCartToken();

            var paymenturl = $"cart/{cartToken}/payment";
            var authrequest = HttpUtil.CreateRequest(paymenturl, HttpMethod.Post);
            var auth = HttpUtil.Send<PaymentAuth>(authrequest);

            var carturl = $"cart/{cartToken}";
            var cartrequest = HttpUtil.CreateRequest(carturl, HttpMethod.Get);
            var cart = HttpUtil.Send<Cart>(cartrequest);

            var message = _mapper.Map(id, cart);
            _bus.Publish(message);
            var model = new ThankYouModel {OrderNumber = "123456"};
            //after checkout, clear cart cookie

            CookieUtil.GetCartToken(true);

            return View("ThankYou", model);
        }
    }

    public interface ICreateOrderMapper
    {
        CreateOrder Map(CheckoutModel model, Cart cart);
    }

    public class CreateOrderMapper : ICreateOrderMapper
    {
        public CreateOrder Map(CheckoutModel model, Cart cart)
        {
            return new CreateOrder
            {
                AuthorizationCode = cart.AuthCode,
                BillingAddress = new Address { City = model.City, Address1 = model.Address, PostalCode = model.Zip, StateRegion = model.State },
                ShippingAddress = new Address { City = model.City, Address1 = model.Address, PostalCode = model.Zip, StateRegion = model.State },
                LineItems = MapCart(cart)
            };
        }

        private IList<Product> MapCart(Cart cart)
        {
            return cart.Items.Select(ci => new Product {SKU = ci.SKU, Quantity = ci.Quantity}).ToList();
        }
    }

    public class CheckoutModel
    {
        public string CartId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        [Display(Name = "Credit Card Number:")]
        public string CcNumber { get; set; }
        [Display(Name = "Expiration Date:")]
        public string CcExpDate { get; set; }
        [Display(Name = "CVV:")]
        public string CcCvv { get; set; }
    }

    public class ThankYouModel
    {
        public string OrderNumber { get; set; }
    }
}