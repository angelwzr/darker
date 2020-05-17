using Microsoft.Win32;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Media.Imaging;

namespace darker
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            IconHandler();

            var t = App.AppSettings.Default.ThemeMode;
            App.AppSettings.Default.ThemeMode = "1";
            App.AppSettings.Default.Save();
        }

        //theme reg keys
        private const string RegistryKeyPathTheme = @"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize";
        private const string RegSysMode = "SystemUsesLightTheme";
        private const string RegAppMode = "AppsUseLightTheme";
        private const string RegColPMode = "ColorPrevalence";

        //get current theme
        private enum WindowsTheme
        {
            Light,
            Dark
        }

        private enum AppsTheme
        {
            Light,
            Dark
        }

        private static WindowsTheme GetWindowsTheme()
        {
            using var key = Registry.CurrentUser.OpenSubKey(RegistryKeyPathTheme);
            var registryValueObject = key.GetValue(RegSysMode);
            if (registryValueObject == null) return WindowsTheme.Light;
            var registryValue = (int) registryValueObject;

            return registryValue > 0 ? WindowsTheme.Light : WindowsTheme.Dark;
        }

        private static AppsTheme GetAppsTheme()
        {
            using var key = Registry.CurrentUser.OpenSubKey(RegistryKeyPathTheme);
            var registryValueObject = key.GetValue(RegAppMode);
            if (registryValueObject == null) return AppsTheme.Light;
            var registryValue = (int) registryValueObject;

            return registryValue > 0 ? AppsTheme.Light : AppsTheme.Dark;
        }

        //setting the right icon for each theme
        private void IconHandler()
        {
            var theme = GetWindowsTheme();
            darkerIcon.IconSource = theme == WindowsTheme.Light
                ? new BitmapImage(new Uri(@"pack://application:,,,/Resources/night_b.ico"))
                : new BitmapImage(new Uri(@"pack://application:,,,/Resources/day_w.ico"));
        }

        //system theme switching
        private void SysThemeHandler()
        {
            var theme = GetWindowsTheme();
            if (theme == WindowsTheme.Light)
            {
                using var key = Registry.CurrentUser.CreateSubKey(RegistryKeyPathTheme);
                {
                    key.SetValue(RegSysMode, $"0", RegistryValueKind.DWord);
                    key.Close();
                }
            }
            else
            {
                using var key = Registry.CurrentUser.CreateSubKey(RegistryKeyPathTheme);
                {
                    key.SetValue(RegSysMode, $"1", RegistryValueKind.DWord);
                    key.SetValue(RegColPMode, $"0", RegistryValueKind.DWord);
                    key.Close();
                }
            }
        }

        //apps theme switching
        private void AppThemeHandler()
        {
            var apptheme = GetAppsTheme();
            if (apptheme == AppsTheme.Light)
            {
                using var key = Registry.CurrentUser.CreateSubKey(RegistryKeyPathTheme);
                {
                    key.SetValue(RegAppMode, $"0", RegistryValueKind.DWord);
                    key.Close();
                }
            }
            else
            {
                using var key = Registry.CurrentUser.CreateSubKey(RegistryKeyPathTheme);
                {
                    key.SetValue(RegAppMode, $"1", RegistryValueKind.DWord);
                    key.Close();
                }
            }
        }

        //reset theme settings
        private void ResetTheme()
        {
            using var key = Registry.CurrentUser.CreateSubKey(RegistryKeyPathTheme);
            key.SetValue(RegSysMode, $"1", RegistryValueKind.DWord);
            key.SetValue(RegAppMode, $"1", RegistryValueKind.DWord);
            key.SetValue(RegColPMode, $"0", RegistryValueKind.DWord);
            key.Close();
            IconHandler();
        }

        //code for changing both system and apps theme
        private void ChangeBoth()
        {
            SysThemeHandler();
            AppThemeHandler();
            IconHandler();
        }

        //code for changing only system theme
        private void ChangeSys()
        {
            SysThemeHandler();
            IconHandler();
        }

        //code for changing only apps theme
        private void ChangeApps()
        {
            AppThemeHandler();
        }

        //do the magic on tray icon click
        private void MagicHandler(object sender, RoutedEventArgs e)
        {
            if (App.AppSettings.Default.ThemeMode.Equals("1"))
                ChangeBoth();
            else if (App.AppSettings.Default.ThemeMode.Equals("2"))
                ChangeSys();
            else if (App.AppSettings.Default.ThemeMode.Equals("3")) ChangeApps();
        }

        //reset button
        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            ResetTheme();
        }

        //settings button
        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            OpenWindow<Settings>();
        }

        //exit button
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        //window single instance management
        public static void OpenWindow<T>() where T : Window
        {
            var windows = Application.Current.Windows.Cast<Window>();
            var any = windows.Any(s => s is T);
            if (any)
            {
                var subWindow = windows.Where(s => s is T).ToList()[0];
                if (subWindow.WindowState == WindowState.Minimized)
                    subWindow.WindowState = WindowState.Normal;
                subWindow.Focus();
            }
            else
            {
                var subWindow = (Window) Activator.CreateInstance(typeof(T));
                subWindow.Show();
            }
        }
    }
}