using DotLiquid;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace NJekyll.Code.Data
{
    public abstract class ContentFile : Drop
    {
        string _virtualPath;
        string _content;

        public ContentFormat ContentFormat { get; private set;}
        public Dictionary<string, object> FrontMatter { get; private set; }
        public string Content
        {
            get
            {
                if(_content == null)
                {
                    Load(loadContent: true);
                }
                return _content;
            }
        }

        public ContentFile(string virtualPath)
        {
            this._virtualPath = virtualPath;
            this.FrontMatter = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);

            Load(loadContent: false);
            Init();
        }

        protected abstract void Init();

        private void Load(bool loadContent)
        {
            string path = Site.GetPath(_virtualPath);

            var extension = Path.GetExtension(path);
            if (extension.Equals(".md", StringComparison.OrdinalIgnoreCase) || extension.Equals(".markdown", StringComparison.OrdinalIgnoreCase))
            {
                ContentFormat = ContentFormat.Markdown;
            }
            else if (extension.Equals(".html", StringComparison.OrdinalIgnoreCase) || extension.Equals(".htm", StringComparison.OrdinalIgnoreCase))
            {
                ContentFormat = ContentFormat.HTML;
            }
            else
            {
                throw new Exception(String.Format("Uknown page format: {0}", _virtualPath));
            }

            var content = new StringBuilder();
            var frontMatter = new StringBuilder();

            // parse metadata
            var reader = new StreamReader(path);
            string line = null;
            bool insideMetadata = false;
            int lineNumber = 0;
            while ((line = reader.ReadLine()) != null)
            {
                lineNumber++;

                if (line.TrimEnd() == "---" && lineNumber == 1) // front matter starts on the first line
                {
                    // toggle mode
                    insideMetadata = !insideMetadata;
                    continue;
                }
                else if (line.StartsWith("<!--") && lineNumber == 1) // front matter starts on the first line
                {
                    insideMetadata = true;
                    continue;
                }
                else if (line.TrimEnd().EndsWith("-->"))
                {
                    insideMetadata = false;
                    continue;
                }

                if (insideMetadata && !loadContent)
                {
                    frontMatter.AppendLine(line);
                }
                else if(loadContent)
                {
                    content.AppendLine(line);
                }
                else
                {
                    break;
                }
            }

            if(loadContent && content.Length > 0)
            {
                _content = content.ToString();
            }
            else if (!loadContent && frontMatter.Length > 0)
            {
                FrontMatter = Site.YamlToObject(frontMatter.ToString()) as Dictionary<string, object>;
            }
        }
    }
}