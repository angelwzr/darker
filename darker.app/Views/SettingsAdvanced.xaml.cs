using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using darker.Helpers;

namespace darker.Views
{
    /// <summary>
    ///     Interaction logic for SettingsAdvanced.xaml
    /// </summary>
    public partial class SettingsAdvanced : Page
    {
        public SettingsAdvanced()
        {
            InitializeComponent();
        }

        private void OnLoad(object sender, RoutedEventArgs e)
        {
            switch (AppSettings.Default.IsHotKeyEnabled)
            {
                case true:
                    HotkeyToggle.IsOn = true;
                    break;

                case false:
                    HotkeyToggle.IsOn = false;
                    break;
            }

            //AutoU checkbox
            switch (AppSettings.Default.IsDebugEnabled)
            {
                case true:
                    DebugCheckbox.IsChecked = true;
                    break;

                case false:
                    DebugCheckbox.IsChecked = false;
                    break;
            }

        }

        private void Hotkey_Toggled(object sender, RoutedEventArgs e)
        {
            if (HotkeyToggle.IsOn)
            {
                AppSettings.Default.IsHotKeyEnabled = true;
                AppSettings.Default.Save();
            }
            else
            {
                AppSettings.Default.IsHotKeyEnabled = false;
                AppSettings.Default.Save();
            }
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            RegistryThemeHelper.ResetTheme();
            //SetTrayIcon();
        }

        //Handle system theme settings page link navigation
        private void ThemeSettings_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(new ProcessStartInfo("ms-settings:colors") {UseShellExecute = true});
        }

        private void DebugCheckbox_Checked(object sender, RoutedEventArgs e)
        {
            AppSettings.Default.IsDebugEnabled = true;
            AppSettings.Default.Save();
        }

        private void DebugCheckbox_Unchecked(object sender, RoutedEventArgs e)
        {
            AppSettings.Default.IsDebugEnabled = false;
            AppSettings.Default.Save();
        }
    }
}