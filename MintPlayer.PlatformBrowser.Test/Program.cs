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
                Console.WriteLine($"Version: {browser.Version}");
                Console.WriteLine($"Icon path: {browser.IconPath}");
                Console.WriteLine($"Icon index: {browser.IconIndex}");
                Console.WriteLine();
            }

            try
            {
                var httpDefaultBrowser = PlatformBrowser.GetDefaultBrowser(browsers, Enums.eProtocolType.Http);
                Console.WriteLine($"HTTP default browser: {httpDefaultBrowser.Name}");
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ResetColor();
            }

            try
            {
                var htmlDefaultBrowser = PlatformBrowser.GetDefaultBrowser(browsers, Enums.eFileType.html);
                Console.WriteLine($"HTML default browser: {htmlDefaultBrowser.Name}");
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
