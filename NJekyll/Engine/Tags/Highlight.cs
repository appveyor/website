using DotLiquid;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace NJekyll.Engine.Tags
{
    public class Highlight : DotLiquid.Block
    {
        private string _language;
        private string _code;

        public override void Initialize(string tagName, string language, List<string> tokens)
        {
            _language = language.Trim();
            _code = tokens[0];
            base.Initialize(tagName, language, tokens);
        }

        public override void Render(Context context, TextWriter result)
        {
            var highlightedCode = HighlightCode(_code);
            result.Write(highlightedCode);
        }

        private string HighlightCode(string code)
        {
            string encodedCode = WebUtility.UrlEncode(code);
            string myParameters = "lexer=" + _language + "&divstyles=&style=vs&code=" + encodedCode;

            using (WebClient wc = new WebClient())
            {
                wc.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                return wc.UploadString("http://hilite.me/api", myParameters);
            }
        }
    }
}