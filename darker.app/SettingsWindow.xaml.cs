using darker.Models;
using Microsoft.Win32;
using System.Diagnostics;
using System.Reflection;
using System.Windows;

namespace darker
{
    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        public SettingsWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //Check for autostart with Windows key
            CheckForAutostart();
            //Handle version text on about tab
            var assembly = Assembly.GetExecutingAssembly();
            var assemblyVersion = assembly.GetName().Version;
            VersionText.Text = $"darker {assemblyVersion}";
            //ThemeMode radiobuttons check
            switch (AppSettings.Default.ThemeMode)
            {
                case SettingsThemeMode.Both:
                    Both.IsChecked = true;
                    break;

                case SettingsThemeMode.OnlySystem:
                    SysOnly.IsChecked = true;
                    break;

                case SettingsThemeMode.OnlyApps:
                    AppsOnly.IsChecked = true;
                    break;
            }

            //AutoU checkbox check
            switch (AppSettings.Default.IsAutoUpdateEnabled)
            {
                case true:
                    AutoU.IsChecked = true;
                    break;

                case false:
                    AutoU.IsChecked = false;
                    break;
            }
        }

        //Start with Windows checkbox
        private void CheckForAutostart()
        {
            using var reg = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run");
            if (reg != null)
            {
                var sVal = reg.GetValue("darker", "").ToString();
                AutoS.IsChecked = sVal == Process.GetCurrentProcess().MainModule.FileName;
                reg.Close();
            }
        }

        //Launch on startup button
        private void AutoS_Checked(object sender, RoutedEventArgs e)
        {
            using var key =
                Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);
            key.SetValue("darker", Process.GetCurrentProcess().MainModule.FileName);
        }

        private void AutoS_Unchecked(object sender, RoutedEventArgs e)
        {
            using var key =
                Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);
            key.DeleteValue("darker", false);
        }

        //Handle GH link navigation
        private void Link_RequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri) {UseShellExecute = true});
            e.Handled = true;
        }

        //Buttons settings file interactions
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
    }
}