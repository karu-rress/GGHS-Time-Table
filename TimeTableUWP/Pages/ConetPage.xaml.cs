// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace TimeTableUWP.Pages;

/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class ConetPage : Page
{
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
        await ShowMessageAsync(@"""열정 가득한 사람들의 열정으로
열정 가득한 장을 만듭니다.""

고양국제고 창진프 셰어텍의 '꼬넷'을 소개합니다.
Conet은 재능거래 플랫폼으로, 자신이 가진 재능으로 타인에게
도움을 줄 수 있는 서비스입니다.", "Conet");
    }

    // ChattingPage 보면서 SQL 쿼리 따기
    private void RefreshButton_Click(object sender, RoutedEventArgs e)
    {

    }


}
