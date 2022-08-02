#nullable enable

using Windows.UI.Text;

namespace TimeTableUWP.MyGod;

public class University : IButtonData
{
    public const string SNU = "서울대학교";
    public const string YU = "연세대학교";
    public const string KU = "고려대학교";
    public const string SKU = "서강대학교";
    public const string SKKU = "성균관대학교";
    public const string HU = "한양대학교";
    public const string CAU = "중앙대학교";
    public const string KHU = "경희대학교";
    public const string HUFS = "한국외국어대학교";
    public const string UOS = "서울시립대학교";
    public static IEnumerable<string> Lists { get; } = new List<string> { SNU, YU, KU, SKU, SKKU, HU, CAU, KHU, HUFS, UOS };

    public string Value { get; set; }
    public University(string value) { Value = value; }
}

public class MyGodButton : GttButton<University>
{
    protected override int ButtonHeight { get; } = 73;
    public MyGodButton(University univ, RoutedEventHandler mygod_click)
        : base(univ, mygod_click)
    {
    }

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
            Text = "TEST",
            Margin = new(0, 5, 0, 46),
            HorizontalAlignment = HorizontalAlignment.Center,
            // FontFamily = new("Malgun Gothic"),
            FontWeight = FontWeights.Bold
        };

        price = new()
        {
            FontSize = 16,
            Text = "",
            Margin = new(0, 55, 0, 12),
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
            Text = Data.Value,
            Margin = new(90, 5, 0, 44),
            Width = ButtonWidth
        };
        body = new()
        {
            FontSize = 15,
            Text = $"{Data.Value} 내신 산출하기 >>>",
            Margin = new(90, 35, 0, 13),
            HorizontalAlignment = HorizontalAlignment.Left,
            Width = ButtonWidth,
            Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0xA0, 0xA0, 0xA0))
        };
    }

}
