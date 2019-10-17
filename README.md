# BrowserDialog
![Dialog that lets the user pick from the installed webbrowsers](https://github.com/MintPlayer/BrowserDialog/blob/master/BrowserDialog.png)
## NuGet package
https://www.nuget.org/packages/MintPlayer.BrowserDialog/
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
