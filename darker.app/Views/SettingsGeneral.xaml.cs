using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using darker.Models;
using Microsoft.Win32;

namespace darker.Views
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

        private void OnLoad(object sender, RoutedEventArgs e)
        {
            //Check for autostart with Windows key
            CheckForAutostart();

            //Theme mode combobox state
            //switch (AppSettings.Default.ThemeMode)
            //{
            //case SettingsThemeMode.Both:
            //Both.IsChecked = true;
            //break;

            //case SettingsThemeMode.OnlySystem:
            //SysOnly.IsChecked = true;
            //break;

            //case SettingsThemeMode.OnlyApps:
            //AppsOnly.IsChecked = true;
            //break;
            //}

            //AutoUpadte toggle state
            switch (AppSettings.Default.IsAutoUpdateEnabled)
            {
                case true:
                    AutoUpdateToggle.IsOn = true;
                    break;

                case false:
                    AutoUpdateToggle.IsOn = false;
                    break;
            }
        }

        //Start with Windows toggle state
        private void CheckForAutostart()
        {
            using var reg = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run");
            if (reg != null)
            {
                var sVal = reg.GetValue("darker", "").ToString();
                AutoStartToggle.IsOn = sVal == Process.GetCurrentProcess().MainModule.FileName;
                reg.Close();
            }
        }

        private void AutoStart_Toggled(object sender, RoutedEventArgs e)
        {
            if (AutoStartToggle.IsOn)
            {
                using var key =
                    Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);
                key.SetValue("darker", Process.GetCurrentProcess().MainModule.FileName);
            }
            else
            {
                using var key =
                    Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);
                key.DeleteValue("darker", false);
            }
        }

        private void AutoUpdate_Toggled(object sender, RoutedEventArgs e)
        {
            if (AutoUpdateToggle.IsOn)
            {
                AppSettings.Default.IsAutoUpdateEnabled = true;
                AppSettings.Default.Save();
            }
            else
            {
                AppSettings.Default.IsAutoUpdateEnabled = false;
                AppSettings.Default.Save();
            }
        }

        private void IconSwitchMode_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var modeName = e.AddedItems[0].ToString();
            switch (modeName)
            {
                case "Both":
                    AppSettings.Default.ThemeMode = SettingsThemeMode.Both;
                    AppSettings.Default.Save();
                    break;
                case "Only system":
                    AppSettings.Default.ThemeMode = SettingsThemeMode.OnlySystem;
                    AppSettings.Default.Save();
                    break;
                case "Only apps":
                    AppSettings.Default.ThemeMode = SettingsThemeMode.OnlyApps;
                    AppSettings.Default.Save();
                    break;
            }
        }
    }
}