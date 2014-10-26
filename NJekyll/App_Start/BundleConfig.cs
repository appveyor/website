using System.Web;
using System.Web.Optimization;

namespace NJekyll
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            // JavaScript
            bundles.Add(new ScriptBundle("~/site-js").IncludeDirectory("~/site/js", "*.js", true));

            // CSS
            bundles.Add(new StyleBundle("~/site-css").IncludeDirectory("~/site/css", "*.css", true));
        }
    }
}
