#nullable enable

using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

using TimeTableUWP.Pages;
using System.Net.NetworkInformation;
using RollingRess.Net;

namespace TimeTableUWP
{
    public static class SmtpExtension
    {
        public static Task SendAsync(this SmtpClient smtp, MailMessage msg) => Task.Run(() => smtp.Send(msg));
    }

    public sealed partial class FeedbackDialog : ContentDialog
    {
        private Brush TextColor => new SolidColorBrush(SettingsPage.IsDarkMode
            ? Color.FromArgb(0xFF, 0x25, 0xD1, 0xE8) : Color.FromArgb(0xFF, 0x22, 0x22, 0x88));

        public FeedbackDialog()
        {
            InitializeComponent();
            RequestedTheme = MainPage.Theme;
            ErrorMsgText.Foreground = TextColor;
        }

        private async void ContentDialog_PrimaryButtonClick(ContentDialog _, ContentDialogButtonClickEventArgs args)
        {
            args.Cancel = true;
            ErrorMsgText.Visibility = Visibility.Collapsed;
            var text = textBox.Text;

            if (string.IsNullOrWhiteSpace(text))
            {
                ErrorMsgText.Text = "Please enter text.";
                ErrorMsgText.Visibility = Visibility.Visible;
                return;
            }
            if (Connection.IsInternetAvailable is false)
            {
                ErrorMsgText.Text = "Sorry. Please check your internet connection.";
                ErrorMsgText.Visibility = Visibility.Visible;
                return;
            }

            var smtp = PrepareSendMail((string.IsNullOrEmpty(senderBox.Text)
            ? "" : $"This feedback is from \"{senderBox.Text}\".\n\n") + string.Join("\r\n", text.Split("\r")), // Converts NewLine
            $"GGHS Time Table Feedback for V{MainPage.Version}", out var msg);

            sendingMsgText.Visibility = progressRing.Visibility = Visibility.Visible;
            IsPrimaryButtonEnabled = false;
            await smtp.SendAsync(msg);

            progressRing.Value = 100;
            sendingMsgText.Text = "Successfully sent!";
            await Task.Delay(700);

            Hide();
        }

        public static SmtpClient PrepareSendMail(string body, string subject, out MailMessage msg)
        {
            MailAddress send = new(Sensitive.GTTMail);
            MailAddress to = new(Sensitive.KaruMail);
            SmtpClient smtp = new()
            {
                Host = "smtp.gmail.com",
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new NetworkCredential(send.Address, Sensitive.MailPassword),
                Timeout = 20_000
            };
            msg = new(send, to) { Subject = subject, Body = body };
            return smtp;
        }
    }
}
