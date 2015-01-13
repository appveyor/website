using DotLiquid;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace NJekyll.Engine
{
    public abstract class ContentFile : Drop
    {
        private string _content;

        internal ContentFormat ContentFormat { get; private set; }
        internal Dictionary<string, object> FrontMatter { get; private set; }

        public string VirtualPath { get; private set; }
        public string Path { get; private set; }
        public virtual string Content
        {
            get
            {
                if (_content == null)
                {
                    Load(loadContent: true);
                }
                return _content;
            }
        }

        public ContentFile(string virtualPath)
        {
            this.VirtualPath = virtualPath;
            this.Path = Site.GetPath(this.VirtualPath);
            this.FrontMatter = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);

            Load(loadContent: false);
            Init();
        }

        protected abstract void Init();

        private void Load(bool loadContent)
        {
            var extension = System.IO.Path.GetExtension(this.Path);
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
                throw new Exception(String.Format("Unknown page format: {0}", this.VirtualPath));
            }

            var content = new StringBuilder();
            var frontMatter = new StringBuilder();

            // parse metadata
            using (var reader = new StreamReader(this.Path))
            {
                string line = null;
                bool insideMetadata = false;
                int lineNumber = 0;
                while ((line = reader.ReadLine()) != null)
                {
                    lineNumber++;

                    if ((line.TrimEnd() == "---" || line.TrimEnd() == "<!--") && lineNumber == 1) // front matter starts on the first line
                    {
                        insideMetadata = true;
                        continue;
                    }
                    else if ((line.TrimEnd() == "---" || line.TrimEnd() == "-->") && lineNumber > 1 && insideMetadata)
                    {
                        insideMetadata = false;
                        continue;
                    }

                    if (insideMetadata && !loadContent)
                    {
                        frontMatter.AppendLine(line);
                    }
                    else if (!insideMetadata && loadContent)
                    {
                        content.AppendLine(line);
                    }
                    else if (!loadContent)
                    {
                        break;
                    }
                }
            }

            if (loadContent && content.Length > 0)
            {
                _content = content.ToString();
            }
            else if (!loadContent && frontMatter.Length > 0)
            {
                try
                {
                    FrontMatter = Site.YamlToObject(frontMatter.ToString()) as Dictionary<string, object>;
                }
                catch (Exception ex)
                {
                    throw new Exception(String.Format("Error parsing front matter in {0}.", VirtualPath), ex);
                }
            }
        }
    }
}