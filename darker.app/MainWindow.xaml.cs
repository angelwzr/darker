using darker.Helpers;
using darker.Models;
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
            SetTrayIcon();
            SetDarkerAppTheme();

            if (AppSettings.Default.IsAutoUpdateEnabled)
                UpdateHelper.CheckForUpdates();
        }

        private void TrayIconClick(object sender, RoutedEventArgs e)
        {
            var themeSettings = AppSettings.Default.ThemeMode;

            switch (themeSettings)
            {
                case SettingsThemeMode.Both:
                    ThemeHelper.SwitchWindowsTheme();
                    ThemeHelper.SwitchAppsTheme();
                    SetDarkerAppTheme();
                    SetTrayIcon();
                    break;

                case SettingsThemeMode.OnlySystem:
                    ThemeHelper.SwitchWindowsTheme();
                    SetTrayIcon();
                    break;

                case SettingsThemeMode.OnlyApps:
                    ThemeHelper.SwitchAppsTheme();
                    SetDarkerAppTheme();
                    break;
            }
        }

        //Reset menu item
        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            RegistryThemeHelper.ResetTheme();
            SetTrayIcon();
            SetDarkerAppTheme();
        }

        //Settings menu item
        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            var settingsWindow = Application.Current.Windows.OfType<SettingsWindow>().FirstOrDefault();
            if (settingsWindow == null)
            {
                settingsWindow = new SettingsWindow();
                settingsWindow.Show();
            }
            else
            {
                settingsWindow.Focus();
            }
        }

        //Exit menu item
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        //Setting the right icon for each theme
        private void SetTrayIcon()
        {
            var theme = RegistryThemeHelper.GetWindowsTheme();

            darkerIcon.IconSource = theme == UITheme.Light
                ? new BitmapImage(new Uri(@"pack://application:,,,/Resources/night_b.ico"))
                : new BitmapImage(new Uri(@"pack://application:,,,/Resources/day_w.ico"));
        }

        private void SetDarkerAppTheme()
        {
            var apptheme = RegistryThemeHelper.GetAppsTheme();
            if (apptheme == UITheme.Light)
            {
                Application.Current.Resources.MergedDictionaries.Clear();
                Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary() { Source = new Uri("/Resources/Themes/LightTheme.xaml", UriKind.Relative) });
            }
            else
            {
                Application.Current.Resources.MergedDictionaries.Clear();
                Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary() { Source = new Uri("/Resources/Themes/DarkTheme.xaml", UriKind.Relative) });
            }
        }

        //Single instance management for app windows
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
                var subWindow = (Window)Activator.CreateInstance(typeof(T));
                subWindow.Show();
            }
        }
    }
}