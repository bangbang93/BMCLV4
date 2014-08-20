using System;
using System.Windows;

namespace BMCLV4.Language
{
    class LangType
    {
        public string Name;
        public ResourceDictionary Language = new ResourceDictionary();
        public LangType(string name,string path)
        {
            this.Name = name;
            Language.Source = new Uri(path);
        }
    }
}
