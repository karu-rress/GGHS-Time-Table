#nullable enable

using Windows.UI.Text;
using Windows.UI.Xaml.Controls.Primitives;

namespace TimeTableUWP.Todo;
public class TaskButton : GttButton<TodoTask>
{
    const int bodyLength = 35;

    public TaskButton(TodoTask task, RoutedEventHandler task_click)
        : base(task, task_click)
    {
        // RightTapped += TaskButton_RightTapped;

        if (Data.DueDate.Date == DateTime.Now.Date)
        {
            BorderThickness = new(2.6);
            BorderBrush = new SolidColorBrush(Info.Settings.ColorType with { A = 200 });
        }

        switch (Data.Title)
        {
            case string ku when ku.Contains("고려대학교"):
                BorderBrush = new SolidColorBrush(Color.FromArgb(255, 139, 0, 41));
                goto default;

            case string ihs when ihs.Contains("서강대학교"):
                BorderBrush = new SolidColorBrush(Color.FromArgb(255, 182, 0, 5));
                goto default;

            case string cau when cau.Contains("중앙대학교"):
                BorderBrush = new SolidColorBrush(Color.FromArgb(255, 30, 113, 192));
                goto default;

            case string khu when khu.Contains("경희대학교"):
                BorderBrush = new SolidColorBrush(Color.FromArgb(255, 153, 14, 23));
                goto default;

            case string hufs when hufs.Contains("한국외국어대학교"):
                BorderBrush = new SolidColorBrush(Color.FromArgb(255, 0, 130, 148));
                goto default;

            case string kaist when kaist.Contains("KAIST"):
                BorderBrush = new SolidColorBrush(Color.FromArgb(255, 1, 67, 143));
                goto default;

            case string gist when gist.Contains("UNIST"):
                BorderBrush = new SolidColorBrush(Color.FromArgb(255, 0, 27, 84));
                goto default;

            case string dgist when dgist.Contains("DGIST"):
                BorderBrush = new SolidColorBrush(Color.FromArgb(255, 11, 191, 242));
                goto default;

            case string gist when gist.Contains("GIST"):
                BorderBrush = new SolidColorBrush(Color.FromArgb(255, 223, 49, 39));
                goto default;

            default:
                BorderThickness = new(1.9);
                break;
        }
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
                AddPage.Task = Data;
                if (Window.Current.Content is Frame rootFrame)
                    rootFrame.Navigate(typeof(AddPage), null, new Windows.UI.Xaml.Media.Animation.DrillInNavigationTransitionInfo());
            };
            delete.Click += async (_, e) =>
            {
                if (await TaskList.DeleteTask(Data.Title, Data) is false)
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


    protected override void CreateGrid(out Grid inner, out Grid dday, out Grid outter)
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
            Width = 65,
            Margin = new(10, 0, 0, 0),
            HorizontalAlignment = HorizontalAlignment.Left
        };
        outter = new();
    }

    protected override void CreateLeftTextBlocks(out TextBlock date, out TextBlock dday)
    {
        date = new()
        {
            FontSize = 19,
            Text = Data.DueDate.ToString("MM/dd"),
            Margin = new(0, 10, 0, 46),
            HorizontalAlignment = HorizontalAlignment.Center,
            FontFamily = new("ms-appx:///Assets/ZegoeLight-U.ttf#Segoe"),
            FontWeight = FontWeights.Bold
        };

        DateTime now = DateTime.Now;
        int days = (new DateTime(now.Year, now.Month, now.Day) - Data.DueDate).Days;
        string text = "D" + days switch
        {
            0 => "-Day",
            _ => days.ToString("+0;-0"),
        };

        dday = new()
        {
            FontSize = 15,
            Text = text,
            Margin = new(0, 44, 0, 12),
            HorizontalAlignment = HorizontalAlignment.Center,
            FontFamily = new("Consolas"),
            FontWeight = FontWeights.Bold
        };
    }

    protected override void CreateRightTextBlocks(out TextBlock subject, out TextBlock title)
    {
        subject = new()
        {
            FontSize = 17,
            Text = Data.Title,
            Margin = new(80, 12, 0, 44),
            Width = ButtonWidth
        };
        title = new()
        {
            FontSize = 15,
            Text = string.IsNullOrEmpty(Data.Body) ? "" : (Data.Body!.Length > bodyLength ? Data.Body[0..(bodyLength + 1)] + "..." : Data.Body),
            Margin = new(80, 43, 0, 13),
            HorizontalAlignment = HorizontalAlignment.Left,
            Width = ButtonWidth,
            Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0xA0, 0xA0, 0xA0))
        };
    }
}