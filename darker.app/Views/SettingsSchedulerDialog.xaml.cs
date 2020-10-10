using ModernWpf.Controls;
using System;
using System.Globalization;
using System.Threading;
using System.Windows;

namespace darker.Viewes
{
    public partial class SettingsSchedulerDialog : ContentDialog
    {
        public SettingsSchedulerDialog()
        {
            InitializeComponent();

            CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;
            TimePickerDark.Culture = currentCulture;
            TimePickerLight.Culture = currentCulture;
            TestControl.Content = currentCulture;
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