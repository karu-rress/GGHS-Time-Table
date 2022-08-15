#nullable enable

namespace TimeTableUWP;

public interface IButtonData { }

public abstract class GttButton<T> : Button
    where T: IButtonData
{
    protected const int ButtonWidth = 2560;
    protected virtual int ButtonHeight { get; } = 93;

    public T Data { get; protected set; }
    
    protected GttButton(T data, RoutedEventHandler clicked)
    {
        Data = data;
        Height = ButtonHeight;
        Click += clicked;

        Margin = new(0, 0, 0, 5);
        CornerRadius = new(10);
        VerticalAlignment = VerticalAlignment.Top;

        CreateGrid(out Grid inner, out Grid lbgrid, out Grid outter);
        CreateLeftTextBlocks(out TextBlock left_top, out TextBlock left_bottom);
        lbgrid.Children.Add(left_top);
        lbgrid.Children.Add(left_bottom);
        inner.Children.Add(lbgrid);

        CreateRightTextBlocks(out TextBlock right_top, out TextBlock right_bottom);
        inner.Children.Add(right_top);
        inner.Children.Add(right_bottom);

        CreateArrowTextBlock(out TextBlock arrow);
        outter.Children.Add(inner);
        outter.Children.Add(arrow);

        Content = outter;
    }

    protected abstract void CreateGrid(out Grid inner, out Grid dday, out Grid outter);
    protected abstract void CreateLeftTextBlocks(out TextBlock top, out TextBlock bottom);
    protected abstract void CreateRightTextBlocks(out TextBlock right_top, out TextBlock right_bottom);

    protected void CreateArrowTextBlock(out TextBlock arrow)
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
}