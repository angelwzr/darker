using darker.Properties;
using System.Diagnostics;
using System.Reflection;
using System.Resources;
using System.Windows.Controls;

namespace darker.Views
{
    /// <summary>
    /// Interaction logic for SettingsAbout.xaml
    /// </summary>
    public partial class SettingsAbout : Page
    {
        public SettingsAbout()
        {
            InitializeComponent();
            //Handle version text
            var assembly = Assembly.GetExecutingAssembly();
            var assemblyVersion = assembly.GetName().Version;
            var resourceManager = new ResourceManager(typeof(Resources));
            VersionText.Text = resourceManager.GetString("App_AppName") + $" {assemblyVersion}";
        }

        //Handle GH link navigation
        private void Link_RequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri) {UseShellExecute = true});
            e.Handled = true;
        }
    }
}