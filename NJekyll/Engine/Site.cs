using DotLiquid;
using DotLiquid.FileSystems;
using MarkdownSharp;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Optimization;
using YamlDotNet.RepresentationModel;

namespace NJekyll.Engine
{
    public class Site
    {
        public const string SITE_CONTENT_VALID_CACHE_KEY = "SITE_CONTENT_VALID_CACHE_KEY";
        public const string SITE_VROOT = "~/site";
        public const string SITE_CONFIG_FILENAME = "_config.yml";
        public const string DATA_FOLDER = "_data";
        public const string INCLUDES_FOLDER = "_includes";
        public const string LAYOUTS_FOLDER = "_layouts";

        static Dictionary<string, object> _site;
        static Dictionary<string, Page> _pages = new Dictionary<string, Page>(StringComparer.OrdinalIgnoreCase); // key - Permalink
        static Dictionary<string, Layout> _layouts = new Dictionary<string, Layout>(StringComparer.OrdinalIgnoreCase); // key - name
        static Dictionary<string, List<Page>> _collections = new Dictionary<string, List<Page>>(StringComparer.OrdinalIgnoreCase);
        static Dictionary<string, string> cacheDependencyDirectories = new Dictionary<string, string>();

        static string _siteRoot = null;

        public static string GetRedirect(string pageUrl)
        {
            EnsureSiteLoaded();

            var redirects = _site["redirects"] as Dictionary<string, object>;
            return (redirects != null && redirects.ContainsKey(pageUrl)) ? (string)redirects[pageUrl] : null;
        }

        public static void EnsureSiteLoaded()
        {
            var contentIsActual = GetFromCache(SITE_CONTENT_VALID_CACHE_KEY);
            if (contentIsActual == null)
            {
                LoadSite();
                AddToCache(SITE_CONTENT_VALID_CACHE_KEY, DateTime.Now.ToString(), cacheDependencyDirectories.Keys.ToArray(), null);
            }
        }

        private static void LoadSite()
        {
            _siteRoot = HttpContext.Current.Server.MapPath(SITE_VROOT).TrimEnd('\\'); // e.g. C:\website

            cacheDependencyDirectories.Clear();

            _pages.Clear();
            _layouts.Clear();
            _collections.Clear();

            // load _config.yml
            _site = LoadSiteConfig();

            // load site data
            _site["data"] = new DataFolder(GetPath(DATA_FOLDER));

            // load site pages (without content)
            LoadSiteContent("");

            // all pages
            _site["pages"] = _pages.Values.ToList();

            // collections
            foreach(var collection in _collections)
            {
                var sortedCollection = collection.Value.OrderByDescending(p => p.Date).ToList();

                // set Next and Previous
                for (int i = 0; i < sortedCollection.Count; i++ )
                {
                    // Next
                    if(i < sortedCollection.Count - 1)
                    {
                        sortedCollection[i].Next = sortedCollection[i + 1];
                    }
                    // Previous
                    if (i > 0)
                    {
                        sortedCollection[i].Previous = sortedCollection[i - 1];
                    }
                }
                _site[collection.Key] = sortedCollection;
            }

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

            // add resource bundles
            BundleTable.EnableOptimizations = _site.ContainsKey("enable_optimizations") ? (bool)_site["enable_optimizations"] : false;
            _site["bundles"] = new Dictionary<string, object>
            {
                { "css", Styles.Render("~/site-css").ToString() },
                { "js", Scripts.Render("~/site-js").ToString() }
            };
        }

        public static string RenderContent(Page page, string content, Dictionary<string, object> parameters = null)
        {
            var context = new Context();
            context["site"] = _site;
            context["page"] = page;
            context.AddFilters(typeof(Filters));

            if(parameters != null)
            {
                foreach(var parameter in parameters)
                {
                    context[parameter.Key] = parameter.Value;
                }
            }

            Template.FileSystem = new IncludesFolder(GetPath("_includes"));
            Template template = Template.Parse(content);

            return template.Render(new RenderParameters {
                Context = context
            });
        }

        public static Page GetPage(string pageUrl)
        {
            EnsureSiteLoaded();

            // remove page information from URL
            var match = Regex.Match(pageUrl, @".+(?<start>/page)/(?<page>\d+)/?$");
            if (match.Success)
            {
                pageUrl = pageUrl.Substring(0, match.Groups["start"].Index);
            }

            return _pages.ContainsKey(pageUrl) ? _pages[pageUrl] : null;
        }

        public static string RenderPage(Page page, string requestUrl)
        {
            EnsureSiteLoaded();

            // look for rendered page in cache
            string cacheKey = "page:" + requestUrl;
            var content = (string)GetFromCache(cacheKey);
            if (content != null)
            {
                return content;
            }

            // get rendered page content
            content = page.Content;

            var layoutName = page.Layout;
            while(layoutName != null)
            {
                // render layout
                if(!_layouts.ContainsKey(layoutName))
                {
                    throw new Exception(String.Format("Error rendering page {0}. Layout {1} not found.", page.Url, layoutName));
                }

                var layout = _layouts[layoutName];
                content = RenderContent(page, layout.Content, new Dictionary<string, object>
                {
                    { "content", content }
                });

                layoutName = layout.ParentLayout;
            }

            // add to cache
            Site.AddToCache(cacheKey, content, new string[] { page.Path }, new string[] { SITE_CONTENT_VALID_CACHE_KEY });

            return content;
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
                if (virtualPath.StartsWith(DATA_FOLDER, StringComparison.OrdinalIgnoreCase) ||
                    virtualPath.StartsWith(INCLUDES_FOLDER, StringComparison.OrdinalIgnoreCase))
                {
                    cacheDependencyDirectories[directoryPath] = directoryPath;
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
            var collection = _collections.ContainsKey(page.Collection) ? _collections[page.Collection] as List<Page> : null;
            if(collection == null)
            {
                collection = new List<Page>();
                _collections[page.Collection] = collection;
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

        public static void AddToCache(string key, object value, string[] dependencyPaths, string[] dependencyKeys)
        {
            HttpContext.Current.Cache.Insert(key, value, new System.Web.Caching.CacheDependency(dependencyPaths, dependencyKeys));
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