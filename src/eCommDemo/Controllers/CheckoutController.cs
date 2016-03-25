using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eCommDemo.Controllers
{
    public class CheckoutController : Controller
    {
        // GET: Checkout
        public ActionResult Index()
        {
            var model = new CheckoutModel();
            return View(model);
        }
    }

    public class CheckoutModel
    {
    }
}