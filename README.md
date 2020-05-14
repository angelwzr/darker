# darker

[![CodeFactor](https://www.codefactor.io/repository/github/angelwzr/darker/badge/master)](https://www.codefactor.io/repository/github/angelwzr/darker/overview/master)

This simple tray app for Windows 10 allows you to switch system theme (Dark/Light) with one click, so you won't need to go to the system theme settings page every time to change it. You can also set it to auto start with Windows.

This repository also includes all of the code for darker mini-homepage, available at [mswin.me/darker](https://mswin.me/darker).

### Requirements

- .NET Core Desktop Runtime 3.1 ([download](https://dotnet.microsoft.com/download/dotnet-core/current/runtime))

### Current status and future plans

Version 1.0 is available for [download right now](https://github.com/angelwzr/darker/releases).

These are some things I want to fix, change or add in the future:

- Native-looking context menu
- Settings page
- Option to switch only system theme or app theme
- Auto theme switching on schedule 
- Proper app versioning
- .MSIX installer bundle
- Publish to Microsoft Store
- More languages (feel free to contact me if you want to help with translation, it's only 10 strings)

### Notes

Windows 10 version 1903+ is required for full theme switching. However, the app can be used on earlier Windows 10 versions to switch mode for apps, not affecting the whole system look as there is no Light theme present. High contrast themes are not supported.

### Acknowledgements

- [Hardcodet NotifyIcon for WPF](https://github.com/hardcodet/wpf-notifyicon)