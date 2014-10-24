using NJekyll.Code.Data;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using YamlDotNet.RepresentationModel;

namespace NJekyll.Code
{
    public class Site
    {
        static ConcurrentDictionary<string, object> site = new ConcurrentDictionary<string, object>(StringComparer.OrdinalIgnoreCase);
        static ConcurrentDictionary<string, Page> pages = new ConcurrentDictionary<string, Page>(StringComparer.OrdinalIgnoreCase); // key - Permalink

        static string _siteRoot = null;

        public static void LoadSite()
        {
            _siteRoot = HttpContext.Current.Server.MapPath("~/site").TrimEnd('\\'); // e.g. C:\website

            // the list of directories with content that will be used for invalidating cached content
            Dictionary<string, string> directories = new Dictionary<string, string>();

            // load _config.yml
            var config = LoadSiteConfig();
            // - [CONFIGURATION_DATA] - loaded from _config.yml

            // load site data
            site["data"] = new DataFolder(GetPath("_data"));

            var test = ((DataFolder)site["data"])["testimonials"];

            test = ((DataFolder)site["data"])["testimonials"];

            var acme = ((DataFolder)site["data"])["acme_inc"];
            var duwamish_members = ((DataFolder)((DataFolder)site["data"])["duwamish"])["members"];

            // load page
            var page = LoadContentFile("docs/index.html", true);

            // scan all pages, layouts, posts, collections
            // pages index [url, page] - for quick access

            // group pages by categories
            // - categories.CATEGORY - list of all pages in category CATEGORY

            // group pages by tags
            // - tags.TAG - list of all pages with tag TAG

            // site data
            // - time - current datetime
            // - pages - List of all pages
            // - posts - List of all pages in _posts directory
            // - collections - List of collections
            // - data - A list containing the data loaded from the YAML files located in the _data directory.
            
            
            


            // go recursively through all directories of the site
        }

        private static Dictionary<string, object> LoadSiteConfig()
        {
            string cacheKey = "site:config";
            var config = GetFromCache(cacheKey) as Dictionary<string, object>;
            if(config != null)
            {
                return config;
            }

            string path = GetPath("_config.yml");
            if(File.Exists(path))
            {
                string yaml = File.ReadAllText(path);
                config = YamlToObject(yaml) as Dictionary<string, object>;
                if(config == null)
                {
                    throw new Exception("Site configuration file _config.yml must be a mapping.");
                }

                // put into cache
                AddToCache(cacheKey, config, _siteRoot);

                return config;
            }
            else
            {
                return null;
            }
        }

        private static void LoadSiteContent()
        {
            // go through all categories
        }

        private static void LoadSiteContentRecursively(string path)
        {
            // _posts - post
            // _layouts - layout, must be .html
            // _<collection> - collection, can be md|html
            // page - can be md|html
        }

        private static Page ParsePage(string path)
        {
            // determine content type
            // recognize only .html and .md files
            
            // load front matter as YAML

            // load the rest to content

            // save full path for future reference

            return null;
        }

        private static ContentFile LoadContentFile(string virtualPath, bool includeContent)
        {
            string path = GetPath(virtualPath);
            ContentFile result = new ContentFile();

            var extension = Path.GetExtension(path);
            if(extension.Equals(".md", StringComparison.OrdinalIgnoreCase) || extension.Equals(".markdown", StringComparison.OrdinalIgnoreCase))
            {
                result.ContentFormat = ContentFormat.Markdown;
            }
            else if (extension.Equals(".html", StringComparison.OrdinalIgnoreCase) || extension.Equals(".htm", StringComparison.OrdinalIgnoreCase))
            {
                result.ContentFormat = ContentFormat.HTML;
            }
            else
            {
                throw new Exception(String.Format("Uknown page format: {0}", virtualPath));
            }

            var content = new StringBuilder();
            var metadata = new StringBuilder();

            // parse metadata
            var reader = new StreamReader(path);
            string line = null;
            bool insideMetadata = false;
            while ((line = reader.ReadLine()) != null)
            {
                if (line.TrimEnd() == "---")
                {
                    // toggle mode
                    insideMetadata = !insideMetadata;
                    continue;
                }
                else if (line.TrimStart().StartsWith("<!--"))
                {
                    insideMetadata = true;
                    continue;
                }
                else if (line.TrimEnd().EndsWith("-->"))
                {
                    insideMetadata = false;
                    continue;
                }

                if (insideMetadata)
                {
                    metadata.AppendLine(line);
                }
                else
                {
                    if(!includeContent)
                    {
                        break;
                    }
                    content.AppendLine(line);
                }
            }

            result.Content = content.Length > 0 ? content.ToString() : null;
            result.FrontMatter = metadata.Length > 0 ? YamlToObject(metadata.ToString()) as Dictionary<string, object> : new Dictionary<string, object>();

            return result;
        }

        public static string GetPath(string virtualPath)
        {
            return Path.Combine(_siteRoot, virtualPath);
        }

        public static object GetFromCache(string key)
        {
            return HttpContext.Current.Cache[key];
        }

        public static void AddToCache(string key, object value, string dependencyPath)
        {
            HttpContext.Current.Cache.Insert(key, value, new System.Web.Caching.CacheDependency(dependencyPath));
        }

        #region YAML
        public static object YamlToObject(string yamlContents)
        {
            // setup the input
            var input = new StringReader(yamlContents);

            // Load the stream
            var yaml = new YamlStream();
            yaml.Load(input);

            return ParseYamlNodeValue(yaml.Documents[0].RootNode);
        }

        private static object ParseYamlNodeValue(YamlNode valueNode)
        {
            if (valueNode is YamlScalarNode)
            {
                return ParseStringValue(((YamlScalarNode)valueNode).Value);
            }
            else if (valueNode is YamlMappingNode)
            {
                Dictionary<string, object> hash = new Dictionary<string, object>();
                var mappingNode = valueNode as YamlMappingNode;
                foreach (var keyNode in mappingNode.Children.Keys)
                {
                    var scalarKeyNode = keyNode as YamlScalarNode;
                    if (scalarKeyNode == null)
                    {
                        // ignore
                        continue;
                    }

                    hash[scalarKeyNode.Value] = ParseYamlNodeValue(mappingNode.Children[keyNode]);
                }

                return hash;
            }
            else if (valueNode is YamlSequenceNode)
            {
                var items = new List<object>();
                foreach (var itemNode in ((YamlSequenceNode)valueNode))
                {
                    items.Add(ParseYamlNodeValue(itemNode));
                }
                return items;
            }
            else
            {
                throw new Exception("Unknown node: " + valueNode.ToString());
            }
        }

        private static object ParseStringValue(string s)
        {
            int integerNumber = 0;
            decimal decimalNumber = 0;
            bool boolean = false;
            DateTime date = DateTime.MinValue;

            if (Int32.TryParse(s, out integerNumber))
            {
                return integerNumber;
            }
            else if (Decimal.TryParse(s, out decimalNumber))
            {
                return decimalNumber;
            }
            else if (Boolean.TryParse(s, out boolean))
            {
                return boolean;
            }
            else if (DateTime.TryParse(s, out date))
            {
                return date;
            }

            // just return original string value
            return s;
        }
        #endregion
    }


}