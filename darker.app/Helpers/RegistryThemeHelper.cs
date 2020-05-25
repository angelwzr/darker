using darker.Models;
using Microsoft.Win32;

namespace darker.Helpers
{
    public static class RegistryThemeHelper
    {
        private const string RegistryKeyPathTheme = @"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize";
        private const string RegSysMode = "SystemUsesLightTheme";
        private const string RegAppMode = "AppsUseLightTheme";
        private const string RegColPMode = "ColorPrevalence";

        /// <summary>
        /// Returns theme used for Windows
        /// </summary>
        public static UITheme GetWindowsTheme()
        {
            return GetThemeFromRegistry(RegSysMode);
        }

        /// <summary>
        /// Sets Windows theme
        /// </summary>
        public static void SetWindowsTheme(UITheme theme)
        {
            using var key = Registry.CurrentUser.CreateSubKey(RegistryKeyPathTheme);

            switch (theme)
            {
                case UITheme.Light:
                    key.SetValue(RegSysMode, "1", RegistryValueKind.DWord);
                    key.SetValue(RegColPMode, "0", RegistryValueKind.DWord);
                    break;

                case UITheme.Dark:
                    key.SetValue(RegSysMode, "0", RegistryValueKind.DWord);
                    break;
            }

            key.Close();
        }

        /// <summary>
        /// Returns theme used for apps
        /// </summary>
        public static UITheme GetAppsTheme()
        {
            return GetThemeFromRegistry(RegAppMode);
        }

        /// <summary>
        /// Sets Apps theme
        /// </summary>
        public static void SetAppsTheme(UITheme theme)
        {
            using var key = Registry.CurrentUser.CreateSubKey(RegistryKeyPathTheme);

            switch (theme)
            {
                case UITheme.Light:
                    key.SetValue(RegAppMode, "1", RegistryValueKind.DWord);
                    break;

                case UITheme.Dark:
                    key.SetValue(RegAppMode, "0", RegistryValueKind.DWord);
                    break;
            }

            key.Close();
        }

        /// <summary>
        /// Resets theme settings to default values
        /// </summary>
        public static void ResetTheme()
        {
            using var key = Registry.CurrentUser.CreateSubKey(RegistryKeyPathTheme);

            key.SetValue(RegSysMode, "1", RegistryValueKind.DWord);
            key.SetValue(RegAppMode, "1", RegistryValueKind.DWord);
            key.SetValue(RegColPMode, "0", RegistryValueKind.DWord);
            key.Close();
        }

        // Reads value by given key in registry and converts to UITheme
        private static UITheme GetThemeFromRegistry(string registryKey)
        {
            using var key = Registry.CurrentUser.OpenSubKey(RegistryKeyPathTheme);
            var themeValue = key.GetValue(registryKey) as int?;

            return themeValue != 0 ? UITheme.Light : UITheme.Dark;
        }
    }
}
