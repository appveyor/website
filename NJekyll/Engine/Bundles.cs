using System.Linq;
using System.Collections.Generic;
using System.IO;
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
            bundles.Add(new ScriptBundle("~/site-js").IncludeDirectory("~/site/js", "*.js", true));

            // CSS
            var cssBundle = new StyleBundle("~/site-css");
            var sortedStyles = SearchFilesSorted("~/site/css", "*.css");
            cssBundle.Include(sortedStyles.Keys.ToArray());
            bundles.Add(cssBundle);

#if DEBUG
            BundleTable.EnableOptimizations = false;
#else
            BundleTable.EnableOptimizations = true;
#endif

        }

        private static SortedList<string, string> SearchFilesSorted(string virtualPath, string pattern)
        {
            var sortedFiles = new SortedList<string, string>();
            string sitePath = GetPath("~/");
            string path = GetPath(virtualPath);

            foreach (var fi in new DirectoryInfo(path).GetFileSystemInfos(pattern, SearchOption.AllDirectories))
            {
                string vPath = "~" + fi.FullName.Substring(sitePath.Length - 1).Replace('\\', '/');
                sortedFiles.Add(vPath, vPath);
            }
            return sortedFiles;
        }

        private static string GetPath(string virtualPath)
        {
            return HttpContext.Current.Server.MapPath("~/");
        }
    }
}
