using darker.Helpers;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace darker.Views
{
    /// <summary>
    /// Interaction logic for SettingsAdvanced.xaml
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
    }
}