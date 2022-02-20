using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
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
        public ChattingPage()
        {
            InitializeComponent();
        }

        private bool isSending = false;

        private int Convert(ActivationLevel level) => level switch
        {
            ActivationLevel.Developer => 0,
            ActivationLevel.Azure => 1,
            ActivationLevel.Bisque => 2,
        };

        private string Convert(string level) => level switch
        {
            "0" => "Karu",
            "1" or "3" => "Azure",
            "2" or "4" => "Bisque",
        };

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (!Info.User.IsAuthorized)
            {
                _ = await TimeTablePage.ActivateAsync("여기는 GTT 유저 대화방으로, Azure/Bisque 레벨만 이용할 수 있습니다.");
                if (!Info.User.IsAuthorized)
                {
                    _ = ShowMessageAsync("You need to be Auzre/Bisque level to use this chatroom.", "Limited feature", Info.Settings.Theme);
                    return;
                }
            }

            textBox.IsEnabled = true;
            if (Info.User.ActivationLevel is ActivationLevel.Developer)
                camoButtonA.Visibility = camoButtonB.Visibility = Visibility.Visible;

            _ = ReloadChatsAsync();
        }

        private async void textBox_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter)
                await SendMessageAsync(Info.User.ActivationLevel);
        }

        private async Task SendMessageAsync(ActivationLevel userLevel)
        {
            // Send Message
            isSending = true;
            textBox.IsEnabled = false;

            using SqlConnection sql = new();
            sql.ConnectionString = ConfigurationManager.ConnectionStrings["ChatDB"].ConnectionString;
            await sql.OpenAsync();

            SqlCommand cmd = new();
            cmd.Connection = sql;

            SqlParameter pSender = new("Sender", SqlDbType.TinyInt);
            pSender.Value = Convert(userLevel);

            SqlParameter pTime = new("Time", SqlDbType.DateTime);
            pTime.Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");

            SqlParameter pMsg = new("Message", SqlDbType.NVarChar, 80);
            pMsg.Value = textBox.Text;

            cmd.Parameters.Add(pSender);
            cmd.Parameters.Add(pTime);
            cmd.Parameters.Add(pMsg);

            cmd.CommandText = "INSERT INTO chatmsg(Sender, Time, Message) VALUES(@Sender, @Time, @Message)";
            await cmd.ExecuteNonQueryAsync();

            isSending = false;
            textBox.Text = String.Empty;
            textBox.IsEnabled = true;
        }

        private async Task ReloadChatsAsync()
        {
            string connection = ConfigurationManager.ConnectionStrings["ChatDB"].ConnectionString;
            string connString = connection;
            string query = "SELECT * FROM chatmsg";

            while (true)
            {
                while (isSending)
                    await Task.Delay(100);

                SqlConnection conn = new(connString);
                SqlCommand cmd = new(query, conn);
                conn.Open();

                SqlDataAdapter da = new(cmd);
                DataTable dataTable = new();
                da.Fill(dataTable);
                conn.Close();
                da.Dispose();

                StringBuilder sb = new();
                foreach (DataRow row in dataTable.Rows)
                {
                    DateTime dt = DateTime.Parse(row["Time"].ToString());
                    string sender = Convert(row["Sender"].ToString());
                    string msg = row["Message"].ToString();
                    sb.AppendLine($"({dt:MM/dd HH:mm}) {sender}: {msg}");
                }

                viewBox.Text = sb.ToString();

                // Chat Reload Delay
                await Task.Delay(800);
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
    }
}
