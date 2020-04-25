using Hardcodet.Wpf.TaskbarNotification;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
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
            ChecForAutostart();
        }

        private TaskbarIcon tb;

        private void InitApplication()
        {
            //initialize NotifyIcon
            tb = (TaskbarIcon)FindResource("MyNotifyIcon");
        }

        //windows startup
        private void ChecForAutostart()
        {
            RegistryKey reg = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run");
            if (reg != null)
            {
                string sVal = reg.GetValue("darker", "").ToString();
                AutoS.IsChecked = sVal == System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;
                reg.Close();
            }
        }

        //theme detection
        private const string RegistryKeyPathTheme = @"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize";
        private const string RegSysMode = "SystemUsesLightTheme";
        private const string RegAppMode = "AppsUseLightTheme";

        private enum WindowsTheme
        {
            Light,
            Dark
        }

        private static WindowsTheme GetWindowsTheme()
        {
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(RegistryKeyPathTheme))
            {
                object registryValueObject = key?.GetValue(RegSysMode);
                if (registryValueObject == null)
                {
                    return WindowsTheme.Light;
                }

                int registryValue = (int)registryValueObject;

                return registryValue > 0 ? WindowsTheme.Light : WindowsTheme.Dark;
            }
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

        //theme switching buttons
        private void DarkCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {

            using RegistryKey key = Registry.CurrentUser.CreateSubKey(RegistryKeyPathTheme);
            {
                key.SetValue(RegSysMode, $"0", RegistryValueKind.DWord);
                key.SetValue(RegAppMode, $"0", RegistryValueKind.DWord);
                key.Close();
            }
            
            MyNotifyIcon.IconSource = new BitmapImage(new Uri(@"pack://application:,,,/Resources/day_w.ico"));
        }

        private void LightCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            using RegistryKey key = Registry.CurrentUser.CreateSubKey(RegistryKeyPathTheme);
            {
                key.SetValue(RegSysMode, $"1", RegistryValueKind.DWord);
                key.SetValue(RegAppMode, $"1", RegistryValueKind.DWord);
                key.Close();
            }
            
            MyNotifyIcon.IconSource = new BitmapImage(new Uri(@"pack://application:,,,/Resources/night_b.ico"));
        }

        //exti button
        void ExitMenuItem_Click(object sender, RoutedEventArgs e)
        {
            // Close this window
            this.Close();
        }

        //about button
        private void AboutItem_Click(object sender, RoutedEventArgs e)
        {
            string title = "WPF NotifyIcon";
            string text = "This is a standard balloon";

            //show balloon with built-in icon
            MyNotifyIcon.ShowBalloonTip(title, text, BalloonIcon.Info);
        }
    }

    //commancds for theme switching launch
    public static class darkerCommands
    {
        public static readonly RoutedUICommand Dark = new RoutedUICommand
            (
                "Dark",
                "Dark",
                typeof(darkerCommands)
            );

        public static readonly RoutedUICommand Light = new RoutedUICommand
            (
                "Light",
                "Light",
                typeof(darkerCommands)
            );
    }

}
