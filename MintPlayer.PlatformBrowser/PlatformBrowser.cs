using System;
using Microsoft.Win32;
using System.Collections.Generic;

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

                    Browser browser = new Browser
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

            return result;
        }
    }
}
