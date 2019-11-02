using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MintPlayer.PlatformBrowser
{
    public class Browser
    {
        internal Browser()
        {
        }

        internal string KeyName { get; set; }
        public string Name { get; set; }
        public string ExecutablePath { get; set; }
        public string IconPath { get; set; }
        public int IconIndex { get; set; }
        public System.Diagnostics.FileVersionInfo Version { get; set; }

        /// <summary>List of file types (.html, .xhtml, ...) that are supported by this webbrowser</summary>
        public ReadOnlyDictionary<string, object> FileAssociations { get; set; }
        /// <summary>List of web protocols (HTTP, FTP, ...) that are supported by this webbrowser</summary>
        public ReadOnlyDictionary<string, object> UrlAssociations { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
