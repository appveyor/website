using NJekyll.Code;
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
            var pageUrl = Request.Url.AbsolutePath.TrimEnd('/');
            if(pageUrl == "")
            {
                pageUrl = "/";
            }

            // check if page was permanently redirected
            var redirectUrl = Site.GetRedirect(pageUrl);
            if(redirectUrl != null)
            {
                return RedirectPermanent(redirectUrl);
            }

            return Content("<h1>Hello, world!</h1>" + Styles.Render("~/site-css"), "text/html");
        }
    }
}