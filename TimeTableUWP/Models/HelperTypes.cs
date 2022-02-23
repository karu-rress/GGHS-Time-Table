using System.Net.Mail;

namespace TimeTableUWP;

public enum DateType
{
    YYYYMMDD,
    MMDDYYYY,
    YYYYMMDD2
}

public static class SmtpExtension
{
    public static Task SendAsync(this SmtpClient smtp, MailMessage msg) => Task.Run(() => smtp.Send(msg));
}
