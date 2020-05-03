using System;
using System.Reflection;
using System.Resources;
using System.Threading;
using System.Windows;

namespace darker
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        //mutex
        private const string MutexName = "##||darker||##";
        private readonly Mutex _mutex;
        bool createdNew;
        public App()
        {
            //check Windows version before anything else
            CheckWin();
            //allow single instance of the app only
            _mutex = new Mutex(true, MutexName, out createdNew);
            if (!createdNew)
            {
                var resourceManager = new ResourceManager(typeof(darker.Properties.Resources));
                MessageBox.Show(resourceManager.GetString("RunningAppMessage"), resourceManager.GetString("AppName"));
                Application.Current.Shutdown(0);
            }
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            if (!createdNew) return;
            MainWindow mw = new MainWindow();
            mw.Hide();
        }

        //actual Windows version check code
        private void CheckWin()
        {
            if (Environment.OSVersion.Version.Major < 10 && Environment.OSVersion.Version.Minor > 0)
            {
                var resourceManager = new ResourceManager(typeof(darker.Properties.Resources));
                MessageBox.Show(resourceManager.GetString("OSVersionMessage"), resourceManager.GetString("AppName"));
                Application.Current.Shutdown();
            }
        }
    }
}
