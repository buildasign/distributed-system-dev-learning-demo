using System.Web;
using System.Web.Mvc;

namespace eCommDemo.Controllers
{
    public abstract class BaseController : Controller
    {
        protected BaseController()
        {
            SetUserSessionCookie();
        }

        protected void SetUserSessionCookie()
        {
            var sessionCookie = this.Request.Cookies["usersession"];
            if (sessionCookie != null && string.IsNullOrEmpty(sessionCookie.Value))
            {
                this.Response.Cookies.Add(new HttpCookie("usersession", this.Session.SessionID));
            }
        }
    }
}