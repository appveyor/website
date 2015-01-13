using System;
using System.Web.Routing;
using RouteMagic;

namespace NJekyll.Engine
{
    public class Routes
    {
        public static void MapRoutes(RouteCollection routes)
        {
            Action<RequestContext> processPage = context =>
            {
                var request = context.HttpContext.Request;
                var response = context.HttpContext.Response;

                var pageUrl = request.Url.AbsolutePath.TrimEnd('/');
                if (String.IsNullOrEmpty(pageUrl))
                {
                    pageUrl = "/";
                }

                // check if page was permanently redirected
                var redirectUrl = Site.GetRedirect(pageUrl);
                if (redirectUrl != null)
                {
                    response.RedirectPermanent(redirectUrl, true);
                }
                else
                {
                    var page = Site.GetPage(pageUrl);
                    if (page == null || !page.Published)
                    {
                        // 404
                        response.StatusCode = 404;
                        response.StatusDescription = "Page not found";
                        response.End();
                    }
                    else
                    {
                        // render page
                        response.ContentType = "text/html";
                        response.Write(Site.RenderPage(page, request.Url.PathAndQuery));
                        response.End();
                    }
                }
            };

            // Routes
            routes.MapDelegate("Default", "{*catchall}", new { httpMethod = new HttpMethodConstraint("GET") }, processPage);
        }
    }
}