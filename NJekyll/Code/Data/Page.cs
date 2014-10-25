using DotLiquid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NJekyll.Code.Data
{
    public class Page : ContentFile
    {
        public string Permalink { get; set; }
        public string Layout { get; set; } // layout file name without extension and directory
        public string Path { get; set; } // physical path to page/post
        public bool Published { get; set; }
        public DateTime Date { get; set; }
        public List<string> Categories { get; set; }
        public List<string> Tags { get; set; }

        public Page(string virtualPath) : base(virtualPath) { }

        public override object BeforeMethod(string method)
        {
            string key = method.ToLowerInvariant();
            switch(key)
            {
                case "content": return GetContent();
                case "id":
                case "url": return Permalink;
                case "date": return Date;
                case "categories": return Categories;
                case "tags": return Tags;
                case "path": return Path;
                // excerpt, next, previous?
                default:
                    return FrontMatter[key];
            }
        }

        protected override void Init()
        {
            if (FrontMatter.ContainsKey("permalink"))
            {
                Permalink = (string)FrontMatter["permalink"];
            }
        }

        private string GetContent()
        {
            return Content;
        }
    }
}