#nullable enable

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.ApplicationModel;
using GGHS;
using GGHS.Grade2.Semester2;
using static RollingRess.StaticClass;
using System.Threading;
using Windows.UI;
using Windows.UI.Xaml.Media.Animation;
using TimeTableUWP.Pages;
using muxc = Microsoft.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace TimeTableUWP
{

    public sealed partial class MainPage : Page
    {
        private static PackageVersion version => Package.Current.Id.Version;

        /// <summary>
        /// GGHS Time Table's version: string value with the format "X.X.X"
        /// </summary>
        public static string Version => $"{version.Major}.{version.Minor}.{version.Build}";

        public static ElementTheme Theme => SettingsPage.IsDarkMode ? ElementTheme.Dark : ElementTheme.Light;

        public MainPage()
        {
            InitializeComponent();

            // NavigationFrame = Frame;

            ContentFrame.Navigate(typeof(TimeTablePage), null, new DrillInNavigationTransitionInfo());
            Navigation.SelectedItem = Navigation.MenuItems[0];
        }

        public static void ChangeTheme()
        {
            
        }

        private void NavigateTo(object tag, NavigationTransitionInfo transition)
        {
            Type page = tag switch
            {
                "settings" => typeof(SettingsPage),
                "timetable" => typeof(TimeTablePage),
                "todo" => typeof(TodoPage),
                _ => throw new ArgumentException($@"MainPage.NavigateTo(): Unknown tag ""{tag}"".")
            };
            ContentFrame.Navigate(page, null, transition);
        }

        private void Navigation_ItemInvoked(muxc.NavigationView sender, Microsoft.UI.Xaml.Controls.NavigationViewItemInvokedEventArgs args)
        {
            if (args.IsSettingsInvoked is true)
            {
                NavigateTo("settings", args.RecommendedNavigationTransitionInfo);
            }
            else if (Navigation.SelectedItem is muxc.NavigationViewItem ItemContent)
            {
                NavigateTo(ItemContent.Tag, args.RecommendedNavigationTransitionInfo);
            }
        }

        private void ContentFrame_Navigated(object sender, Windows.UI.Xaml.Navigation.NavigationEventArgs e)
        {

        }
    }
}
