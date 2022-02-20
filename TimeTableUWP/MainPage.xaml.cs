#nullable enable

using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using Windows.ApplicationModel;
using TimeTableUWP.Pages;
using RollingRess;
using muxc = Microsoft.UI.Xaml.Controls;

namespace TimeTableUWP
{

    public sealed partial class MainPage : Page
    {
        public static bool IsGoingToTodoPage { get; set; } = false;

        public MainPage()
        {
            InitializeComponent();

            // NavigationFrame = Frame;

            if (IsGoingToTodoPage)
            {
                IsGoingToTodoPage = false;
                ContentFrame.Navigate(typeof(TodoListPage), null, new DrillInNavigationTransitionInfo());
                Navigation.SelectedItem = Navigation.MenuItems[1];
            }
            else
            {
                ContentFrame.Navigate(typeof(TimeTablePage), null, new DrillInNavigationTransitionInfo());
                Navigation.SelectedItem = Navigation.MenuItems[0];
            }
        }

        private void NavigateTo(object tag, NavigationTransitionInfo transition)
        {
            Type page = tag switch
            {
                "settings" => typeof(SettingsPage),
                "timetable" => typeof(TimeTablePage),
                "todo" => typeof(TodoListPage),
                "chat" => typeof(ChattingPage),
                _ => throw new ArgumentException($@"MainPage.NavigateTo(): Unknown tag ""{tag}"".")
            };
            ContentFrame.Navigate(page, null, transition);
        }

        private void Navigation_ItemInvoked(muxc.NavigationView sender, muxc.NavigationViewItemInvokedEventArgs args)
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
    }
}
