#nullable enable

using Windows.UI.Xaml.Media.Animation;
using Windows.UI.ViewManagement;
using Windows.ApplicationModel.Core;

namespace TimeTableUWP;

public sealed partial class MainPage : Page
{
    public static bool IsGoingToTodoPage { get; set; } = false;

    public MainPage()
    {
        InitializeComponent();
        
        // Set title bar
        CoreApplication.GetCurrentView().TitleBar.ExtendViewIntoTitleBar = true;
        ApplicationViewTitleBar titleBar = ApplicationView.GetForCurrentView().TitleBar;
        titleBar.ButtonBackgroundColor = Colors.Transparent;
        titleBar.ButtonInactiveBackgroundColor = Colors.Transparent;
        AppTitle.Text = $"GGHS Time Table {Info.Version}";
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

    private void Navigation_ItemInvoked(muxc::NavigationView sender, muxc::NavigationViewItemInvokedEventArgs args)
    {
        if (args.IsSettingsInvoked)
            NavigateTo("settings", args.RecommendedNavigationTransitionInfo);
        
        else if (Navigation.SelectedItem is muxc::NavigationViewItem ItemContent)
            NavigateTo(ItemContent.Tag, args.RecommendedNavigationTransitionInfo);
    }

    private void NavigateTo(object tag, NavigationTransitionInfo transition)
    {
        NavigationColor.Color = Info.Settings.ColorType with { A = 255 };
        Type page = tag switch
        {
            "settings" => typeof(SettingsPage),
            "timetable" => typeof(TimeTablePage),
            "todo" => typeof(TodoListPage),
            "chat" => typeof(ChattingPage),
            "conet" => typeof(ConetPage),
            "mygod" => typeof(MyGodPage),
            _ => throw new ArgumentException($@"MainPage.NavigateTo(): Unknown tag ""{tag}"".")
        };
        ContentFrame.Navigate(page, null, transition);
    }
}
