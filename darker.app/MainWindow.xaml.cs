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
            using RegistryKey key = Registry.CurrentUser.OpenSubKey(RegistryKeyPathTheme);
            object registryValueObject = key.GetValue(RegSysMode);
            if (registryValueObject == null)
            {
                return WindowsTheme.Light;
            }
            int registryValue = (int)registryValueObject;

            return registryValue > 0 ? WindowsTheme.Light : WindowsTheme.Dark;
        }

        private static AppsTheme GetAppsTheme()
        {
            using RegistryKey key = Registry.CurrentUser.OpenSubKey(RegistryKeyPathTheme);
            object registryValueObject = key.GetValue(RegAppMode);
            if (registryValueObject == null)
            {
                return AppsTheme.Light;
            }
            int registryValue = (int)registryValueObject;

            return registryValue > 0 ? AppsTheme.Light : AppsTheme.Dark;
        }

        //setting the right icon for each theme
        private void IconHandler()
        {
            WindowsTheme theme = GetWindowsTheme();
            if (theme == WindowsTheme.Light)
            {
                darkerIcon.IconSource = new BitmapImage(new Uri(@"pack://application:,,,/Resources/night_b.ico"));
            }
            else
            {
                darkerIcon.IconSource = new BitmapImage(new Uri(@"pack://application:,,,/Resources/day_w.ico"));
            }
        }

        //system theme switching
        private void SysThemeHandler()
        {
            WindowsTheme theme = GetWindowsTheme();
            if (theme == WindowsTheme.Light)
            {
                using RegistryKey key = Registry.CurrentUser.CreateSubKey(RegistryKeyPathTheme);
                {
                    key.SetValue(RegSysMode, $"0", RegistryValueKind.DWord);
                    key.Close();
                }
            }
            else
            {
                using RegistryKey key = Registry.CurrentUser.CreateSubKey(RegistryKeyPathTheme);
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
            AppsTheme apptheme = GetAppsTheme();
            if (apptheme == AppsTheme.Light)
            {
                using RegistryKey key = Registry.CurrentUser.CreateSubKey(RegistryKeyPathTheme);
                {
                    key.SetValue(RegAppMode, $"0", RegistryValueKind.DWord);
                    key.Close();
                }
            }
            else
            {
                using RegistryKey key = Registry.CurrentUser.CreateSubKey(RegistryKeyPathTheme);
                {
                    key.SetValue(RegAppMode, $"1", RegistryValueKind.DWord);
                    key.Close();
                }
            }
        }

        //do the magic on tray icon click
        private void MagicHandler(object sender, RoutedEventArgs e)
        {
            SysThemeHandler();
            AppThemeHandler();
            IconHandler();
        }

        //settings button
        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            OpenWindow<Settings>();
        }

        //about button
        private void About_Click(object sender, RoutedEventArgs e)
        {
            OpenWindow<About>();
        }

        //exit button
        void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        //window single instance management
        public static void OpenWindow<T>() where T : Window
        {
            var windows = System.Windows.Application.Current.Windows.Cast<Window>();
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