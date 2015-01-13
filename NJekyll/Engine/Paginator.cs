using DotLiquid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace NJekyll.Engine
{
    public class Paginator : Drop
    {
        private List<Page> _collection;

        public int Page { get; private set; }
        public int PerPage { get; private set; }
        public int TotalPosts { get; private set; }
        public int TotalPages { get; private set; }
        public List<Page> Posts
        {
            get { return _collection.Skip((Page - 1) * PerPage).Take(PerPage).ToList(); }
        }

        public int? PreviousPage
        {
            get
            {
                int? previousPage = null;
                if (Page > 1)
                {
                    previousPage = Page - 1;
                }
                return previousPage;
            }
        }

        public string PreviousPagePath
        {
            get
            {
                return (PreviousPage.HasValue && PreviousPage.Value > 1) ? "/page/" + PreviousPage.Value : null;
            }
        }

        public int? NextPage
        {
            get
            {
                int? nextPage = null;
                if (Page < TotalPages)
                {
                    nextPage = Page + 1;
                }
                return nextPage;
            }
        }

        public string NextPagePath
        {
            get
            {
                return (NextPage.HasValue) ? "/page/" + NextPage.Value : null;
            }
        }

        public Paginator(List<Page> collection, int perPage)
        {
            this._collection = collection;
            this.PerPage = perPage;
            this.Calculate();
        }

        private void Calculate()
        {
            // get current page number from URL
            Page = 1;
            var url = HttpContext.Current.Request.Url.AbsolutePath;
            var match = Regex.Match(url, @".+/page/(?<page>\d+)/?$");
            if (match.Success)
            {
                Page = Int32.Parse(match.Groups["page"].Value);
            }

            this.TotalPosts = _collection.Count;
            this.TotalPages = (int)Math.Ceiling((double)_collection.Count / PerPage);
        }
    }
}