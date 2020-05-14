using Microsoft.Win32;
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
        }

        //start with Windows checkbox
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
    }
}
