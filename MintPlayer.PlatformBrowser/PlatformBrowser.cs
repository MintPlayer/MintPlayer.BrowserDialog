using System;
using Microsoft.Win32;
using System.Collections.Generic;
using System.Linq;

namespace MintPlayer.PlatformBrowser
{
    public static class PlatformBrowser
    {
        /// <summary>Retrieves a list of installed browsers from the registry.</summary>
        public static List<Browser> GetInstalledBrowsers()
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

                    var browser = new Browser
                    {
                        Name = (string)browserKey.GetValue(null),
                        ExecutablePath = ((string)commandKey.GetValue(null)).Trim('"'),
                        IconPath = iconParts[0],
                        IconIndex = iconParts.Length > 1 ? Convert.ToInt32(iconParts[1]) : 0
                    };
                    result.Add(browser);
                }
                catch (Exception)
                {
                }
            }

            #endregion

            #region Check if Edge is installed

            if (System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(System.Runtime.InteropServices.OSPlatform.Windows))
            {
                if (Environment.OSVersion.Version.Major == 10)
                {
                    if (System.IO.Directory.Exists("C:/Windows/SystemApps"))
                    {
                        var directories = System.IO.Directory.GetDirectories("C:/Windows/SystemApps");
                        var edgeDir = directories.FirstOrDefault(d => d.StartsWith("Microsoft.MicrosoftEdge_"));
                        if(edgeDir != null)
                        {
                            var edgePath = $"C:/Windows/SystemApps/{edgeDir}/MicrosoftEdge.exe";
                            
                            var browser = new Browser
                            {
                                Name = "Microsoft Edge",
                                ExecutablePath = edgePath,
                                IconPath = edgePath,
                                IconIndex = 0
                            };
                            result.Add(browser);
                        }
                    }
                }
            }

            #endregion

            return result;
        }
    }
}
