using System;
using System.Linq;
using Microsoft.Win32;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MintPlayer.PlatformBrowser
{
    public static class PlatformBrowser
    {
        /// <summary>Retrieves a list of installed browsers from the registry.</summary>
        public static ReadOnlyCollection<Browser> GetInstalledBrowsers()
        {
            #region Get registry key containing browser information

            var internetKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\WOW6432Node\Clients\StartMenuInternet");
            if (internetKey == null)
                internetKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Clients\StartMenuInternet");

            #endregion

            #region Loop through keys

            var result = new List<Browser>();
            foreach (var browserName in internetKey.GetSubKeyNames())
            {
                try
                {
                    // Key containing browser information
                    var browserKey = internetKey.OpenSubKey(browserName);

                    // Key containing executable path
                    var commandKey = browserKey.OpenSubKey(@"shell\open\command");

                    // Key containing icon path
                    var iconKey = browserKey.OpenSubKey(@"DefaultIcon");
                    var iconPath = (string)iconKey.GetValue(null);
                    var iconParts = iconPath.Split(',');

                    // Validate the values
                    string executablePath = ((string)commandKey.GetValue(null)).Trim('"');

                    // ExecutablePath must be .exe
                    if (System.IO.Path.GetExtension(executablePath) != ".exe")
                        throw new Exception("ExecutablePath must be .exe");

                    // IconPath must be .exe or .ico
                    var iconValid = true;
                    if (!new[] { ".exe", ".ico" }.Contains(System.IO.Path.GetExtension(iconParts[0])))
                        iconValid = false;

                    ReadOnlyDictionary<string, object> fileAssociations;
                    ReadOnlyDictionary<string, object> urlAssociations;
                    try
                    {
                        // Read the FileAssociations
                        var fileAssociationsKey = browserKey.OpenSubKey(@"Capabilities\FileAssociations");
                        fileAssociations = new ReadOnlyDictionary<string, object>(fileAssociationsKey.GetValueNames().ToDictionary(v => v, v => fileAssociationsKey.GetValue(v)));
                    }
                    catch (Exception)
                    {
                        fileAssociations = new ReadOnlyDictionary<string, object>(new Dictionary<string, object>());
                    }

                    try
                    {
                        // Read the UrlAssociations
                        var urlAssociationsKey = browserKey.OpenSubKey(@"Capabilities\URLAssociations");
                        urlAssociations = new ReadOnlyDictionary<string, object>(urlAssociationsKey.GetValueNames().ToDictionary(v => v, v => urlAssociationsKey.GetValue(v)));
                    }
                    catch (Exception)
                    {
                        urlAssociations = new ReadOnlyDictionary<string, object>(new Dictionary<string, object>());
                    }


                    var browser = new Browser
                    {
                        Name = (string)browserKey.GetValue(null),
                        ExecutablePath = executablePath,
                        IconPath = iconValid ? iconParts[0] : executablePath,
                        IconIndex = !iconValid
                            ? 0
                            : iconParts.Length > 1
                            ? Convert.ToInt32(iconParts[1])
                            : 0,
                        FileAssociations = fileAssociations,
                        UrlAssociations = urlAssociations
                    };
                    result.Add(browser);
                }
                catch (Exception)
                {
                }
            }

            #endregion

            //Debug.Print("You shall not pass");

            #region Check if Edge is installed

            var systemAppsFolder = @"C:\Windows\SystemApps\";
            if (System.IO.Directory.Exists(systemAppsFolder))
            {
                string[] directories = System.IO.Directory.GetDirectories(systemAppsFolder);
                var edgeFolder = directories.FirstOrDefault(d => d.StartsWith($"{systemAppsFolder}Microsoft.MicrosoftEdge_"));

                if (edgeFolder != null)
                {
                    if (System.IO.File.Exists($@"{edgeFolder}\MicrosoftEdge.exe"))
                    {
                        result.Add(new Browser
                        {
                            Name = "Microsoft Edge",
                            ExecutablePath = $@"{edgeFolder}\MicrosoftEdge.exe",
                            IconPath = $@"{edgeFolder}\MicrosoftEdge.exe",
                            IconIndex = 0,
                            FileAssociations = new ReadOnlyDictionary<string, object>(new Dictionary<string, object>()),
                            UrlAssociations = new ReadOnlyDictionary<string, object>(new Dictionary<string, object>())
                        });
                    }
                }
            }

            #endregion

            return new ReadOnlyCollection<Browser>(result);
        }

        public static Browser GetDefaultBrowser(IEnumerable<Browser> browsers, Enums.eProtocolType protocolType)
        {
            var urlAssociationsKey = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\Shell\Associations\URLAssociations");
            var protocolName = Enum.GetName(typeof(Enums.eProtocolType), protocolType);
            if (!urlAssociationsKey.GetSubKeyNames().Contains(protocolName))
                throw new Exception($"No url association for {protocolName}");

            var userChoiceKey = urlAssociationsKey.OpenSubKey($@"{protocolName}\UserChoice");
            var defaultBrowserProgId = userChoiceKey.GetValue("ProgId");

            var defaultBrowser = browsers.FirstOrDefault(
                b => b.UrlAssociations.Any(
                    a => ((a.Key == protocolName) & (a.Value.Equals(defaultBrowserProgId)))
                )
            );
            return defaultBrowser;
        }

        public static Browser GetDefaultBrowser(IEnumerable<Browser> browsers, Enums.eFileType protocolType)
        {
            throw new NotImplementedException();
            //var defaultBrowserKey = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\Shell\Associations\FileAssociations\")

        }
    }
}
