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
        public FeedbackDialog()
        {
            this.InitializeComponent();
        }

        private async void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            if (string.IsNullOrEmpty(textBox.Text))
            {
                MessageDialog message = new("Please enter text.", "Error");
                await message.ShowAsync();
                return;
            }

            var smtp = PrepareSendMail((string.IsNullOrEmpty(senderBox.Text) ? "" : $"Sender: {senderBox.Text}\n") + textBox.Text, 
                $"GGHS Time Table Feedback for V{MainPage.Version}", out var msg);
            MessageDialog messageDialog = new("Sending feedback. Please wait for a while...", "Feedback");
            await messageDialog.ShowAsync();
            await smtp.SendAsync(msg);
            messageDialog = new("Feedback sent! Thank you.", "Success");
            await messageDialog.ShowAsync();
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
