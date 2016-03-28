using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace eCommDemo.Controllers
{
    public class CheckoutController : BaseController
    {
        [HttpGet]
        // GET: Checkout
        public ActionResult Index()
        {
            var model = new CheckoutModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(CheckoutModel id)
        {
            var model = new ThankYouModel {OrderNumber = "123456"};

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