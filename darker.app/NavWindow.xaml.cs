using ModernWpf.Controls;
using System;
using System.Windows;
using System.Windows.Controls;

namespace darker
{
    /// <summary>
    /// Interaction logic for NavWindow.xaml
    /// </summary>
    public partial class NavWindow : Window
    {
        public NavWindow()
        {
            InitializeComponent();
        }


        private void NavView_Loaded(object sender, RoutedEventArgs e)
        {
            // set the initial SelectedItem
            foreach (NavigationViewItemBase item in NavigationViewControl.MenuItems)
                if (item is NavigationViewItem && item.Tag.ToString() == "SettingsGeneral")
                {
                    NavigationViewControl.SelectedItem = item;
                    break;
                }

            ContentFrame.Navigate(new Uri("Views/SettingsGeneral.xaml", UriKind.RelativeOrAbsolute));
        }

        private void Nav_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
        }

        private void Nav_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            var ItemContent = args.InvokedItem as TextBlock;
            if (ItemContent != null)
                switch (ItemContent.Tag)
                {
                    case "NavHome":
                        ContentFrame.Navigate(new Uri("Views/SettingsGeneral.xaml", UriKind.RelativeOrAbsolute));
                        break;

                    case "NavScheduler":
                        ContentFrame.Navigate(new Uri("Views/SettingsScheduler.xaml", UriKind.RelativeOrAbsolute));
                        break;

                    case "NavAdvanced":
                        ContentFrame.Navigate(new Uri("Views/SettingsAdvanced.xaml", UriKind.RelativeOrAbsolute));
                        break;

                    case "NavAbout":
                        ContentFrame.Navigate(new Uri("Views/SettingsAbout.xaml", UriKind.RelativeOrAbsolute));
                        break;
                }
        }

        public static implicit operator NavWindow(SettingsWindow v)
        {
            throw new NotImplementedException();
        }
    }
}