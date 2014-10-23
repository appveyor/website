using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NJekyll.Code.Data
{
    public class Layout : Dictionary<string, object>
    {
        public Layout ParentLayout { get; set; }
    }
}