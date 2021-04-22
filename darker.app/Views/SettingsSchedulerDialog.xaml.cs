using FluentScheduler;
using ModernWpf.Controls;
using System;
using System.Diagnostics;
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
            TimePickerMorning.Culture = currentCulture;
            TimePickerEvening.Culture = currentCulture;
            TestControl.Content = currentCulture;
            TimePickerEvening.SelectedDateTime = new DateTime(03);
            //TimePickerMorning.SelectedDateTime = DateTime.Parse(AppSettings.Default.ThemeChangingMorningHour, AppSettings.Default.ThemeChangingMorningMin);
            //TimePickerEvening.SelectedDateTime = DateTime.Parse(AppSettings.Default.ThemeChangingEveningHour, AppSettings.Default.ThemeChangingEveningMin);

        }

        private void TimePickerMorning_SelectedDateTimeChanged(object sender, RoutedPropertyChangedEventArgs<DateTime?> e)
        {
            JobManager.StopAndBlock();
            AppSettings.Default.ThemeChangingMorningHour = e.NewValue.Value.Hour;
            AppSettings.Default.ThemeChangingMorningMin = e.NewValue.Value.Minute;
            AppSettings.Default.Save();
            TestControl.Content = e.NewValue?.ToShortTimeString() ?? "null";
            JobManager.Initialize();
            JobManager.AddJob(() => Debug.WriteLine("Morning changing event fired"), (s) => s.ToRunEvery(1).Days().At(AppSettings.Default.ThemeChangingMorningHour, AppSettings.Default.ThemeChangingMorningMin));
        }

        private void TimePickerEvening_SelectedDateTimeChanged(object sender, RoutedPropertyChangedEventArgs<DateTime?> e)
        {
            JobManager.StopAndBlock();
            AppSettings.Default.ThemeChangingEveningHour = e.NewValue.Value.Hour;
            AppSettings.Default.ThemeChangingEveningMin = e.NewValue.Value.Minute;
            AppSettings.Default.Save();
            TestControl.Content = e.NewValue?.ToShortTimeString() ?? "null";
            JobManager.Initialize();
            JobManager.AddJob(() => Debug.WriteLine("Evening changing event fired"), (s) => s.ToRunEvery(1).Days().At(AppSettings.Default.ThemeChangingEveningHour, AppSettings.Default.ThemeChangingEveningMin));
        }
    }
}