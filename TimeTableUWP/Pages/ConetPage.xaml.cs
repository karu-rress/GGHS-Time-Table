using TimeTableUWP.Conet;

namespace TimeTableUWP.Pages;

/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class ConetPage : Page
{
    public List<ConetHelp> ConetList { get; set; } = new();
    private const string title = "Conet";

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
        var msg = ShowMessageAsync(@"""열정 가득한 사람들의 열정으로
열정 가득한 장을 만듭니다.""

고양국제고 창진프 셰어텍의 '꼬넷'을 소개합니다.
Conet은 재능거래 플랫폼으로, 자신이 가진 재능으로 타인에게
도움을 줄 수 있는 서비스입니다.", "Conet");

        // ConetList.Add(new("수행평가 대리응시자 구합니다!", new(3116, "나선우")) { Body = "사탐방 하다가 빡쳐서요! 대신 좀 해주실 분!", Price = new(10)});
        // 인터넷 없으면 리턴
        await LoadHelps();

        await msg;
    }

    private async Task LoadHelps()
    {
        // progresgrid 따오기 (chattingpage)
        // SQL 다운로드 후
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
                    row["Uploader"].ToString(),
                    row["Title"].ToString(),
                    row["Body"]?.ToString(),
                    row["Price"]?.ToString()));
            }
        }
        catch (SqlException) // 이건 내가 대응할 수가 없음. 그냥 Swallow.
        {
            await ShowMessageAsync("서버 연결에 실패했습니다.\n다른 탭으로 나간 뒤, 다시 Conet에 들어오면\n재접속을 시도합니다.", title, Info.Settings.Theme);
            return;
        }
        finally
        {
            // Invisible(progressGrid);
        }

        foreach (var help in ConetList)
            conetGrid.Children.Add(new ConetButton(help, (o, e) => { }));
    }



    // ChattingPage 보면서 SQL 쿼리 따기
    private void RefreshButton_Click(object sender, RoutedEventArgs e)
    {

    }

    private void AddButton_Click(object sender, RoutedEventArgs e)
    {

    }
}
