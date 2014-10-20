using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;

namespace NJekyll.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return Content("<h1>Hello, world!</h1>" + Styles.Render("~/Content/css"), "text/html");
        }
    }
}