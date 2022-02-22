﻿#nullable enable

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTableCore;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using static RollingRess.StaticClass;

namespace TimeTableUWP.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ChattingPage : Page
    {
        private static bool isFirstLoaded = true;
        private bool isSending = false;
        private bool isCancelRequested = false;
        private const int chatDelay = 1050;
        private const string title = "GGHS Anonymous";
        private string connectionString { get; set; } = "";
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
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            var getString = FileIO.ReadTextAsync(await StorageFile.GetFileFromApplicationUriAsync(new("ms-appx:///connection.txt")));
            if (await TimeTablePage.AuthorAsync("여기는 GTT 유저 대화방으로, Azure/Bisque 레벨만 이용할 수 있습니다.") is false)
                return;

            string? connectionString = await getString;
            if (connectionString == null)
                throw new NullReferenceException("ConnectionString is null.");
            this.connectionString = connectionString;
            _ = ReloadChatsAsync();

            if (Info.User.ActivationLevel is ActivationLevel.Developer)
                camoButtonA.Visibility = camoButtonB.Visibility = sqlButton.Visibility 
                    = delButton.Visibility = infoButton.Visibility = Visibility.Visible;

            if (isFirstLoaded)
            {
                string txt = textBox.PlaceholderText;
                textBox.PlaceholderText = "채팅 불러오는 중...";
                await Task.Delay(1300);
                textBox.PlaceholderText = txt;
            }

            textBox.IsEnabled = true;
            isFirstLoaded = false;
        }

        private async void textBox_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            // 엔터 치면 메시지 보내기, 단 메시지 박스가 비어 있으면 안 됨.
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
                if (string.IsNullOrWhiteSpace(textBox.Text))
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
                    var response = await dialog.ShowAsync();
                    if (response is ContentDialogResult.None)
                        return;
                }

                PrepareSend();
                using SqlConnection sql = new() { ConnectionString = connectionString };
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
                using SqlConnection sc = new(connectionString);
                using var cmd = sc.CreateCommand();
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
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                ContentDialog dialog = new()
                {
                    Content = "Nothing seems to be entered. Do you really want to proceed?",
                    Title = title,
                    PrimaryButtonText = "Yes",
                    CloseButtonText = "No",
                };
                var result = await dialog.ShowAsync();
                if (result is ContentDialogResult.None)
                    return;
            }
            await RunSQL(@$"DELETE FROM chatmsg WHERE Message=N'{message}'");
        }

        private async Task ReloadChatsAsync()
        {
            const string query = "SELECT * FROM chatmsg ORDER BY Time";
            while (true)
            {
                while (isSending)
                    await Task.Delay(400);

                if (isCancelRequested)
                    return;

                SqlConnection sql = new(connectionString);
                SqlCommand cmd = new(query, sql);
                await sql.OpenAsync();

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
                    DateTime dt = DateTime.Parse(row["Time"].ToString());
                    string sender = Convert(row["Sender"].ToString());
                    string msg = row["Message"].ToString();
                    sb.AppendLine($"({dt:MM/dd HH:mm}) {sender}: {msg}");
                }
                viewBox.Text = sb.ToString();

                ScrollToBottom(viewBox);
                await Task.Delay(chatDelay);
            }
        }

        private void ScrollToBottom(TextBox textBox)
        {
            var grid = (Grid)VisualTreeHelper.GetChild(textBox, 0);
            for (var i = 0; i <= VisualTreeHelper.GetChildrenCount(grid) - 1; i++)
            {
                object obj = VisualTreeHelper.GetChild(grid, i);
                if (!(obj is ScrollViewer)) continue;
                ((ScrollViewer)obj).ChangeView(0.0f, ((ScrollViewer)obj).ExtentHeight, 1.0f, true);
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
        private string Convert(string level) => level switch
        {
            "0" => "Karu",
            "1" or "3" => "Azure",
            "2" or "4" => "Bisque",
            _ => throw new ArgumentException($"ChattingPage.Convert(string): unknown '{level}'")
        };

        private void PrepareSend()
        {
            isSending = true;
            textBox.IsEnabled = false;
        }

        private void DisposeSend()
        {
            isSending = false;
            textBox.IsEnabled = true;
            textBox.Text = string.Empty;
        }
    }
}