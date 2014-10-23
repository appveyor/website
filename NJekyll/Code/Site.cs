using NJekyll.Code.Data;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using YamlDotNet.RepresentationModel;

namespace NJekyll.Code
{
    public class Site
    {
        static ConcurrentDictionary<string, object> site = new ConcurrentDictionary<string, object>();

        private static void LoadSite()
        {
            // load _config.yml
            // - [CONFIGURATION_DATA] - loaded from _config.yml

            // load all data

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

        private static Dictionary<string, object> ParseSiteData()
        {
            // parsing _data:
            //   members.yml
            //   banners.yml
            //   orgs/
            //     abc.yml
            //     def.yml
            //   

            Dictionary<string, object> data = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
            ParseDataRecursively(data, "_data");
            return data;
        }

        private static void ParseDataRecursively(Dictionary<string, object> data, string directory)
        {
            //   file? - object - parse as YAML
            //   directory? - Dictionary<string, object> and parse recursively
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

        private static ContentFile LoadContentFile(string path)
        {
            return null;
        }



        #region YAML
        private static object YamlToObject(string yamlContents)
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