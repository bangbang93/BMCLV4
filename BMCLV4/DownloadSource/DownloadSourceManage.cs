using System;
using System.Collections.Generic;
using System.Windows;

namespace BMCLV4.DownloadSource
{
    static class DownloadSourceManage
    {
        private static readonly Dictionary<string, DownloadSourceType> DownloadSources =
            new Dictionary<string, DownloadSourceType>();
        private static readonly ResourceDictionary DefaultDownloadSourceDictionary =
            LoadLangFromResource("pack://application:,,,/DownloadSource/BmclApi.xaml");

        private static ResourceDictionary _usingDictionary = DefaultDownloadSourceDictionary;
        public static void Add(string downloadSourceName, string languageUrl)
        {
            if (DownloadSources.ContainsKey(downloadSourceName))
            {
                DownloadSources[downloadSourceName] = new DownloadSourceType(downloadSourceName, languageUrl);
                return;
            }
            DownloadSources.Add(downloadSourceName, new DownloadSourceType(downloadSourceName, languageUrl));

        }
        public static ResourceDictionary LoadLangFromResource(string path)
        {
            var dictionary = new ResourceDictionary {Source = new Uri(path)};
            return dictionary;
        }

        public static string GetUrlFromResource(string key)
        {
            if (_usingDictionary.Contains(key))
                return _usingDictionary[key] as string;
            if (DefaultDownloadSourceDictionary.Contains(key))
                return DefaultDownloadSourceDictionary[key] as string;
            return key;
        }

        static public void ChangeDownloadResource(string downloadSourceName)
        {
            if (!DownloadSources.ContainsKey(downloadSourceName))
            {
                _usingDictionary = DefaultDownloadSourceDictionary;
                return;
            }
            var downloadSourceType = DownloadSources[downloadSourceName];
            if (downloadSourceType != null)
            {
                _usingDictionary = downloadSourceType.Dictionary;
            }
                
        }

        public static string[] ListLanuage()
        {
            var langs = new string[DownloadSources.Count];
            var i = 0;
            foreach (var lang in DownloadSources)
            {
                langs[i] = (string)lang.Value.Dictionary["DisplayName"];
                i++;
            }
            return langs;
        }
    }
}
