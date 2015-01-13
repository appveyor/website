using DotLiquid;
using System.IO;

namespace NJekyll.Engine
{
    public class DataFolder : Drop
    {
        private string _path = null;

        public DataFolder(string path)
        {
            this._path = path;
        }

        public override object BeforeMethod(string method)
        {
            return GetDataOrFolder(method);
        }

        private object GetDataOrFolder(string name)
        {
            var itemPath = Path.Combine(this._path, name);

            // check cache
            string cacheKey = "data:" + itemPath;
            object result = Site.GetFromCache(cacheKey);
            if (result != null)
            {
                return result;
            }

            string cacheDependencyPath = null;
            if (Directory.Exists(itemPath))
            {
                result = new DataFolder(itemPath);
                cacheDependencyPath = itemPath;
            }
            else if (File.Exists(itemPath + ".yml"))
            {
                var yaml = File.ReadAllText(itemPath + ".yml");
                result = Site.YamlToObject(yaml);
                cacheDependencyPath = itemPath + ".yml";
            }
            else
            {
                // unknown data type
                return null;
            }

            // put to cache
            Site.AddToCache(cacheKey, result, new string[] { cacheDependencyPath }, null);

            return result;
        }
    }
}