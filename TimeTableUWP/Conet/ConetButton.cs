#nullable enable

using Windows.UI.Text;

namespace TimeTableUWP.Conet;

public class ConetButton : GttButton<ConetHelp>
{
    private readonly int bodyLength = 40;

    public ConetButton(ConetHelp task, RoutedEventHandler conet_click)
        : base(task, conet_click) {  }

    protected override void CreateGrid(out Grid inner, out Grid egg, out Grid outter)
    {
        inner = new()
        {
            Height = 80,
            Width = 2560,
            Margin = new(-12, 0, 0, 0),
            HorizontalAlignment = HorizontalAlignment.Left
        };
        egg = new()
        {
            Width = 75,
            Margin = new(10, 0, 0, 0),
            HorizontalAlignment = HorizontalAlignment.Left
        };
        outter = new();
    }

    protected override void CreateLeftTextBlocks(out TextBlock uploader, out TextBlock price)
    {
        uploader = new()
        {
            FontSize = 16,
            Text = Data.Uploader.Name,
            Margin = new(0, 12, 0, 46),
            HorizontalAlignment = HorizontalAlignment.Center,
            FontFamily = new("Malgun Gothic"),
            FontWeight = FontWeights.Bold
        };

        price = new()
        {
            FontSize = 16,
            Text = Data.Price?.ToString() ?? "",
            Margin = new(0, 41, 0, 12),
            HorizontalAlignment = HorizontalAlignment.Center,
            FontFamily = new("Malgun Gothic"),
            FontWeight = FontWeights.Bold
        };
    }

    protected override void CreateRightTextBlocks(out TextBlock title, out TextBlock body)
    {
        title = new()
        {
            FontSize = 17,
            Text = Data.Title,
            Margin = new(90, 12, 0, 44),
            Width = ButtonWidth,
        };
        body = new()
        {
            FontSize = 15,
            Text = string.IsNullOrEmpty(Data.Body) ? string.Empty
                : (Data.Body!.Length > bodyLength ? Data.Body[0..(bodyLength + 1)] + "..." : Data.Body),
            Margin = new(90, 43, 0, 13),
            HorizontalAlignment = HorizontalAlignment.Left,
            Width = ButtonWidth,
            Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0xA0, 0xA0, 0xA0))
        };
    }
}