using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace BMCLV4.DownloadSource
{
    class DownloadSourceType
    {
        public string Name;
        public ResourceDictionary Dictionary = new ResourceDictionary();

        public DownloadSourceType(string name, string path)
        {
            this.Name = name;
            Dictionary.Source = new Uri(path);
        }
    }
}
