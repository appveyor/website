using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NJekyll.Code.Data
{
    public class ContentFile
    {
        public Dictionary<string, object> FrontMatter { get; set; }
        public string Content { get; set; }
        public ContentFormat ContentFormat { get; set; }
    }
}