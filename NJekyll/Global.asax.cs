using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;

namespace NJekyll
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            NJekyll.Engine.Bundles.Register(BundleTable.Bundles);
            NJekyll.Engine.Routes.MapRoutes(RouteTable.Routes);
        }
    }
}
