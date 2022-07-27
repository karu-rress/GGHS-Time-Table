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
        if (Info.User.Conet is null)
        {
            ConetLoginDialog dialog = new();
            await dialog.ShowAsync();

            if (Info.User.Conet is null)
            {
                await ShowMessageAsync("이 기능을 사용하기 위해선 로그인이 필요합니다.", title, Info.Settings.Theme);
                mainText2.Text = "로그인을 해 주세요.";
                AddButton.IsEnabled = RefreshButton.IsEnabled = false;
                return;
            }
        }

        if (Info.User.ActivationLevel is ActivationLevel.Developer)
            SignOutButton.Visibility = Visibility.Visible;

        await LoadEggsAsync();
        mainText2.Text = "지금. 여기. 우리. Conet";
        nameText.Text = $"{Info.User.Conet.Id} {Info.User.Conet.Name}님";
        eggText.Text = $"나의 에그: {Info.User.Conet.Eggs.Value} 에그";
        await LoadHelps();
    }

    private static async Task LoadEggsAsync()
    {
        using SqlConnection sql = new(ChatMessageDac.ConnectionString);
        await sql.OpenAsync();
        ConetUserDac conet = new(sql, Info.User.Conet);
        Info.User.Conet.Eggs = await conet.GetEggAsync();
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
                object price = row["Price"];
                ConetList.Add(new(
                    (DateTime)row["UploadDate"],
                    row["Uploader"].ToString(),
                    row["Title"].ToString(),
                    row["Body"].ToString(),
                    price == DBNull.Value ? null : price.ToString()));
            }
        }
        catch (SqlException) // 이건 내가 대응할 수가 없음. 그냥 Swallow.
        {
            await ShowMessageAsync(Messages.Dialog.ConetError, title, Info.Settings.Theme);
            return;
        }
        finally
        {
            Invisible(progressGrid);
        }

        var conetEnum = from conet in ConetList
                   orderby conet.Price descending, conet.UploadDate descending
                   select conet;

        foreach (var help in conetEnum)
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
        // load eggs async?
        conetGrid.Children.Clear();
        await LoadHelps();
    }

    private void AddButton_Click(object sender, RoutedEventArgs e)
        => Frame.Navigate(typeof(ConetAddPage), null, new DrillInNavigationTransitionInfo());

    private async void SignOutButton_Click(object sender, RoutedEventArgs e)
    {
        ContentDialog dialog = new()
        {
            Title = "Sign Out",
            Content = "Are you sure want to sign out?",
            PrimaryButtonText = "Yes, sign out",
            CloseButtonText = "Cancel",
            DefaultButton = ContentDialogButton.Primary,
        };
        if (await dialog.ShowAsync() is ContentDialogResult.Primary)
        {
            Info.User.Conet = null;
            Frame.Navigate(GetType());
        }
    }
}