#nullable enable

using System.Data;
using System.Data.SqlClient;
using System.Text;
using Windows.Storage;
using Windows.UI.Xaml.Input;

namespace TimeTableUWP.Pages;
public sealed partial class ChattingPage : Page
{
    private static bool isFirstLoaded = true;
    private bool isReloadPaused = false;
    private bool isCancelRequested = false;
    private const int chatDelay = 500;
    private const string title = "GGHS Anonymous";
    private const string chatFormat = "[{0:MM/dd HH:mm}]  {1}:\t{2}\n";

    private string ConnectionString { get; set; } = "";
    private string StringBuffer { get; set; } = "";
    private string LastSqlString { get; set; } = "";

    private List<string> BadWords { get; set; } = new()
    {
        "씨발",
        "좆",
        "개새끼",
        "존나",
        "지랄"
    };

    public ChattingPage()
    {
        InitializeComponent();
        viewBox.SelectionHighlightColor.Color = Info.Settings.ColorType;
        viewBox.SelectionHighlightColor.Opacity = 0.35;
        TextBoxLineColor.Color = Info.Settings.ColorType;
    }
    private async void Page_Loaded(object sender, RoutedEventArgs e)
    {
        Windows.Foundation.IAsyncOperation<string>? getString = FileIO.ReadTextAsync(await StorageFile.GetFileFromApplicationUriAsync(new("ms-appx:///connection.txt")));
        if (await TimeTablePage.AuthorAsync("여기는 GTT 유저 대화방으로, Azure/Bisque 레벨만 이용할 수 있습니다.") is false)
            return;

        if (await getString is not string str)
            throw new NullReferenceException("ConnectionString is null.");
        ConnectionString = str;

        if (Info.User.ActivationLevel is ActivationLevel.Developer)
        {
            camoButtonA.Visibility = camoButtonB.Visibility = sqlButton.Visibility
                = delButton.Visibility = infoButton.Visibility = Visibility.Visible;
            textBox.Margin = new(27, 0, 102, 18);
        }

        if (!RollingRess.Net.Connection.IsInternetAvailable)
        {
            await ShowMessageAsync("인터넷에 연결하지 못했습니다.\n네트워크 연결을 확인하세요.", "Connection Error");
            return;
        }

        if (isFirstLoaded)
            await LoadChatsAsync();

        textBox.IsEnabled = true;
        textBox.BorderBrush = new SolidColorBrush(Info.Settings.ColorType);
        isFirstLoaded = false;
        _ = ReloadChatsAsync().ConfigureAwait(false);
    }

    private async void textBox_KeyDown(object sender, KeyRoutedEventArgs e)
    {
        if (e.Key == Windows.System.VirtualKey.Enter)
            await SendMessageAsync(Info.User.ActivationLevel);
    }

    private async void Button_Click(object sender, RoutedEventArgs e)
    {
        if (sender is not Button btn)
            return;

        if (btn.Name is "camoButtonA")
            await SendMessageAsync(ActivationLevel.Azure);
        else if (btn.Name is "camoButtonB")
            await SendMessageAsync(ActivationLevel.Bisque);
        else if (btn.Name is "sqlButton")
            await RunSQL(textBox.Text);
        else if (btn.Name is "delButton")
            await DeleteMessage(textBox.Text);
        else if (btn.Name is "infoButton")
            await ShowMessageAsync("A/B: Azure/Bisque로 메시지 보내기\nSQL: SQL 쿼리 실행\nDEL: 메시지 삭제", title);
    }

