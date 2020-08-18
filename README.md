# MintPlayer.PlatformBrowser
[![NuGet Version](https://img.shields.io/nuget/v/MintPlayer.PlatformBrowser.svg?style=flat)](https://www.nuget.org/packages/MintPlayer.PlatformBrowser)
[![NuGet](https://img.shields.io/nuget/dt/MintPlayer.PlatformBrowser.svg?style=flat)](https://www.nuget.org/packages/MintPlayer.PlatformBrowser)
[![Build Status](https://travis-ci.org/MintPlayer/MintPlayer.PlatformBrowser.svg?branch=master)](https://travis-ci.org/MintPlayer/MintPlayer.PlatformBrowser)
[![License](https://img.shields.io/badge/License-Apache%202.0-green.svg)](https://opensource.org/licenses/Apache-2.0)
[![Codacy Badge](https://app.codacy.com/project/badge/Grade/c0cc807ae50645ca909b68c95f2275d0)](https://www.codacy.com/gh/MintPlayer/MintPlayer.PlatformBrowser?utm_source=github.com&amp;utm_medium=referral&amp;utm_content=MintPlayer/MintPlayer.PlatformBrowser&amp;utm_campaign=Badge_Grade)

This package allows you to retrieve the web browsers (including Microsoft Edge) installed on the system.
## Installation
### NuGet package manager
Open the NuGet package manager and install the **MintPlayer.PlatformBrowser** package in the project
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
