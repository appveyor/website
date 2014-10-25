using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NJekyll.Code.Data
{
    public class Layout : ContentFile
    {
        public string ParentLayout { get; set; }

        public Layout(string virtualPath) : base(virtualPath) { }

        protected override void Init()
        {
            if(FrontMatter.ContainsKey("layout"))
            {
                ParentLayout = (string)FrontMatter["layout"];
            }
        }
    }
}