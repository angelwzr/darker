using Microsoft.Win32;
using System;
using System.Diagnostics;
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
            CheckForAutostart();
            IconHandler();
        }

        //start with windows windows
        private void CheckForAutostart()
        {
            RegistryKey reg = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run");
            if (reg != null)
            {
                string sVal = reg.GetValue("darker", "").ToString();
                AutoS.IsChecked = sVal == System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;
                reg.Close();
            }
        }

        private void IconHandler()
        {
            WindowsTheme theme = GetWindowsTheme();
            if (theme == WindowsTheme.Light)
            {
                MyNotifyIcon.IconSource = new BitmapImage(new Uri(@"pack://application:,,,/Resources/night_b.ico"));
            }
            else
            {
                MyNotifyIcon.IconSource = new BitmapImage(new Uri(@"pack://application:,,,/Resources/day_w.ico"));
            }
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

        //theme switching button
        private void ThemeCycle(object sender, RoutedEventArgs e)
        {
            WindowsTheme theme = GetWindowsTheme();
            if (theme == WindowsTheme.Light)
            {
                using RegistryKey key = Registry.CurrentUser.CreateSubKey(RegistryKeyPathTheme);
                {
                    key.SetValue(RegSysMode, $"0", RegistryValueKind.DWord);
                    key.SetValue(RegAppMode, $"0", RegistryValueKind.DWord);
                    key.Close();
                }
            }
            else
            {
                using RegistryKey key = Registry.CurrentUser.CreateSubKey(RegistryKeyPathTheme);
                {
                    key.SetValue(RegSysMode, $"1", RegistryValueKind.DWord);
                    key.SetValue(RegAppMode, $"1", RegistryValueKind.DWord);
                    key.SetValue(RegColPMode, $"0", RegistryValueKind.DWord);
                    key.Close();
                }
            }
            IconHandler();
        }

        //launch on startup button
        private void AutoS_Checked(object sender, RoutedEventArgs e)
        {
            using RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);
            key.SetValue("darker", System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);
        }

        private void AutoS_Unchecked(object sender, RoutedEventArgs e)
        {
            using RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);
            key.DeleteValue("darker", false);
        }

        //settings button
        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(new ProcessStartInfo("ms-settings:colors") { UseShellExecute = true });
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
        
        //about window single instance management
        public static void OpenWindow<T>() where T : Window
        {
            var windows = System.Windows.Application.Current.Windows.Cast<Window>();
            var any = windows.Any(s => s is T);
            if (any)
            {
                var abWindow = windows.Where(s => s is T).ToList()[0];
                if (abWindow.WindowState == WindowState.Minimized)
                abWindow.WindowState = WindowState.Normal;
                abWindow.Focus();
            }
            else
            {
                var abWindow = (Window)Activator.CreateInstance(typeof(T));
                abWindow.Show();
            }
        }
    }
}