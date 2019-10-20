using System;

namespace MintPlayer.PlatformBrowser.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var browsers = PlatformBrowser.GetInstalledBrowsers();
            foreach (var browser in browsers)
            {
                Console.WriteLine($"Browser: {browser.Name}");
                Console.WriteLine($"Executable: {browser.ExecutablePath}");
                Console.WriteLine($"Icon path: {browser.IconPath}");
                Console.WriteLine($"Icon index: {browser.IconIndex}");
                Console.WriteLine();
            }

            while (true)
            {
                try
                {
                    var httpDefaultBrowser = PlatformBrowser.GetDefaultBrowser(browsers, Enums.eProtocolType.http);
                    Console.WriteLine($"HTTP default browser: {httpDefaultBrowser.Name}");
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(ex.Message);
                    Console.ResetColor();
                }
                Console.ReadKey();
            }
        }
    }
}
