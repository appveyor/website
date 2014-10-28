using System.Web;
using System.Web.Optimization;

namespace NJekyll.Engine
{
    public class Bundles
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void Register(BundleCollection bundles)
        {
            // JavaScript
            bundles.Add(new ScriptBundle("~/site-js").IncludeDirectory("~/js", "*.js", true));

            // CSS
            bundles.Add(new StyleBundle("~/site-css").IncludeDirectory("~/css", "*.css", true));
        }
    }
}
