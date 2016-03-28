using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eCommDemo.Controllers
{
    public abstract class BaseController : Controller
    {
        protected BaseController()
        {
            //Wire up common code here
        }
    }
}