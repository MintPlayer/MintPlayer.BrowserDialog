# MintPlayer.PlatformBrowser
This package allows you to retrieve the web browsers (including Microsoft Edge) installed on the system.

## Version info

| License                                                                                                               | Build status                                                                                           | Code coverage | Code quality |
|-----------------------------------------------------------------------------------------------------------------------|--------------------------------------------------------------------------------------------------------|---------------|--------------|
| [![License](https://img.shields.io/badge/License-Apache%202.0-green.svg)](https://opensource.org/licenses/Apache-2.0) | ![.NET Core](https://github.com/MintPlayer/MintPlayer.PlatformBrowser/workflows/.NET%20Core/badge.svg) |               | [![Codacy Badge](https://app.codacy.com/project/badge/Grade/c0cc807ae50645ca909b68c95f2275d0)](https://www.codacy.com/gh/MintPlayer/MintPlayer.PlatformBrowser?utm_source=github.com&amp;utm_medium=referral&amp;utm_content=MintPlayer/MintPlayer.PlatformBrowser&amp;utm_campaign=Badge_Grade) |

| Package                    | Release                                                                                                                                                 | Preview                                                                                                                                                    | Downloads |
|----------------------------|---------------------------------------------------------------------------------------------------------------------------------------------------------|------------------------------------------------------------------------------------------------------------------------------------------------------------|-----------|
| MintPlayer.PlatformBrowser | [![NuGet Version](https://img.shields.io/nuget/v/MintPlayer.PlatformBrowser.svg?style=flat)](https://www.nuget.org/packages/MintPlayer.PlatformBrowser) | [![NuGet Version](https://img.shields.io/nuget/vpre/MintPlayer.PlatformBrowser.svg?style=flat)](https://www.nuget.org/packages/MintPlayer.PlatformBrowser) | [![NuGet](https://img.shields.io/nuget/dt/MintPlayer.PlatformBrowser.svg?style=flat)](https://www.nuget.org/packages/MintPlayer.PlatformBrowser) |
| MintPlayer.BrowserDialog   | [![NuGet Version](https://img.shields.io/nuget/v/MintPlayer.BrowserDialog.svg?style=flat)](https://www.nuget.org/packages/MintPlayer.BrowserDialog)     | [![NuGet Version](https://img.shields.io/nuget/vpre/MintPlayer.BrowserDialog.svg?style=flat)](https://www.nuget.org/packages/MintPlayer.BrowserDialog)     | [![NuGet](https://img.shields.io/nuget/dt/MintPlayer.BrowserDialog.svg?style=flat)](https://www.nuget.org/packages/MintPlayer.BrowserDialog)     |

## Installation
### NuGet package manager
Open the NuGet package manager and install the `MintPlayer.PlatformBrowser` package in the project
### Package manager console

    Install-Package MintPlayer.PlatformBrowser

## Usage
Simply call the following method:

    var browsers = PlatformBrowser.GetInstalledBrowsers();

## Copy-n-paste code sample

    var browsers = PlatformBrowser.GetInstalledBrowsers();
    foreach (var browser in browsers)
    {
        Console.WriteLine($"Browser: {browser.Name}");
        Console.WriteLine($"Executable: {browser.ExecutablePath}");
        Console.WriteLine($"Icon path: {browser.IconPath}");
        Console.WriteLine($"Icon index: {browser.IconIndex}");
        Console.WriteLine();
    }

## Contributors

| Pull-request                                                                                            | Contributor                                       |
|---------------------------------------------------------------------------------------------------------|---------------------------------------------------|
| [Target .NET Standard](https://github.com/MintPlayer/MintPlayer.PlatformBrowser/pull/1)                 | [merijndejonge](https://github.com/merijndejonge) |
| [Also lists browsers from CurrentUser](https://github.com/MintPlayer/MintPlayer.PlatformBrowser/pull/2) | [mderu](https://github.com/mderu)                 |
