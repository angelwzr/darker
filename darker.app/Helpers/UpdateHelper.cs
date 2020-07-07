using Onova;
using Onova.Services;
using System;

namespace darker.Helpers
{
    public static class UpdateHelper
    {
        private const string REPO_OWNER = "angelwzr";
        private const string REPO_NAME = "darker";
        private const string VERSION_PATTERN = "darker-*.*.*.zip";

        public static async void CheckForUpdates()
        {
            using var updateManager = new UpdateManager(new GithubPackageResolver(REPO_OWNER, REPO_NAME, VERSION_PATTERN), new ZipPackageExtractor());


            try
            {
                await updateManager.CheckPerformUpdateAsync();
            }
            catch (Exception updateEx)
            {
                //MessageBox.Show("Error updating the app: " + updateEx);
            }
            finally
            {
                updateManager.Dispose();
            }
        }
    }
}