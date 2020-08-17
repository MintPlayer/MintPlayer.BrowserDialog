# BrowserDialog
[![NuGet Version](https://img.shields.io/nuget/v/MintPlayer.BrowserDialog.svg?style=flat)](https://www.nuget.org/packages/MintPlayer.BrowserDialog/)
[![NuGet](https://img.shields.io/nuget/dt/MintPlayer.BrowserDialog.svg?style=flat)](https://www.nuget.org/packages/MintPlayer.BrowserDialog)
[![Build Status](https://travis-ci.org/MintPlayer/MintPlayer.BrowserDialog.svg?branch=master)](https://travis-ci.org/MintPlayer/MintPlayer.BrowserDialog)
![.NET Core Desktop](https://github.com/MintPlayer/MintPlayer.BrowserDialog/workflows/.NET%20Core%20Desktop/badge.svg)
[![License](https://img.shields.io/badge/License-Apache%202.0-green.svg)](https://opensource.org/licenses/Apache-2.0)
[![Codacy Badge](https://app.codacy.com/project/badge/Grade/2a28efb4433543afb96b32c5306a5b3b)](https://www.codacy.com/gh/MintPlayer/MintPlayer.BrowserDialog?utm_source=github.com&amp;utm_medium=referral&amp;utm_content=MintPlayer/MintPlayer.BrowserDialog&amp;utm_campaign=Badge_Grade)

Dialog that lets the user pick from the installed webbrowsers

## Preview
![Dialog that lets the user pick from the installed webbrowsers](https://github.com/MintPlayer/BrowserDialog/blob/master/BrowserDialog.png)
## Installation
### NuGet package manager
Open the NuGet package manager and install the **MintPlayer.BrowserDialog** package in the project
### Package manager console
    Install-Package MintPlayer.BrowserDialog
## Usage

    var dialog = new BrowserDialog();
    if (dialog.ShowDialog() == DialogResult.OK)
    {
        MessageBox.Show($"You picked {dialog.SelectedBrowser.Name}.\r\nThe executable path is {dialog.SelectedBrowser.ExecutablePath}");
    }
