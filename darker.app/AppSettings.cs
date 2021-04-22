using System;
using System.IO;
using System.Reflection;
using System.Resources;
using System.Text.Json;
using darker.Models;
using darker.Properties;
using darker.Viewes;

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

        public bool IsAutoThemeChangingEnabled { get; set; } = false;

        public int ThemeChangingMorningHour { get; set; } = 08;
        public int ThemeChangingMorningMin { get; set; } = 00;

        public int ThemeChangingEveningHour { get; set; } = 12;
        public int ThemeChangingEveningMin { get; set; } = 00;

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
                        ErrorWindow errwindow = new ErrorWindow();
                        var resourceManager = new ResourceManager(typeof(Resources));
                        errwindow.ErrorText.Text = resourceManager.GetString("Error_FileAccessErrorMessage");
                        errwindow.ShowAsync();
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

                ErrorWindow errwindow = new ErrorWindow();
                var resourceManager = new ResourceManager(typeof(Resources));
                errwindow.ErrorText.Text = resourceManager.GetString("Error_FileAccessErrorMessage");
                errwindow.ShowAsync();
            }
        }
    }
}