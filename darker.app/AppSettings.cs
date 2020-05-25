using darker.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.Json;

namespace darker
{
    /// <summary>
    /// Application settings
    /// </summary>
    public class AppSettings
    {
        private static string _settingsPath = $"{Path.GetDirectoryName(Assembly.GetEntryAssembly().Location)}\\appsettings.json";

        private static AppSettings _appSettings = null;

        private AppSettings()
        {
            // marked as private to prevent outside classes from creating new.
        }

        public SettingsThemeMode ThemeMode { get; set; }

        public bool IsAutoUpdateEnabled { get; set; } = true;

        public static AppSettings Default
        {
            get
            {
                if (_appSettings != null) 
                    return _appSettings;

                var json = File.ReadAllText(_settingsPath);
                _appSettings = JsonSerializer.Deserialize<AppSettings>(json);

                return _appSettings;
            }
        }

        public void Save()
        {
            // open config file
            var json = JsonSerializer.Serialize(_appSettings);

            //write string to file
            File.WriteAllText(_settingsPath, json);
        }
    }
}
