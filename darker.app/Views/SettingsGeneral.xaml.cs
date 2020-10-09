using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using darker.Models;
using darker.Viewes;
using Microsoft.Win32;

namespace darker.Views
{
    /// <summary>
    ///     Interaction logic for SettingsGeneral.xaml
    /// </summary>
    public partial class SettingsGeneral : System.Windows.Controls.Page
    {
        public SettingsGeneral()
        {
            InitializeComponent();

            if (AppSettings.Default.IsAutoThemeChangingEnabled)
                ShowSchedulerFrameButton.IsEnabled = true;
        }

        private void OnLoad(object sender, RoutedEventArgs e)
        {
            //Check for autostart with Windows key
            CheckForAutostart();

            //ThemeMode combobox state
            switch (AppSettings.Default.ThemeMode)
            {
                case SettingsThemeMode.Both:
                    ModeComboBox.SelectedItem = Both;
                    break;

                case SettingsThemeMode.OnlySystem:
                    ModeComboBox.SelectedItem = SysOnly;
                    break;

                case SettingsThemeMode.OnlyApps:
                    ModeComboBox.SelectedItem = AppsOnly;
                    break;
            }

            //AutoThemeChanging toggle state
            switch (AppSettings.Default.IsAutoThemeChangingEnabled)
            {
                case true:
                    AutoThemeChangingToggle.IsOn = true;
                    break;

                case false:
                    AutoThemeChangingToggle.IsOn = false;
                    break;
            }

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
            var comboBoxItem = e.AddedItems[0] as ComboBoxItem;
            if (comboBoxItem == null) return;
            var name = comboBoxItem.Name;
            switch (name)
            {
                case "Both":
                    AppSettings.Default.ThemeMode = SettingsThemeMode.Both;
                    AppSettings.Default.Save();
                    break;
                case "SysOnly":
                    AppSettings.Default.ThemeMode = SettingsThemeMode.OnlySystem;
                    AppSettings.Default.Save();
                    break;
                case "AppsOnly":
                    AppSettings.Default.ThemeMode = SettingsThemeMode.OnlyApps;
                    AppSettings.Default.Save();
                    break;
            }
        }

        private async void ShowSchedulerFrameButton_Click(object sender, RoutedEventArgs e)
        {
            SettingsSchedulerDialog dialog = new SettingsSchedulerDialog();
            var result = await dialog.ShowAsync();
        }

        private void AutoThemeChanging_Toggled(object sender, RoutedEventArgs e)
        {
            if (AutoThemeChangingToggle.IsOn)
            {
                ShowSchedulerFrameButton.IsEnabled = true;
                AppSettings.Default.IsAutoThemeChangingEnabled = true;
                AppSettings.Default.Save();
            }
            else
            {
                ShowSchedulerFrameButton.IsEnabled = false;
                AppSettings.Default.IsAutoThemeChangingEnabled = false;
                AppSettings.Default.Save();
            }
        }
    }
}