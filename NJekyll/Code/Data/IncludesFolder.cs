using DotLiquid.FileSystems;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace NJekyll.Code.Data
{
    public class IncludesFolder : IFileSystem
    {
        string _path;

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