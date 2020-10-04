using System;
using System.IO;
using System.Reflection;
using System.Resources;
using System.Text.Json;
using System.Windows;
using darker.Models;
using darker.Properties;

namespace darker
{
    /// <summary>
    ///     Application settings
    /// </summary>
    public class AppSettings
    {
        private static readonly string _settingsPath =
            $"{Path.GetDirectoryName(Assembly.GetEntryAssembly().Location)}\\appsettings.json";

        private static AppSettings _appSettings;

        private AppSettings()
        {
            // Marked as private to prevent outside classes from creating new.
        }

        public SettingsThemeMode ThemeMode { get; set; } = SettingsThemeMode.Both;


        public bool IsAutoUpdateEnabled { get; set; } = true;

        public bool IsHotKeyEnabled { get; set; } = true;

        public static AppSettings Default
        {
            get
            {
                if (_appSettings != null)
                    return _appSettings;

                if (File.Exists(_settingsPath))
                    try
                    {
                        var json = File.ReadAllText(_settingsPath);
                        _appSettings = JsonSerializer.Deserialize<AppSettings>(json);
                    }
                    catch (Exception configreadaccessEx)
                    {
                        var resourceManager = new ResourceManager(typeof(Resources));
                        MessageBox.Show(resourceManager.GetString("FileAccessErrorMessage"),
                            resourceManager.GetString("AppName"));
                    }
                else
                    _appSettings = new AppSettings();

                return _appSettings;
            }
        }

        public void Save()
        {
            // Open config file
            var json = JsonSerializer.Serialize(_appSettings);

            try
            {
                //Write string to file
                File.WriteAllText(_settingsPath, json);
            }
            catch (Exception configwriteaccessEx)
            {
                var resourceManager = new ResourceManager(typeof(Resources));
                MessageBox.Show(resourceManager.GetString("FileAccessErrorMessage"),
                    resourceManager.GetString("AppName"));
            }
        }
    }
}