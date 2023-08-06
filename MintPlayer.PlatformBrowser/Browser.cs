using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MintPlayer.PlatformBrowser;

public class Browser
{
    internal Browser()
    {
    }

    /// <summary>Internal property that specifies whether the browser object was generated from code, and does not come from the registry.</summary>
    internal bool IsApplicationGenerated { get; set; }

    internal string KeyName { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string ExecutablePath { get; set; } = string.Empty;
    public string IconPath { get; set; } = string.Empty;
    public int IconIndex { get; set; }
    public System.Diagnostics.FileVersionInfo? Version { get; set; }

    /// <summary>List of file types (.html, .xhtml, ...) that are supported by this webbrowser</summary>
    public ReadOnlyDictionary<string, object> FileAssociations { get; set; } = (new object[0]).ToDictionary(x => string.Empty, x => x).AsReadOnly();
    /// <summary>List of web protocols (HTTP, FTP, ...) that are supported by this webbrowser</summary>
    public ReadOnlyDictionary<string, object> UrlAssociations { get; set; } = (new object[0]).ToDictionary(x => string.Empty, x => x).AsReadOnly();

    public override string ToString()
    {
        return Name;
    }
}
