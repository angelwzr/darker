using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Reflection;
using System.Resources;
using System.Threading;
using System.Windows;

namespace darker
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        //mutex
        private const string MutexName = "##||darker||##";
        private readonly Mutex _mutex;
        private readonly bool _createdNew;

        public App()
        {
            //check Windows version before anything else
            CheckWin();
            //allow single instance of the app only
            _mutex = new Mutex(true, MutexName, out _createdNew);
            if (_createdNew) return;
            var resourceManager = new ResourceManager(typeof(Properties.Resources));
            MessageBox.Show(resourceManager.GetString("RunningAppMessage"), resourceManager.GetString("AppName"));
            Current.Shutdown(0);
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            if (!_createdNew) return;
            var mw = new MainWindow();
            mw.Hide();
        }

        //actual Windows version check code
        private void CheckWin()
        {
            if (Environment.OSVersion.Version.Major >= 10 || Environment.OSVersion.Version.Minor <= 0) return;
            var resourceManager = new ResourceManager(typeof(Properties.Resources));
            MessageBox.Show(resourceManager.GetString("OSVersionMessage"), resourceManager.GetString("AppName"));
            Current.Shutdown();
        }

        //implementing application settings
        public class AppSettings
        {
            private AppSettings()
            {
                // marked as private to prevent outside classes from creating new.
            }

            private static string _jsonSource;
            private static AppSettings _appSettings = null;

            public static AppSettings Default
            {
                get
                {
                    if (_appSettings != null) return _appSettings;
                    var builder = new ConfigurationBuilder()
                        .SetBasePath(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location))
                        .AddJsonFile("appsettings.json", false, true);

                    _jsonSource = $"{Path.GetDirectoryName(Assembly.GetEntryAssembly().Location)}\\appsettings.json";

                    var config = builder.Build();
                    _appSettings = new AppSettings();
                    config.Bind(_appSettings);

                    return _appSettings;
                }
            }

            public void Save()
            {
                // open config file
                var json = JsonConvert.SerializeObject(_appSettings);

                //write string to file
                File.WriteAllText(_jsonSource, json);
            }

            public string ThemeMode { get; set; }
        }
    }
}