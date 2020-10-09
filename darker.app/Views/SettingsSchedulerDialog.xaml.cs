using ModernWpf.Controls;
using System;
using System.Windows;

namespace darker.Viewes
{
    public partial class SettingsSchedulerDialog : ContentDialog
    {
        public SettingsSchedulerDialog()
        {
            InitializeComponent();

            //TimePickerDark.Culture = CultureInfo.CurrentUICulture.Name.ToString;
            //TimePickerLight.Culture = CultureInfo.CurrentUICulture.Name.ToString;
        }

        private void TimePickerLight_SelectedDateTimeChanged(object sender, RoutedPropertyChangedEventArgs<DateTime?> e)
        {
            TestControl.Content = e.NewValue?.ToShortTimeString() ?? "null";
        }

        private void TimePickerDark_SelectedDateTimeChanged(object sender, RoutedPropertyChangedEventArgs<DateTime?> e)
        {
            TestControl.Content = e.NewValue?.ToShortTimeString() ?? "null";
        }
    }
}