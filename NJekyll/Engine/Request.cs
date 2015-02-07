using DotLiquid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NJekyll.Engine
{
    public class Request : Drop
    {
        HttpContext context;

        public Request(HttpContext context)
        {
            this.context = context;
        }

        public bool IsMobileSafari
        {
            get
            {
                if (context != null)
                {
                    var userAgent = context.Request.UserAgent;

                    if (!string.IsNullOrEmpty(userAgent))
                    {
                        var ipodIndex = userAgent.IndexOf("iPod");
                        var iphoneIndex = userAgent.IndexOf("iPhone");
                        var ipadIndex = userAgent.IndexOf("iPad");

                        if (iphoneIndex > -1 || ipodIndex > -1 || ipadIndex > -1) return true;
                    }
                }

                return false;
            }
        }
    }
}