using darker.Helpers;
using darker.Models;
using FluentScheduler;
using Hardcodet.Wpf.TaskbarNotification;
using NHotkey;
using NHotkey.Wpf;
using System;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace darker
{
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            SetTrayIcon();

            //Update cheker
            if (AppSettings.Default.IsAutoUpdateEnabled)
                UpdateHelper.CheckForUpdates();

            //Implementing hotkeys
            if (AppSettings.Default.IsHotKeyEnabled)
                HotkeyManager.Current.AddOrReplace("Switch", Key.D, ModifierKeys.Control | ModifierKeys.Alt, OnHotKey);

            if (AppSettings.Default.IsAutoThemeChangingEnabled)
            {
                var jobregistry = new FluentScheduler.Registry();
                jobregistry.Schedule(ShowDebugBalloon).ToRunEvery(1).Days().At(AppSettings.Default.ThemeChangingMorningHour, AppSettings.Default.ThemeChangingMorningMin);
                jobregistry.Schedule(ShowDebugBalloon).ToRunEvery(1).Days().At(AppSettings.Default.ThemeChangingEveningHour, AppSettings.Default.ThemeChangingEveningMin);

                JobManager.Initialize(jobregistry);
            }

            }


        private void ShowDebugBalloon()
        {
            string title = "Debug notification";
            string text = "This is a debug notification for timer work check";

            //show balloon with built-in icon
            darkerIcon.ShowBalloonTip(title, text, BalloonIcon.Error);
        }

        private void OnHotKey(object sender, HotkeyEventArgs e)
        {
            TrayIconClick(sender, null);
        }

        public void TrayIconClick(object sender, RoutedEventArgs e)
        {
            var themeSettings = AppSettings.Default.ThemeMode;

            switch (themeSettings)
            {
                case SettingsThemeMode.Both:
                    ThemeHelper.SwitchWindowsTheme();
                    ThemeHelper.SwitchAppsTheme();
                    SetTrayIcon();
                    break;

                case SettingsThemeMode.OnlySystem:
                    ThemeHelper.SwitchWindowsTheme();
                    SetTrayIcon();
                    break;

                case SettingsThemeMode.OnlyApps:
                    ThemeHelper.SwitchAppsTheme();
                    break;
            }
        }

        //Settings menu item
        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            var navWindow = Application.Current.Windows.OfType<NavWindow>().FirstOrDefault();
            if (navWindow == null)
            {
                navWindow = new NavWindow();
                navWindow.Show();
            }
            else
            {
                navWindow.Focus();
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
                var subWindow = (Window) Activator.CreateInstance(typeof(T));
                subWindow.Show();
            }
        }
    }
}