using System;
using System.Resources;
using System.Threading;
using System.Windows;
using darker.Properties;

namespace darker
{
    /// <summary>
    ///     Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        //Mutex
        private const string MutexName = "##||darker||##";
        private readonly bool _createdNew;
        private readonly Mutex _mutex;

        public App()
        {
            //Check Windows version before anything else
            CheckWin();
            //Allow single instance of the app only
            _mutex = new Mutex(true, MutexName, out _createdNew);
            if (_createdNew) return;
            var resourceManager = new ResourceManager(typeof(Resources));
            MessageBox.Show(resourceManager.GetString("Error_RunningAppMessage"), resourceManager.GetString("App_AppName"));
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
            var resourceManager = new ResourceManager(typeof(Resources));
            MessageBox.Show(resourceManager.GetString("Error_OSVersionMessage"), resourceManager.GetString("App_AppName"));
            Current.Shutdown();
        }
    }
}