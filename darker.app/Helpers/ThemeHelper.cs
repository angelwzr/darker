using darker.Models;

namespace darker.Helpers
{
    public static class ThemeHelper
    {
        /// <summary>
        /// Switches current Windows theme
        /// </summary>
        public static void SwitchWindowsTheme()
        {
            var theme = RegistryThemeHelper.GetWindowsTheme();
            var newTheme = theme == UITheme.Light ? UITheme.Dark : UITheme.Light;

            RegistryThemeHelper.SetWindowsTheme(newTheme);
        }

        /// <summary>
        /// Switches current apps theme
        /// </summary>
        public static void SwitchAppsTheme()
        {
            var theme = RegistryThemeHelper.GetAppsTheme();
            var newTheme = theme == UITheme.Light ? UITheme.Dark : UITheme.Light;

            RegistryThemeHelper.SetAppsTheme(newTheme);
        }
    }
}