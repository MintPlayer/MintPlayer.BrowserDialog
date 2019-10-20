using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MintPlayer.PlatformBrowser
{
    public class Browser
    {
        internal Browser()
        {
        }

        public string Name { get; set; }
        public string ExecutablePath { get; set; }
        public string IconPath { get; set; }
        public int IconIndex { get; set; }
        public ReadOnlyDictionary<string, object> FileAssociations { get; set; }
        public ReadOnlyDictionary<string, object> UrlAssociations { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
