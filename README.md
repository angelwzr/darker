# darker

![app demo](https://mswin.me/cdn/darkeranim.gif)

This simple tray app for Windows 10 allows you to switch system theme (Dark/Light) with a click, which may be helpful when you have to take a lot of screenshots so you won't need to go to the Settings app page every time to change it. You can also set it to auto start with Windows.

### Requirements

- .NET Core Runtime 3.1 ([download](https://dotnet.microsoft.com/download/dotnet-core/current/runtime))

### Current status

The app is basically done. There are some things I want to fix, change or add in the future:

- Make context menu look native
- Localization support

### Notes

The app will work on all Windows 10 versions with Dark and Light mode for apps support. Light theme for system was first introduced in Windows 10 1903 so you'll need to be using this OS version or newer for proper switching. High contrast themes are not supported.

### Acknowledgements

- [Hardcodet NotifyIcon for WPF](https://github.com/hardcodet/wpf-notifyicon)