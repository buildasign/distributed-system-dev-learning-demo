using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Web.Mvc;
using eComm.Domain.Models;
using eCommDemo.Common;

namespace eCommDemo.Controllers
{
    public class CheckoutController : BaseController
    {
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
            var authrequest = HttpUtil.CreateRequest(paymenturl, HttpMethod.Post, new CreatePayment {CardNumber = id.CcNumber, ExpDate=id.CcExpDate, Cvv = id.CcCvv});
            var auth = HttpUtil.Send<PaymentAuth>(authrequest);

            var checkouturl = $"cart/{cartToken}/checkout";
            var checkoutrequest = HttpUtil.CreateRequest(checkouturl, HttpMethod.Post,
                new CustomerInfo
                {
                    Name = id.Name,
                    Address = id.Address,
                    State = id.State,
                    City = id.City,
                    Zip = id.Zip,
                    Email = id.Email
                });
            var orderNumber = HttpUtil.Send<int>(checkoutrequest);

            var model = new ThankYouModel {OrderNumber = orderNumber.ToString()};
            //after checkout, clear cart cookie

            CookieUtil.GetCartToken(true);

            return View("ThankYou", model);
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