    private async Task SendMessageAsync(ActivationLevel userLevel)
    {
        try
        {
            if (textBox.IsNullOrWhiteSpace())
            {
                await ShowMessageAsync("보낼 메시지를 입력하세요.", title);
                return;
            }
            // 욕설 필터링
            if (BadWords.Any(s => textBox.Text.Contains(s)))
            {
                ContentDialog dialog = new()
                {
                    Content = "부적절한 말들이 포함되어 있습니다. 그래도 보내시겠습니까?",
                    Title = title,
                    PrimaryButtonText = "Yes",
                    CloseButtonText = "No"
                };
                if (await dialog.ShowAsync() is ContentDialogResult.None)
                    return;
            }

            PrepareSend();
            using SqlConnection sql = new() { ConnectionString = ConnectionString };
            await sql.OpenAsync();

            SqlCommand cmd = new() { Connection = sql };

            SqlParameter pSender = new("Sender", SqlDbType.TinyInt) { Value = Convert(userLevel) };
            SqlParameter pTime = new("Time", SqlDbType.DateTime) { Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") };
            SqlParameter pMsg = new("Message", SqlDbType.NVarChar, 80) { Value = textBox.Text };

            cmd.Parameters.Add(pSender);
            cmd.Parameters.Add(pTime);
            cmd.Parameters.Add(pMsg);

            cmd.CommandText = "INSERT INTO chatmsg(Sender, Time, Message) VALUES(@Sender, @Time, @Message)";
            await cmd.ExecuteNonQueryAsync();
        }
        catch (Exception ex)
        {
            await ShowMessageAsync("채팅 전송에 실패했습니다.\n" + ex.ToString(), title);
            throw;
        }
        finally
        {
            DisposeSend();
        }
    }

    private async Task RunSQL(string query)
    {
        if (string.IsNullOrWhiteSpace(query))
        {
            await ShowMessageAsync("Enter query.", title);
            return;
        }
        try
        {
            PrepareSend();
            using SqlConnection sc = new(ConnectionString);
            using SqlCommand? cmd = sc.CreateCommand();
            await sc.OpenAsync();
            cmd.CommandText = query;
            await cmd.ExecuteNonQueryAsync();
        }
        catch (Exception e)
        {
            await ShowMessageAsync("Failed to run the SQL query. \n" + e.ToString(), title);
        }
        finally
        {
            DisposeSend();
        }
    }

    private async Task DeleteMessage(string message)
    {
        if (textBox.IsNullOrWhiteSpace())
        {
            ContentDialog dialog = new()
            {
                Content = "Nothing seems to be entered. Do you really want to proceed?",
                Title = title,
                PrimaryButtonText = "Yes",
                CloseButtonText = "No",
            };
            if (await dialog.ShowAsync() is ContentDialogResult.None)
                return;
        }
        await RunSQL(@$"DELETE FROM chatmsg WHERE Message=N'{message}'");
    }


    // 전체 메시지  받아오기
    private async Task LoadChatsAsync()
    {
        string txt = textBox.PlaceholderText;
        textBox.PlaceholderText = "채팅 불러오는 중...";

        DataSet ds = new();
        object result = new();
        using (SqlConnection sql = new(ConnectionString))
        {
            using SqlCommand cmd = new(@"select max(Time) from chatmsg", sql);
            SqlDataAdapter sda = new("SELECT * FROM chatmsg ORDER BY Time", sql);

            // Start SQL
            await sql.OpenAsync();
            result = await cmd.ExecuteScalarAsync();
            sda.Fill(ds, "chatmsg");
        } // End SQL

        DataTable dt = ds.Tables["chatmsg"];
        StringBuilder sb = new();

        foreach (DataRow row in dt.Rows)
        {
            // sb.AppendLine($"({DateTime.Parse(row["Time"].ToString()):MM/dd HH:mm}) {Convert(row["Sender"].ToString())}: {row["Message"]}");
            sb.AppendFormat(chatFormat, DateTime.Parse(row["Time"].ToString()), Convert((byte)row["Sender"]), row["Message"]);
        }

        LastSqlString = ((DateTime)result).ToString("yyyy-MM-dd HH:mm:ss.fff");
        viewBox.Text = StringBuffer = sb.ToString();
        ScrollViewBox();
        textBox.PlaceholderText = txt;
    }


    private async Task ReloadChatsAsync()
    {
        while (true)
        {
            try
            {
                string query1 = $"SELECT COUNT(*) FROM chatmsg WHERE Time > '{LastSqlString}'";
                string query2 = $"SELECT * FROM chatmsg WHERE Time > '{LastSqlString}' ORDER BY Time";

                while (isReloadPaused)
                    await Task.Delay(400);

                while (!RollingRess.Net.Connection.IsInternetAvailable)
                {
                    await ShowMessageAsync("인터넷에 연결하지 못했습니다.\n네트워크 연결을 확인하세요.", "Connection Error");
                    await Task.Delay(1000);
                }

                if (isCancelRequested)
                    return;

                SqlConnection sql = new(ConnectionString);
                using SqlCommand cmd = new(query1, sql);
                await sql.OpenAsync();

                if (await cmd.ExecuteScalarAsync() is 0)
                {
                    sql.Close();
                    await Task.Delay(chatDelay);
                    continue;
                }

                cmd.CommandText = query2;
                StringBuilder sb = new();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                        sb.AppendFormat(chatFormat, reader.GetDateTime(1), Convert(reader.GetByte(0)), reader.GetString(2));
                }
                    
                cmd.CommandText = "select max(Time) from chatmsg";
                object result = await cmd.ExecuteScalarAsync();
                sql.Close();

                LastSqlString = ((DateTime)result).ToString("yyyy-MM-dd HH:mm:ss.fff");
                StringBuffer += sb.ToString();
                viewBox.Text = StringBuffer;
                ScrollViewBox();
            }
            catch (Exception ex)
            {
                await ShowMessageAsync(ex.ToString(), "오류가 발생했습니다.");
                throw;
            }
        }
    }

    private void ScrollViewBox()
    {
        Grid? grid = VisualTreeHelper.GetChild(viewBox, 0) as Grid;
        for (int i = 0; i <= VisualTreeHelper.GetChildrenCount(grid) - 1; i++)
        {
            if (VisualTreeHelper.GetChild(grid, i) is not ScrollViewer sv) 
                continue;
            sv.ChangeView(0.0f, sv.ExtentHeight, 1.0f, true);
            break;
        }
    }

    private void Page_Unloaded(object sender, RoutedEventArgs e)
    => isCancelRequested = true;

    private int Convert(ActivationLevel level) => level switch
    {
        ActivationLevel.Developer => 0,
        ActivationLevel.Azure => 1,
        ActivationLevel.Bisque => 2,
        _ => throw new ArgumentException($"ChattingPage.Convert(ActivationLevel): unknown '{level}'.")
    };

    private string Convert(byte level) => level switch
    {
        0 => "Karu",
        1 or 3 => "Azure",
        2 or 4 => "Bisque",
        _ => throw new ArgumentException($"ChattingPage.Convert(string): unknown '{level}'")
    };

    private void PrepareSend()
    {
        isReloadPaused = true;
        textBox.IsEnabled = false;
    }

    private void DisposeSend()
    {
        isReloadPaused = false;
        textBox.IsEnabled = true;
        textBox.Text = string.Empty;
        ScrollViewBox();
    }
}
