using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using darker.Models;
using Microsoft.Win32;

namespace darker
{
    /// <summary>
    /// Interaction logic for SettingsGeneral.xaml
    /// </summary>
    public partial class SettingsGeneral : Page
    {
        public SettingsGeneral()
        {
            InitializeComponent();
        }

        private void Both_OnChecked(object sender, RoutedEventArgs e)
        {
            AppSettings.Default.ThemeMode = SettingsThemeMode.Both;
            AppSettings.Default.Save();
        }

        private void SysOnly_OnChecked(object sender, RoutedEventArgs e)
        {
            AppSettings.Default.ThemeMode = SettingsThemeMode.OnlySystem;
            AppSettings.Default.Save();
        }

        private void AppsOnly_OnChecked(object sender, RoutedEventArgs e)
        {
            AppSettings.Default.ThemeMode = SettingsThemeMode.OnlyApps;
            AppSettings.Default.Save();
        }

        private void AutoU_OnChecked(object sender, RoutedEventArgs e)
        {
            AppSettings.Default.IsAutoUpdateEnabled = true;
            AppSettings.Default.Save();
        }

        private void AutoU_OnUnchecked(object sender, RoutedEventArgs e)
        {
            AppSettings.Default.IsAutoUpdateEnabled = false;
            AppSettings.Default.Save();
        }

        private void AutoS_OnChecked(object sender, RoutedEventArgs e)
        {
            using var key =
                Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);
            key.SetValue("darker", Process.GetCurrentProcess().MainModule.FileName);
        }

        private void AutoS_OnUnchecked(object sender, RoutedEventArgs e)
        {
            using var key =
                Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);
            key.DeleteValue("darker", false);
        }
    }
}
