using Microsoft.Win32;
using System.Diagnostics;
using System.Reflection;
using System.Windows;

namespace darker
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : Window
    {
        public Settings()
        {
            InitializeComponent();
            //check for autostart with Windows key
            CheckForAutostart();
            //handle version text on about tab
            var assembly = Assembly.GetExecutingAssembly();
            var assemblyVersion = assembly.GetName().Version;
            VersionText.Text = $"darker {assemblyVersion}";
            //ThemeMode radiobuttons check
            if (App.AppSettings.Default.ThemeMode.Equals("1"))
                Both.IsChecked = checked(true);
            else if (App.AppSettings.Default.ThemeMode.Equals("2"))
                SysOnly.IsChecked = checked(true);
            else if (App.AppSettings.Default.ThemeMode.Equals("3")) AppsOnly.IsChecked = checked(true);
        }

        //start with Windows checkbox
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

        //launch on startup button
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

        //handle GH link navigation
        private void Link_RequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri) {UseShellExecute = true});
            e.Handled = true;
        }

        private void Both_OnChecked(object sender, RoutedEventArgs e)
        {
            App.AppSettings.Default.ThemeMode = "1";
            App.AppSettings.Default.Save();
        }

        private void SysOnly_OnChecked(object sender, RoutedEventArgs e)
        {
            App.AppSettings.Default.ThemeMode = "2";
            App.AppSettings.Default.Save();
        }

        private void AppsOnly_OnChecked(object sender, RoutedEventArgs e)
        {
            App.AppSettings.Default.ThemeMode = "3";
            App.AppSettings.Default.Save();
        }
    }
}