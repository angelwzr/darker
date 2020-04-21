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
        }

        private TaskbarIcon tb;

        private void InitApplication()
        {
            //initialize NotifyIcon
            tb = (TaskbarIcon)FindResource("MyNotifyIcon");
        }


        private void DarkCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void DarkCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            using RegistryKey key = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize");
            {
                key.SetValue("SystemUsesLightTheme", $"0", RegistryValueKind.DWord);
                key.SetValue("AppsUseLightTheme", $"0", RegistryValueKind.DWord);
                key.Close();
            }
        }

        private void LightCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void LightCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            using RegistryKey key = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize");
            {
                key.SetValue("SystemUsesLightTheme", $"1", RegistryValueKind.DWord);
                key.SetValue("AppsUseLightTheme", $"1", RegistryValueKind.DWord);
                key.Close();
            }
        }


        void ExitMenuItem_Click(object sender, RoutedEventArgs e)
        {
            // Close this window
            this.Close();
        }

    }
}
