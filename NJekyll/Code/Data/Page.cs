using DotLiquid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace NJekyll.Code.Data
{
    public class Page : ContentFile
    {
        internal string Collection { get; private set; }
        public string Permalink { get; private set; }
        public string Layout { get; set; } // layout file name without extension and directory
        public bool Published { get; set; }
        public DateTime Date { get; set; }
        public List<string> Categories { get; set; }
        public List<string> Tags { get; set; }

        public string Id
        {
            get { return Permalink; }
        }

        public string Url
        {
            get { return Permalink; }
        }

        public override string Content
        {
            get
            {
                var _content = base.Content;
                return _content;
            }
        }

        public Page(string virtualPath) : base(virtualPath) { }

        public override object BeforeMethod(string method)
        {
            return FrontMatter[method];
        }

        protected override void Init()
        {
            Permalink = FrontMatter.ContainsKey("permalink") ? (string)FrontMatter["permalink"] : null;
            Layout = FrontMatter.ContainsKey("layout") ? (string)FrontMatter["layout"] : null;
            Published = FrontMatter.ContainsKey("published") ? (bool)FrontMatter["published"] : true;
            Categories = new List<string>();
            Tags = new List<string>();

            // categories
            if (FrontMatter.ContainsKey("categories"))
            {
                var categories = FrontMatter["categories"] as List<object>;
                if(categories != null)
                {
                    Categories = categories.Select(item => item.ToString()).ToList();
                }
                else
                {
                    var commaSeparatedString = FrontMatter["categories"] as string;
                    if(commaSeparatedString != null)
                    {
                        Categories = commaSeparatedString.Split(new char[] { ',', ';', ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                    }
                }
            }

            // tags
            if (FrontMatter.ContainsKey("tags"))
            {
                var tags = FrontMatter["tags"] as List<object>;
                if (tags != null)
                {
                    Tags = tags.Select(item => item.ToString()).ToList();
                }
                else
                {
                    var commaSeparatedString = FrontMatter["tags"] as string;
                    if (commaSeparatedString != null)
                    {
                        Tags = commaSeparatedString.Split(new char[] { ',', ';', ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                    }
                }
            }
            
            // calculate Permalink
            if(String.IsNullOrWhiteSpace(Permalink))
            {
                Permalink = "/" + VirtualPath.Replace('\\', '/').Trim('/');
             
                // now we have /some_folder/file.html or /some_file.md
                // remove extension
                int idx = Permalink.LastIndexOf(".");
                if(idx != -1)
                {
                    Permalink = Permalink.Substring(0, idx);
                }

                // check if it ends with /index
                if(Permalink.EndsWith("/index", StringComparison.OrdinalIgnoreCase))
                {
                    Permalink = Permalink.Substring(0, Permalink.Length - 6);
                }

                if(Permalink == "")
                {
                    Permalink = "/";
                }
            }

            // is it collection document?
            string[] permalinkParts = Permalink.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);

            if (permalinkParts.Length > 1 && permalinkParts[0].StartsWith("_"))
            {
                this.Collection = permalinkParts[0].Substring(1);
                var collectionMetadata = Site.GetCollectionMetadata(this.Collection);

                // calculate date and permalink from post URL
                string pageName = permalinkParts.Last();
                var match = Regex.Match(pageName, @"^(?<year>\d{4})-(?<month>\d{2})-(?<day>\d{2})-(?<title>.+)");
                if(match.Success)
                {
                    string year = match.Groups["year"].Value;
                    string month = match.Groups["month"].Value;
                    string day = match.Groups["day"].Value;

                    // replace permalink
                    Permalink = collectionMetadata.Permalink
                        .Replace(":year", year)
                        .Replace(":month", month)
                        .Replace(":day", day)
                        .Replace(":title", match.Groups["title"].Value);

                    // replace date
                    Date = new DateTime(Int32.Parse(year), Int32.Parse(month), Int32.Parse(day));
                }
                else
                {
                    // replace '/_' in the beginning with '/'
                    Permalink = "/" + Permalink.Substring(2);
                }

                // published state
                this.Published = collectionMetadata.Output;

                // add page to a collection
                Site.AddCollectionPage(this);
            }

            // override date from front matter
            if(FrontMatter.ContainsKey("date"))
            {
                Date = (DateTime)FrontMatter["date"];
            }
        }
    }
}