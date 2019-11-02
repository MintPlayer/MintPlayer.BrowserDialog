using System;
using System.Linq;
using Microsoft.Win32;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;

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
                        #region Disconfigured browser installation, Add more browsers to this list
                        string browserIdFormat = string.Empty;
                        if (browserName.Equals("IEXPLORE.EXE"))
                            browserIdFormat = "IE.AssocFile.{0}";
                        #endregion

                        // Add a default list of file associations
                        var associations = CreateDefaultFileAssociations(browserIdFormat);

                        fileAssociations = new ReadOnlyDictionary<string, object>(associations);
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
                        KeyName = browserName,
                        Name = (string)browserKey.GetValue(null),
                        ExecutablePath = executablePath,
                        Version = FileVersionInfo.GetVersionInfo(executablePath),
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
                        var edgePath = $@"{edgeFolder}\MicrosoftEdge.exe";
                        result.Add(new Browser
                        {
                            KeyName = "Microsoft Edge",
                            Name = "Microsoft Edge",
                            ExecutablePath = edgePath,
                            Version = FileVersionInfo.GetVersionInfo(edgePath),
                            IconPath = edgePath,
                            IconIndex = 0,
                            // http://mikenation.net/files/win-10-reg.txt
                            //FileAssociations = new ReadOnlyDictionary<string, object>(CreateDefaultFileAssociations("AppXde74bfzw9j31bzhcvsrxsyjnhhbq66cs")),
                            FileAssociations = new ReadOnlyDictionary<string, object>(CreateDefaultFileAssociations("AppX4hxtad77fbk3jkkeerkrm0ze94wjf3s9")),
                            UrlAssociations = new ReadOnlyDictionary<string, object>(new Dictionary<string, object>())
                        });
                    }
                }
            }

            #endregion

            return new ReadOnlyCollection<Browser>(result);
        }

        private static Dictionary<string, object> CreateDefaultFileAssociations(string browserIdFormat)
        {
            var associations = new Dictionary<string, object>(new[] {
                new KeyValuePair<string, object>(".htm", string.Format(browserIdFormat, "HTM")),
                new KeyValuePair<string, object>(".html", string.Format(browserIdFormat, "HTML")),
                new KeyValuePair<string, object>(".pdf", string.Format(browserIdFormat, "PDF")),
                new KeyValuePair<string, object>(".shtml", string.Format(browserIdFormat, "SHTML")),
                new KeyValuePair<string, object>(".svg", string.Format(browserIdFormat, "SVG")),
                new KeyValuePair<string, object>(".webp", string.Format(browserIdFormat, "WEBP")),
                new KeyValuePair<string, object>(".xht", string.Format(browserIdFormat, "XHT")),
                new KeyValuePair<string, object>(".xhtml", string.Format(browserIdFormat, "XHTML"))
            });
            return associations;
        }

        /// <summary>Get the default browser by protocol (eg. HTTP, HTTPS, FTP, SMS, MailTo, ...)</summary>
        /// <param name="browsers">When you already called GetInstalledBrowsers(), pass it here.</param>
        /// <param name="protocolType">Protocol type</param>
        /// <returns></returns>
        public static Browser GetDefaultBrowser(IEnumerable<Browser> browsers, Enums.eProtocolType protocolType)
        {
            var urlAssociationsKey = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\Shell\Associations\URLAssociations");
            var protocolName = Enum.GetName(typeof(Enums.eProtocolType), protocolType).ToLower();
            if (!urlAssociationsKey.GetSubKeyNames().Contains(protocolName))
                throw new Exception($"No url association for {protocolName}");

            var userChoiceKey = urlAssociationsKey.OpenSubKey($@"{protocolName}\UserChoice");
            var defaultBrowserProgId = userChoiceKey.GetValue("ProgId");

            switch (defaultBrowserProgId)
            {
                case "AppX4hxtad77fbk3jkkeerkrm0ze94wjf3s9": // htm, html
                case "AppXd4nrz8ff68srnhf9t5a8sbjyar1cr723": // pdf
                case "AppXde74bfzw9j31bzhcvsrxsyjnhhbq66cs": // svg
                case "AppXcc58vyzkbjbs4ky0mxrmxf8278rk9b3t": // xml
                case "AppXq0fevzme2pys62n3e0fbqa7peapykr8v":
                    // Edge
                    return browsers.FirstOrDefault(
                        b => b.KeyName == "Microsoft Edge"
                    );
                case "IE.HTTP":
                    // Internet Explorer
                    return browsers.FirstOrDefault(
                        b => b.KeyName == "IEXPLORE.EXE"
                    );
                default:
                    return browsers.FirstOrDefault(
                        b => b.UrlAssociations.Any(
                            a => ((a.Key == protocolName) & (a.Value.Equals(defaultBrowserProgId)))
                        )
                    );
            }
        }

        /// <summary>Get the default browser by protocol (eg. HTTP, HTTPS, FTP, SMS, MailTo, ...).</summary>
        /// <param name="protocolType">Protocol type</param>
        /// <returns></returns>
        public static Browser GetDefaultBrowser(Enums.eProtocolType protocolType)
        {
            var browsers = GetInstalledBrowsers();
            return GetDefaultBrowser(browsers, protocolType);
        }

        /// <summary>Get the default browser for the HTTP protocol.</summary>
        /// <returns></returns>
        public static Browser GetDefaultBrowser()
        {
            return GetDefaultBrowser(Enums.eProtocolType.Http);
        }

        /// <summary>Get the default browser for the specified file type (html, pdf, svg, ...)</summary>
        /// <param name="browsers">When you already called GetInstalledBrowsers(), pass it here.</param>
        /// <param name="fileType">The filetype you want to open</param>
        public static Browser GetDefaultBrowser(IEnumerable<Browser> browsers, Enums.eFileType fileType)
        {
            var ext = Enum.GetName(typeof(Enums.eFileType), fileType).ToLower();
            var fileExtsKey = Registry.CurrentUser.OpenSubKey($@"SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\FileExts");
            var subkeyNames = fileExtsKey.GetSubKeyNames();

            if (!subkeyNames.Contains($@".{ext}"))
                throw new Exception("The specified filetype is not present in the registry");

            var fileTypeKey = fileExtsKey.OpenSubKey($@".{ext}\UserChoice");
            var progId = fileTypeKey.GetValue("ProgId");

            return browsers.FirstOrDefault(
                b =>
                {
                    // Don't compare key AND value. Just check if the value exists in the list.
                    var res = b.FileAssociations.Any(v => v.Value.Equals(progId));

                    //var res = b.FileAssociations.Any(
                    //     //fa => ((fa.Key == $".{ext}") & (Equals(fa.Value, progId)))
                    //     fa => (Equals(fa[$".{ext}"], progId)))
                    // );
                    return res;
                }
            );
        }

        public static Browser GetDefaultBrowser(Enums.eFileType fileType)
        {
            var browsers = GetInstalledBrowsers();
            return GetDefaultBrowser(browsers, fileType);
        }
    }
}
