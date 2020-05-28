using System;
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
        //Mutex
        private const string MutexName = "##||darker||##";
        private readonly Mutex _mutex;
        private readonly bool _createdNew;

        public App()
        {
            //Check Windows version before anything else
            CheckWin();
            //Allow single instance of the app only
            _mutex = new Mutex(true, MutexName, out _createdNew);
            if (_createdNew) return;
            var resourceManager = new ResourceManager(typeof(Properties.Resources));
            MessageBox.Show(resourceManager.GetString("RunningAppMessage"), resourceManager.GetString("AppName"));
            Current.Shutdown(0);
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            if (!_createdNew) return;
            var mw = new MainWindow();
            mw.Hide();
        }

        //Actual Windows version check code
        private void CheckWin()
        {
            if (Environment.OSVersion.Version.Major >= 10 || Environment.OSVersion.Version.Minor <= 0) return;
            var resourceManager = new ResourceManager(typeof(Properties.Resources));
            MessageBox.Show(resourceManager.GetString("OSVersionMessage"), resourceManager.GetString("AppName"));
            Current.Shutdown();
        }
    }
}