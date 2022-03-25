#nullable enable

using Windows.UI.Text;

namespace TimeTableUWP.Conet;
public class ConetButton : Button
{
    private const int ButtonWidth = 2560;
    private const int ButtonHeight = 93;
    private readonly int bodyLength = 40;

    public ConetHelp ConetHelp { get; private set; }

    public ConetButton(in ConetHelp task, RoutedEventHandler TaskButton_Click)
    {
        ConetHelp = task;
        Click += TaskButton_Click;
        Height = ButtonHeight;
        Margin = new(0, 0, 0, 5);
        CornerRadius = new(10);
        VerticalAlignment = VerticalAlignment.Top;

        CreateGrid(out Grid inner, out Grid dday, out Grid outter);
        CreateDdayTextBlock(out TextBlock tb1, out TextBlock tb2);
        dday.Children.Add(tb1);
        dday.Children.Add(tb2);
        inner.Children.Add(dday);

        CreateTaskTextBlock(out TextBlock tb3, out TextBlock tb4);
        inner.Children.Add(tb3);
        inner.Children.Add(tb4);

        CreateArrowTextBlock(out TextBlock arrow);
        outter.Children.Add(inner);
        outter.Children.Add(arrow);

        Content = outter;
    }

    /*
    private void TaskButton_RightTapped(object sender, RightTappedRoutedEventArgs e)
    {
        if (sender is UIElement uiElem)
        {
            MenuFlyout btnFlyOut = new();
            MenuFlyoutItem edit = new() { Text = "Edit", Icon = new SymbolIcon(Symbol.Edit) };
            MenuFlyoutItem delete = new() { Text = "Delete", Icon = new SymbolIcon(Symbol.Delete) };

            edit.Click += (_, e) => {
                AddPage.Task = ConetHelp;
                if (Window.Current.Content is Frame rootFrame)
                    rootFrame.Navigate(typeof(AddPage), null, new Windows.UI.Xaml.Media.Animation.DrillInNavigationTransitionInfo());
            };
            delete.Click += async (_, e) =>
            {
                if (await TaskList.DeleteTask(ConetHelp.Title, ConetHelp) is false)
                    return;
                await Task.Delay(100);
                if (Window.Current.Content is Frame rootFrame)
                {
                    // TODO: 이걸 그냥 MainPage의 Reload Task..?
                    // TODO: 이거 그냥 TodoPage로 하면 NavigationView 날아간다. 수정좀.
                    MainPage.IsGoingToTodoPage = true;
                    rootFrame.Navigate(typeof(MainPage), null, new Windows.UI.Xaml.Media.Animation.SuppressNavigationTransitionInfo());
                }
            };

            btnFlyOut.Items.Add(edit);
            btnFlyOut.Items.Add(delete);
            btnFlyOut.Placement = FlyoutPlacementMode.Bottom;
            btnFlyOut.ShowAt(uiElem, e.GetPosition(uiElem));
        }
    }
    */

    private void CreateArrowTextBlock(out TextBlock arrow)
    {
        arrow = new()
        {
            Text = "\xE971", // E9B9 
            FontFamily = new("ms-appx:///Assets/segoefluent.ttf#Segoe Fluent Icons"),
            FontSize = 17,
            Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0x72, 0x72, 0x72)),
            VerticalAlignment = VerticalAlignment.Center,
            HorizontalAlignment = HorizontalAlignment.Right,
            Margin = new(0, 0, 15, 0)
        };
    }

    private void CreateGrid(out Grid inner, out Grid dday, out Grid outter)
    {
        inner = new()
        {
            Height = 80,
            Width = 2560,
            Margin = new(-12, 0, 0, 0),
            HorizontalAlignment = HorizontalAlignment.Left
        };
        dday = new()
        {
            Width = 75,
            Margin = new(10, 0, 0, 0),
            HorizontalAlignment = HorizontalAlignment.Left
        };
        outter = new();
    }

    private void CreateDdayTextBlock(out TextBlock tb1, out TextBlock tb2)
    {
        tb1 = new()
        {
            FontSize = 16,
            Text = ConetHelp.Uploader.Name,
            Margin = new(0, 12, 0, 46),
            HorizontalAlignment = HorizontalAlignment.Center,
            FontFamily = new("Malgun Gothic"),
            FontWeight = FontWeights.Bold
        };

        tb2 = new()
        {
            FontSize = 16,
            Text = ConetHelp.Price?.ToString() ?? "",
            Margin = new(0, 41, 0, 12),
            HorizontalAlignment = HorizontalAlignment.Center,
            FontFamily = new("Malgun Gothic"),
            FontWeight = FontWeights.Bold
        };
    }

    private void CreateTaskTextBlock(out TextBlock tb3, out TextBlock tb4)
    {
        tb3 = new()
        {
            FontSize = 17,
            Text = ConetHelp.Title,
            Margin = new(90, 12, 0, 44),
            Width = ButtonWidth
        };
        tb4 = new()
        {
            FontSize = 15,
            Text = string.IsNullOrEmpty(ConetHelp.Body) ? "" : (ConetHelp.Body!.Length > bodyLength ? ConetHelp.Body[0..(bodyLength + 1)] + "..." : ConetHelp.Body),
            Margin = new(90, 43, 0, 13),
            HorizontalAlignment = HorizontalAlignment.Left,
            Width = ButtonWidth,
            Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0xA0, 0xA0, 0xA0))
        };
    }
}