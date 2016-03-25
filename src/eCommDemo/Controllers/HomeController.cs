using System.Web.Mvc;
using eCommDemo.DependencyResolution;
using eCommDemo.Messages;

namespace eCommDemo.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEnterpriseBus _enterpriseBus;

        public HomeController(IEnterpriseBus enterpriseBus)
        {
            _enterpriseBus = enterpriseBus;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            var message = new CreateOrder();

            _enterpriseBus.Publish(message);

            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}