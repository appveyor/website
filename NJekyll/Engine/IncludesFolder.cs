using DotLiquid.FileSystems;
using System.IO;

namespace NJekyll.Engine
{
    public class IncludesFolder : IFileSystem
    {
        private string _path;

        public IncludesFolder(string path)
        {
            this._path = path;
        }

        public string ReadTemplateFile(DotLiquid.Context context, string templateName)
        {
            return File.ReadAllText(Path.Combine(_path, templateName));
        }
    }
}