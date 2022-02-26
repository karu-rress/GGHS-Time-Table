#nullable enable

using System.Net;
using System.Net.Mail;

namespace TimeTableUWP;

public sealed partial class FeedbackDialog : ContentDialog
{
    private Brush TextColor => new SolidColorBrush(Info.Settings.IsDarkMode
        ? Color.FromArgb(0xFF, 0x25, 0xD1, 0xE8) : Color.FromArgb(0xFF, 0x22, 0x22, 0x88));

    public FeedbackDialog()
    {
        InitializeComponent();
        RequestedTheme = Info.Settings.Theme;
        ErrorMsgText.Foreground = TextColor;
    }

    private async void ContentDialog_PrimaryButtonClick(ContentDialog _, ContentDialogButtonClickEventArgs args)
    {
        args.Cancel = true;
        ErrorMsgText.Visibility = Visibility.Collapsed;
        string? text = textBox.Text;

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

        SqlConnection sql = new(ChatMessageDac.ConnectionString);
        ChatMessageDac chat = new(sql);
        await sql.OpenAsync();

        var smtp = PrepareSendMail((senderBox.IsNullOrEmpty() ? "" : $"This feedback is from \"{senderBox.Text}\".\n\n")
        + string.Join("\r\n", text.Split("\r")), // Converts NewLine
        $"GGHS Time Table Feedback for V{Info.Version}", out var msg);

        sendingMsgText.Visibility = progressRing.Visibility = Visibility.Visible;
        IsPrimaryButtonEnabled = false;
        var mailTask = smtp.SendAsync(msg);
        
        await chat.InsertAsync((byte)ChatMessageDac.Sender.GttBot, string.Format(Messages.FeedbackChat, Info.Version));

        sql.Close();
        await mailTask;
        progressRing.Value = 100;
        sendingMsgText.Text = "Successfully sent!";
        await Task.Delay(600);

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
