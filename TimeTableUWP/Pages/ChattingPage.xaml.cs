#nullable enable
#define BETA

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTableCore;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using static RollingRess.StaticClass;
// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace TimeTableUWP.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ChattingPage : Page
    {
        private bool isSending = false;
        private bool isCancelRequested = false;

        private int chatDelay = 1050;

        private int Convert(ActivationLevel level) => level switch
        {
            ActivationLevel.Developer => 0,
            ActivationLevel.Azure => 1,
            ActivationLevel.Bisque => 2,
            _ => throw new ArgumentException($"ChattingPage.Convert(ActivationLevel): unknown '{level}'.")
        };
        private string Convert(string level) => level switch
        {
            "0" => "Karu",
            "1" or "3" => "Azure",
            "2" or "4" => "Bisque",
            _ => throw new ArgumentException($"ChattingPage.Convert(string): unknown '{level}'")
        };
        private List<string> BadWords { get; set; } = new()
        {
            "씨발",
            "좆",
            "개새끼",
            "존나",
            "씨X"
        };

        private static string connectionString =>
            @"workstation id=gttchat.mssql.somee.com;packet size=4096;user id=nsun527_SQLLogin_2;pwd=16qlxq3sd1;data source=gttchat.mssql.somee.com;persist security info=False;initial catalog=gttchat";
        //            ConfigurationManager.ConnectionStrings["ChatDB"].ConnectionString;

        //https://docs.microsoft.com/en-us/windows/uwp/get-started/settings-learning-track
        //https://stackoverflow.com/questions/34803648/configurationmanager-and-appsettings-in-universal-uwp-app
        public ChattingPage()
        {
            InitializeComponent();
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!Info.User.IsSpecialLevel)
                    _ = await TimeTablePage.ActivateAsync("여기는 GTT 유저 대화방으로, Azure/Bisque 레벨만 이용할 수 있습니다.");

                if (!Info.User.IsSpecialLevel)
                {
                    _ = ShowMessageAsync("You need to be Auzre/Bisque level to use this chatroom.", "Limited feature", Info.Settings.Theme);
                    return;
                }

                textBox.IsEnabled = true;
                if (Info.User.ActivationLevel is ActivationLevel.Developer)
                    camoButtonA.Visibility = camoButtonB.Visibility = Visibility.Visible;

                _ = ReloadChatsAsync();
            }
            catch (Exception ex)
            {
                throw new TimeTableException("Page_Loaded: \n" + ex.ToString());
            }
        }

        private async void textBox_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            // 엔터 치면 메시지 보내기, 단 메시지 박스가 비어 있으면 안 됨.
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                if (string.IsNullOrWhiteSpace(textBox.Text))
                {
                    await ShowMessageAsync("보낼 메시지를 입력하세요.");
                    return;
                }
                await SendMessageAsync(Info.User.ActivationLevel);
            }
        }

        private async Task SendMessageAsync(ActivationLevel userLevel)
        {
            // 욕설 필터링
            if (BadWords.Any(s => textBox.Text.Contains(s)))
            {
                ContentDialog dialog = new()
                {
                    Content = "부적절한 말들이 포함되어 있습니다. 그래도 보내시겠습니까?",
                    Title = "GGHS Anonymous",
                    PrimaryButtonText = "Yes",
                    CloseButtonText = "No"
                };
                var response = await dialog.ShowAsync();
                if (response is ContentDialogResult.None)
                    return;
            }

            // Prepare send message
            isSending = true;
            textBox.IsEnabled = false;

            SqlConnection sql = new() { ConnectionString = connectionString };
            await sql.OpenAsync();

            SqlCommand cmd = new();
            cmd.Connection = sql;

            SqlParameter pSender = new("Sender", SqlDbType.TinyInt);
            SqlParameter pTime = new("Time", SqlDbType.DateTime);
            SqlParameter pMsg = new("Message", SqlDbType.NVarChar, 80);

            pSender.Value = Convert(userLevel);
            pTime.Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            pMsg.Value = textBox.Text;

            cmd.Parameters.Add(pSender);
            cmd.Parameters.Add(pTime);
            cmd.Parameters.Add(pMsg);

            cmd.CommandText = "INSERT INTO chatmsg(Sender, Time, Message) VALUES(@Sender, @Time, @Message)";
            await cmd.ExecuteNonQueryAsync();

            sql.Close();
            isSending = false;
            textBox.Text = string.Empty;
            textBox.IsEnabled = true;
        }

        private async Task ReloadChatsAsync()
        {
            string query = "SELECT * FROM chatmsg";

            try
            {
                while (true)
                {
                    while (isSending)
                        await Task.Delay(400);

                    if (isCancelRequested)
                        return;

                    SqlConnection sql = new(connectionString);
                    SqlCommand cmd = new(query, sql);
                    await sql.OpenAsync();

#if BETA
                    if (sql.State is not ConnectionState.Open)
                        throw new TimeTableException("Chatting Error: sql is not open");
#endif

                    DataTable dataTable = new();
                    using (SqlDataAdapter adapter = new(cmd))
                    {
                        adapter.Fill(dataTable);
                        sql.Close();
                    }

                    StringBuilder sb = new();
                    // TODO: 일부만 빼서 추가하든가..
                    foreach (DataRow row in dataTable.Rows)
                    {
#if BETA
                        if (row is null)
                        {
                            throw new NullReferenceException("Chatting Error: row is null\n" +
                                $"row: {row?.ToString()}\n"+
                                $"dataTable: {dataTable?.ToString()}\n");
                        }
#endif
                        DateTime dt = DateTime.Parse(row["Time"].ToString());
                        string sender = Convert(row["Sender"].ToString());
                        string msg = row["Message"].ToString();
                        sb.AppendLine($"({dt:MM/dd HH:mm}) {sender}: {msg}");
                    }
                    viewBox.Text = sb.ToString();
                    await Task.Delay(chatDelay);
                }
            }
            catch (Exception ex)
            {
                await ShowMessageAsync(ex.ToString(), "오류가 발생했습니다.");
                throw;
            }
        }

        private async void camoButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is not Button btn)
                return;

            if (btn.Name == "camoButtonA")
                await SendMessageAsync(ActivationLevel.Azure);
            else if (btn.Name == "camoButtonB")
                await SendMessageAsync(ActivationLevel.Bisque);
        }

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        => isCancelRequested = true;
    }
}