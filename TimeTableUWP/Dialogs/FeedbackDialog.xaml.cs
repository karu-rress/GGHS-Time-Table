#nullable enable

using System;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

using System.Net.Mail;
using System.Net;
using static RollingRess.StaticClass;
using Windows.UI.Xaml.Media;
using Windows.UI;
using TimeTableUWP.Pages;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace TimeTableUWP
{
    static class SmtpExtension
    {
        public static Task SendAsync(this SmtpClient smtp, MailMessage msg) => Task.Run(() => smtp.Send(msg));
    }

    public sealed partial class FeedbackDialog : ContentDialog
    {

        Brush TextColor => new SolidColorBrush(SettingsPage.IsDarkMode
            ? Color.FromArgb(0xFF, 0x25, 0xD1, 0xE8)
            : Color.FromArgb(0xFF, 0x22, 0x22, 0x88));

        public FeedbackDialog()
        {
            InitializeComponent();
            RequestedTheme = MainPage.Theme;
            ErrorMsgText.Foreground = TextColor;
        }

        private async void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            var text = textBox.Text;
            ErrorMsgText.Visibility = Visibility.Collapsed;
            args.Cancel = true;

            if (string.IsNullOrWhiteSpace(text))
            {
                ErrorMsgText.Text = "Please enter text.";
                ErrorMsgText.Visibility = Visibility.Visible;
                return;
            }

            var smtp = PrepareSendMail((string.IsNullOrEmpty(senderBox.Text) ? "" : $"This feedback is from \"{senderBox.Text}\".\n") + text, 
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
            MailAddress send = new("gghstimetable@gmail.com");
            MailAddress to = new("nsun527@naver.com");
            SmtpClient smtp = new()
            {
                Host = "smtp.gmail.com",
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new NetworkCredential(send.Address, "rflnapjbcqznllqu"),
                Timeout = 20_000
            };
            msg = new(send, to)
            {
                Subject = subject,
                Body = body
            };
            return smtp;
        }
    }
}
