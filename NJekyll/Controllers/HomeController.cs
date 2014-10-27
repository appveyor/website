using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using NJekyll.Engine;

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

            var page = Site.GetPage(pageUrl);
            if (page == null)
            {
                return new HttpStatusCodeResult(404, "Page not found");
            }
            else
            {
                // render page
                return Content(Site.RenderPage(page, Request.Url.PathAndQuery), "text/html");
            }
        }
    }
}