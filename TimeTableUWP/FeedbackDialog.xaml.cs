using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.ApplicationModel.Contacts;
using Windows.ApplicationModel.Email;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

using System.Net.Mail;
using System.Net;
using Windows.UI.Popups;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;
using Windows.UI.Text;
//using EASendMail;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace TimeTableUWP
{
    static class SmtpExtension
    {
        public static Task SendAsync(this SmtpClient smtp, MailMessage msg) => Task.Run(() => smtp.Send(msg));
    }

    public sealed partial class FeedbackDialog : ContentDialog
    {
        bool isSending = false;
        public FeedbackDialog()
        {
            this.InitializeComponent();
        }

        private async void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            var text = textBox.Text;
            if (string.IsNullOrWhiteSpace(text))
            {
                MessageDialog message = new("Please enter text.", "Error");
                await message.ShowAsync();
                return;
            }

            var smtp = PrepareSendMail((string.IsNullOrEmpty(senderBox.Text) ? "" : $"Sender: {senderBox.Text}\n") + text, 
                $"GGHS Time Table Feedback for V{MainPage.Version}", out var msg);
            isSending = true;
            sendingMsgText.Visibility = progressRing.Visibility = Visibility.Visible;
            IsPrimaryButtonEnabled = IsSecondaryButtonEnabled = false;
            await smtp.SendAsync(msg);
            isSending = false;
            progressRing.Value = 100;
            sendingMsgText.Text = "Successfully sent!";
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

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void ContentDialog_Closing(ContentDialog sender, ContentDialogClosingEventArgs args)
        {
            if (isSending)
            {
                args.Cancel = true;
            }
        }
    }
}
