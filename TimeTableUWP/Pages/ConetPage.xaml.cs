using TimeTableUWP.Conet;
using TimeTableUWP.Dialogs;
using Windows.UI.Xaml.Media.Animation;

namespace TimeTableUWP.Pages;

/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class ConetPage : Page
{
    private const string title = "Conet";
    public static List<ConetHelp> ConetList { get; set; } = new();
    
    public ConetPage()
    {
        InitializeComponent();

        imgSource.UriSource = Info.Settings.Theme switch
        {
            ElementTheme.Light => new("ms-appx:///Assets/Conet/conet_light.png"),
            ElementTheme.Dark => new("ms-appx:///Assets/Conet/conet_dark.png"),
        };
    }

    private async void Page_Loaded(object sender, RoutedEventArgs e)
    {
        if (!Connection.IsInternetAvailable)
        {
            await ShowMessageAsync("인터넷에 연결되어 있어야 Conet을 사용할 수 있습니다.", title, Info.Settings.Theme);
            return;
        }
        conetGrid.Children.Clear();
        ConetLoginDialog dialog = new();
        await dialog.ShowAsync();

        if (Info.User.Conet is null)
        {
            await ShowMessageAsync("이 기능을 사용하기 위해선 로그인이 필요합니다.", title, Info.Settings.Theme);
            mainText2.Text = "로그인을 해 주세요.";
            return;
        }

        mainText2.Text = "지금. 여기. 우리. Conet";
        await LoadHelps();
    }

    private async Task LoadHelps()
    {
        ConetList.Clear();
        Visible(progressGrid);
        try
        {
            DataTable dt = new();
            using (SqlConnection sql = new(ChatMessageDac.ConnectionString))
            {
                ConetHelpDac conet = new(sql);
                await sql.OpenAsync();
                conet.SelectAll(dt);
            }

            StringBuilder sb = new();
            foreach (DataRow row in dt.Rows)
            {
                ConetList.Add(new(
                    (DateTime)row["UploadDate"],
                    row["Uploader"].ToString(),
                    row["Title"].ToString(),
                    (row["Body"] is string body) ? body : null,
                    (row["Price"] is string price) ? price : null));
            }
        }
        catch (SqlException) // 이건 내가 대응할 수가 없음. 그냥 Swallow.
        {
            await ShowMessageAsync("서버 연결에 실패했습니다.\n다른 탭으로 나간 뒤, 다시 Conet에 들어오면\n재접속을 시도합니다.", title, Info.Settings.Theme);
            return;
        }
        finally
        {
            Invisible(progressGrid);
        }

        foreach (var help in ConetList)
            conetGrid.Children.Add(new ConetButton(help, ConetButton_Click));
    }

    private void ConetButton_Click(object sender, RoutedEventArgs e)
    {
        if (sender is ConetButton cb)
        {
            ConetAddPage.Conet = cb.ConetHelp;
            Frame.Navigate(typeof(ConetAddPage));
        }
    }

    // ChattingPage 보면서 SQL 쿼리 따기
    private async void RefreshButton_Click(object sender, RoutedEventArgs e)
    {
        conetGrid.Children.Clear();
        await LoadHelps();
    }

    private void AddButton_Click(object sender, RoutedEventArgs e)
        => Frame.Navigate(typeof(ConetAddPage), null, new DrillInNavigationTransitionInfo());
}