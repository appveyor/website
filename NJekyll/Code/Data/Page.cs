using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NJekyll.Code.Data
{
    public class Page : Dictionary<string, object>
    {
        public string Permalink { get; set; }
        public string Content { get; set; }
        public ContentFormat ContentFormat { get; set; }

        // Standard fields:
        //  - layout - layout file name without extension and directory
        //  - published - true|false
        //  - content
        //  - title
        //  - excerpt?
        //  - url - url of the post, page URL or permalink
        //  - id - unique post identifier (the same as URL)
        //  - date - date of the post, parsed from URL or specified in front matter
        //  - categories - list of categories
        //  - tags - list of tags
        //  - path - physical path to page/post
        //  - next?
        //  - previous?
    }
}