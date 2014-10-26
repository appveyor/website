using DotLiquid;
using DotLiquid.FileSystems;
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
        public const string SITE_VROOT = "~/site";
        public const string SITE_CONFIG_FILENAME = "_config.yml";
        public const string DATA_FOLDER = "_data";
        public const string INCLUDES_FOLDER = "_includes";
        public const string LAYOUTS_FOLDER = "_layouts";

        static Dictionary<string, object> _site;
        static Dictionary<string, Page> _pages = new Dictionary<string, Page>(StringComparer.OrdinalIgnoreCase); // key - Permalink
        static Dictionary<string, Layout> _layouts = new Dictionary<string, Layout>(StringComparer.OrdinalIgnoreCase); // key - name
        static Dictionary<string, string> cacheDependencyDirectories = new Dictionary<string, string>();

        static string _siteRoot = null;

        public static string GetRedirect(string pageUrl)
        {
            EnsureSiteLoaded();

            var redirects = _site["redirects"] as Dictionary<string, object>;
            return (redirects != null && redirects.ContainsKey(pageUrl)) ? (string)redirects[pageUrl] : null;
        }

        private static void EnsureSiteLoaded()
        {
            string cacheKey = "SiteContentIsActual";
            var contentIsActual = GetFromCache(cacheKey);
            if (contentIsActual == null)
            {
                LoadSite();
                AddToCache(cacheKey, DateTime.Now.ToString(), cacheDependencyDirectories.Keys.ToArray());
            }
        }

        public static void LoadSite()
        {
            cacheDependencyDirectories.Clear();

            _siteRoot = HttpContext.Current.Server.MapPath(SITE_VROOT).TrimEnd('\\'); // e.g. C:\website
            
            // load _config.yml
            _site = LoadSiteConfig();

            // load site data
            _site["data"] = new DataFolder(GetPath(DATA_FOLDER));

            // load site pages (without content)
            LoadSiteContent("");

            // all pages
            _site["pages"] = _pages.Values.ToList();

            // group pages by categories
            var categories = from page in _pages.Values
                             from category in page.Categories
                             group page by category;

            var categoriesHash = new Dictionary<string, object>();
            foreach(var category in categories)
            {
                categoriesHash[category.Key] = category.ToList();
            }
            _site["categories"] = categoriesHash;

            // group pages by tags
            var tags = from page in _pages.Values
                       from tag in page.Tags
                       group page by tag;

            var tagsHash = new Dictionary<string, object>();
            foreach (var tag in tags)
            {
                tagsHash[tag.Key] = tags.ToList();
            }
            _site["tags"] = tagsHash;



            var rnd = RenderPage("f");
        }

        public static string RenderPage(string url)
        {
            var renderContext = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
            renderContext["site"] = _site;

            Template.FileSystem = new IncludesFolder(GetPath("_includes"));
            Template template = Template.Parse(@"
{% for item in site.data.testimonials -%}
  {{ item.customer }}
    {% for link in item.links -%} {{ link }} {% endfor -%}
{% endfor -%}

title: {{site.page.title}}
permalink: {{site.page.permalink}}


content: {{site.page.content}}

{% for kbPage in site.categories.kb -%}
  kb page title: {{ kbPage.title }}
{% endfor -%}

{% include footer.html %}
");

            return template.Render(Hash.FromDictionary(renderContext));
        }

        private static Dictionary<string, object> LoadSiteConfig()
        {
            var config = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);

            string path = GetPath(SITE_CONFIG_FILENAME);
            if(File.Exists(path))
            {
                string yaml = File.ReadAllText(path);
                try
                {
                    config = YamlToObject(yaml) as Dictionary<string, object>;
                }
                catch(Exception ex)
                {
                    throw new Exception(String.Format("Error parsing {0}.", SITE_CONFIG_FILENAME), ex);
                }

                if(config == null)
                {
                    throw new Exception(String.Format("Site configuration file {0} must be a mapping.", SITE_CONFIG_FILENAME));
                }

                cacheDependencyDirectories[path] = path;
            }

            return config;
        }

        private static void LoadSiteContent(string path)
        {
            string directoryPath = GetPath(path);
            foreach(var fileInfo in new DirectoryInfo(directoryPath).GetFileSystemInfos())
            {
                string virtualPath = (String.IsNullOrEmpty(path) ? "" : path + "\\") + fileInfo.Name;
                if (virtualPath.StartsWith(INCLUDES_FOLDER, StringComparison.OrdinalIgnoreCase))
                {
                    // ignore _includes folder
                    continue;
                }
                else if(fileInfo is DirectoryInfo)
                {
                    // recurse directory
                    LoadSiteContent(virtualPath);
                }
                else
                {
                    // process file
                    if(fileInfo.Extension.Equals(".md", StringComparison.OrdinalIgnoreCase)
                        || fileInfo.Extension.Equals(".markdown", StringComparison.OrdinalIgnoreCase)
                        || fileInfo.Extension.Equals(".html", StringComparison.OrdinalIgnoreCase))
                    {
                        if (virtualPath.StartsWith(Site.LAYOUTS_FOLDER, StringComparison.OrdinalIgnoreCase))
                        {
                            var layout = new Layout(virtualPath);
                            _layouts[layout.Name] = layout;
                        }
                        else
                        {
                            var page = new Page(virtualPath);
                            _pages[page.Permalink] = page;
                        }

                        // cache this directory
                        cacheDependencyDirectories[directoryPath] = directoryPath;
                    }
                }
            }
        }

        public static CollectionMetadata GetCollectionMetadata(string name)
        {
            var collections = _site.ContainsKey("collections") ? _site["collections"] as Dictionary<string, object> : null;
            if(collections != null)
            {
                // collections are defined
                var collection = collections.ContainsKey(name) ? collections[name] as Dictionary<string, object> : null;
                if(collection != null)
                {
                    var cm = new CollectionMetadata { Name = name };
                    if(collection.ContainsKey("output"))
                    {
                        cm.Output = (bool)collection["output"];
                    }
                    if (collection.ContainsKey("permalink"))
                    {
                        cm.Permalink = (string)collection["permalink"];
                    }
                    return cm;
                }
            }

            return new CollectionMetadata
            {
                Name = name,
                Output = true,
                Permalink = "/:year/:month/:day/:title"
            };
        }

        public static void AddCollectionPage(Page page)
        {
            var collection = _site.ContainsKey(page.Collection) ? _site[page.Collection] as List<Page> : null;
            if(collection == null)
            {
                collection = new List<Page>();
                _site[page.Collection] = collection;
            }

            collection.Add(page);
        }

        public static string GetPath(string virtualPath)
        {
            return String.IsNullOrEmpty(virtualPath) ? _siteRoot : Path.Combine(_siteRoot, virtualPath);
        }

        public static object GetFromCache(string key)
        {
            return HttpContext.Current.Cache[key];
        }

        public static void AddToCache(string key, object value, params string[] dependencyPaths)
        {
            HttpContext.Current.Cache.Insert(key, value, new System.Web.Caching.CacheDependency(dependencyPaths));
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
                Dictionary<string, object> hash = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
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