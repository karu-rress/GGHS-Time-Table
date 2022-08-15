using System.Net.Mail;

namespace TimeTableUWP;

public enum DateType
{
    YYYYMMDD,
    MMDDYYYY,
    YYYYMMDD2
}

// Ensures that a class or struct is sync-able with SQL server.
public interface ISyncable
{
    Task SyncAsync();
}

public static class SmtpExtension
{
    public static Task SendAsync(this SmtpClient smtp, MailMessage msg) => Task.Run(() => smtp.Send(msg));
}