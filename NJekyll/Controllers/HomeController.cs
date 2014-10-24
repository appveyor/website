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
            Site.LoadSite();
            //var cached = HttpContext.Cache["Cached"] as string;
            //if(cached == null)
            //{
            //    cached = DateTime.Now.ToString();
            //    HttpContext.Cache.Insert("Cached", cached, new System.Web.Caching.CacheDependency(Server.MapPath("~/")));
            //}

            return Content("<h1>Hello, world!</h1>" + Styles.Render("~/site-css"), "text/html");
        }
    }
}