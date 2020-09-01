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