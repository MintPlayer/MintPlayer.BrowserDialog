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
            Console.ReadKey();
        }
    }
}
