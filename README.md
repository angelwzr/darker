# darker

![Website](https://img.shields.io/website?url=https%3A%2F%2Fmswin.me%2Fdarker%2F) ![GitHub All Releases](https://img.shields.io/github/downloads/angelwzr/darker/total) ![GitHub issues](https://img.shields.io/github/issues/angelwzr/darker) ![GitHub release (latest by date)](https://img.shields.io/github/v/release/angelwzr/darker) ![GitHub commits since latest release (by date)](https://img.shields.io/github/commits-since/angelwzr/darker/1.1.0.0) [![CodeFactor](https://www.codefactor.io/repository/github/angelwzr/darker/badge/master)](https://www.codefactor.io/repository/github/angelwzr/darker/overview/master)

This simple tray app for Windows 10 allows you to switch system or applications theme (Dark/Light) with one click, so you won't need to go to the system settings page every time to change it. 

This repository also includes Inno Script Studio config file (darker.setup) for building an installation package and all of the code for darker mini-homepage (darker.site), available at [mswin.me/darker](https://mswin.me/darker).

### Download

Version 1.1 is available for [download right now](https://github.com/angelwzr/darker/releases).

### Requirements

- .NET Core Desktop Runtime 3.1 ([download](https://dotnet.microsoft.com/download/dotnet-core/current/runtime))
- Windows 10 version 1903+

### Features

- One-click theme changing
- Theme changing options: **Both** (system + apps), **Apps only** or **System only**
- Reset to default button (applies Light theme to everything)
- Available in English, Russian, French and Ukrainian languages
- Auto update (1.1+)

### Future plans

These are some things I want to fix, change or add in the future:

- Wallpaper changing
- Auto theme switching on schedule
- More languages (feel free to submit additional translations)

### Important notes and known bugs

- **Changes in distribution**. Starting with version 1.1, darker is available for download either as a portable package in a .zip archive or a full installer. 
- **Switching to x64 releases only**. With aim to provide a smooth and reliable auto update experience, I'm forced to switch to providing only one binary for the app. However. NET Core apps doesn't support AnyCPU binary compilation as of right now, so publishing an universal package for both architectures isn't possible. If you need to use darker on 32-bit machines, version 1.0 remains available on the releases tab. 64-bit binaries will work on most of the modern systems. 
- **Icon context menu theme doesn't change until app is relaunched**. Tray icon in this project is a Windows Forms component and requires a different approach to dynamically switch resources when theme is changed. Not fixed in 1.1.
