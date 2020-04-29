using Hardcodet.Wpf.TaskbarNotification;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace darker
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            CheckWin();
            CheckForAutostart();
        }

        //check Windows version
        private void CheckWin()
        {
            if (Environment.OSVersion.Version.Major < 10 && Environment.OSVersion.Version.Minor > 0)
            {
                MessageBox.Show("This app is designed for Windows 10 only. Please consider upgrading your OS.", "Unsupported OS");
                Application.Current.Shutdown();
            }
        }

        //windows startup
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

        //theme reg keys
        private const string RegistryKeyPathTheme = @"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize";
        private const string RegSysMode = "SystemUsesLightTheme";
        private const string RegAppMode = "AppsUseLightTheme";
        private const string RegColPMode = "ColorPrevalence";

        //theme switching buttons
        private void DarkCommand(object sender, RoutedEventArgs e)
        {

            using RegistryKey key = Registry.CurrentUser.CreateSubKey(RegistryKeyPathTheme);
            {
                key.SetValue(RegSysMode, $"0", RegistryValueKind.DWord);
                key.SetValue(RegAppMode, $"0", RegistryValueKind.DWord);
                key.Close();
            }
            MyNotifyIcon.IconSource = new BitmapImage(new Uri(@"pack://application:,,,/Resources/day_w.ico"));
        }

        private void LightCommand(object sender, RoutedEventArgs e)
        {
            using RegistryKey key = Registry.CurrentUser.CreateSubKey(RegistryKeyPathTheme);
            {
                key.SetValue(RegSysMode, $"1", RegistryValueKind.DWord);
                key.SetValue(RegAppMode, $"1", RegistryValueKind.DWord);
                key.SetValue(RegColPMode, $"0", RegistryValueKind.DWord);
                key.Close();
            }
            MyNotifyIcon.IconSource = new BitmapImage(new Uri(@"pack://application:,,,/Resources/night_b.ico"));
        }

        //launch on startup button
        private void AutoS_Checked(object sender, RoutedEventArgs e)
        {
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true))
            {
                key.SetValue("darker", System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);
            }
        }

        private void AutoS_Unchecked(object sender, RoutedEventArgs e)
        {
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true))
            {
                key.DeleteValue("darker", false);
            }
        }

        //settings button
        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(new ProcessStartInfo("ms-settings:colors") { UseShellExecute = true });
        }

        //about button
        private void About_Click(object sender, RoutedEventArgs e)
        {
            About subWindow = new About();
            subWindow.Show();
        }

        //exit button
        void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

    }

}
