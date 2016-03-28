using System;
using System.Web;
using System.Web.Mvc;

namespace eCommDemo.Controllers
{
    public abstract class BaseController : Controller
    {
        protected BaseController()
        {
            Guid cartId;
            var cartCookie = Request.Cookies["userCart"];
            if (string.IsNullOrEmpty(cartCookie?.Value) || !Guid.TryParse(cartCookie.Value, out cartId))
            {
                cartId = Guid.NewGuid();    //TODO: pull from Cart API
                Response.Cookies.Add(new HttpCookie("userCart", cartId.ToString()));
            }
        }
    }
}