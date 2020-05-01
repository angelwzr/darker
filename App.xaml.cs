using System;
using System.Threading;
using System.Windows;

namespace darker
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        // give the mutex a  unique name
        private const string MutexName = "##||darker||##";
        // declare the mutex
        private readonly Mutex _mutex;
        // overload the constructor
        bool createdNew;
        public App()
        {
            //check Windows version before anything else
            CheckWin();
            // overloaded mutex constructor which outs a boolean
            // telling if the mutex is new or not.
            // see http://msdn.microsoft.com/en-us/library/System.Threading.Mutex.aspx
            _mutex = new Mutex(true, MutexName, out createdNew);
            if (!createdNew)
            {
                // if the mutex already exists, notify and quit
                MessageBox.Show("This program is already running", "darker");
                Application.Current.Shutdown(0);
            }
        }
        protected override void OnStartup(StartupEventArgs e)
        {
            if (!createdNew) return;
            // overload the OnStartup so that the main window 
            // is constructed and visible
            MainWindow mw = new MainWindow();
            mw.Hide();
        }

        //actual Windows version check code
        private void CheckWin()
        {
            if (Environment.OSVersion.Version.Major < 10 && Environment.OSVersion.Version.Minor > 0)
            {
                MessageBox.Show("This app is designed for Windows 10 only. Please consider upgrading your OS.", "Unsupported OS");
                Application.Current.Shutdown();
            }
        }

    }
}
