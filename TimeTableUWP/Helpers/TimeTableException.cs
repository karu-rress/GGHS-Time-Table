#nullable enable

namespace TimeTableUWP;

using RollingRess.Net;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Popups;
using wux = Windows.UI.Xaml;

[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
internal sealed class ErrorCodeAttribute : Attribute
{
    public string PositionalString { get; }

    // This is a positional argument
    public ErrorCodeAttribute(string positionalString)
    {
        PositionalString = positionalString;
    }
}

[Serializable, ErrorCode("1xxx")]
public class TimeTableException : Exception
{
    public int ErrorCode { get; protected set; } = -1;
    public TimeTableException() { }
    public TimeTableException(string message) : base(message) { }
    public TimeTableException(string message, int errorCode) : this(message) { ErrorCode = errorCode; }
    public TimeTableException(string message, Exception inner) : base(message, inner) { }
    protected TimeTableException(SerializationInfo info, StreamingContext context) : base(info, context) { }

    public static async void HandleException(object _, wux::UnhandledExceptionEventArgs e)
    {
        // 여기에 try~catch 걸까
        e.Handled = true;
        await HandleException(e.Exception);
    }

    public static async Task HandleException(Exception exception)
    {
        int? code = (exception is TimeTableException te) ? te.ErrorCode : null;

        string message = @$"에러가 발생했습니다. {(Info.User.ActivationLevel is not ActivationLevel.Developer
            ? "다른 사용자들에게서 발생한 오류이므로 속히 해결 부탁드립니다."
            : "디버그 중 발생한 오류입니다.")}

==========EXCEPTION INFO==========
Code: {code?.ToString() ?? "Not Available"}
User Level: {Info.User.ActivationLevel}
User: {Info.User.Conet?.ToString() ?? "Unknown"}
Time: {DateTime.Now:yyyy-MM-dd HH:mm:ss}

========FULL REPRESENTATION========
{exception}";

        var smtp = FeedbackDialog.PrepareSendMail(message,
            $"GGHS Time Table EXCEPTION OCCURED in V{Info.Version}", out var msg);

        if (Connection.IsInternetAvailable)
        {
            Task mail = smtp.SendAsync(msg);

            if (Info.User.ActivationLevel is not ActivationLevel.Developer)
            {
                SqlConnection sql = new(ChatMessageDac.ConnectionString);
                ChatMessageDac chat = new(sql);
                await sql.OpenAsync();
                await chat.InsertAsync((byte)ChatMessageDac.Sender.GttBot, string.Format(Messages.ErrorChat, exception.GetType().Name));
                sql.Close();
            }
            await mail;
        }
        else if (Info.User.ActivationLevel is not ActivationLevel.Developer) // TODO : 종료시, 혹은 시작시 메일 보내고 삭제
        {
            int idx = 0;
            while (true)
            {
                if ((await ApplicationData.Current.LocalFolder.TryGetItemAsync($"error{idx}.txt")) is null)
                    break;
                idx++;
            }

            var storageFolder = ApplicationData.Current.LocalFolder;
            StorageFile errorLog = await storageFolder.CreateFileAsync($"error{idx}.txt");
            await FileIO.WriteTextAsync(errorLog, message);
        }
    }

    public static async Task SendUnsentErrorLogs()
    {
        int idx = 0;
        string message = "";

        while (true)
        {
            var storageFolder = ApplicationData.Current.LocalFolder;
            if ((await storageFolder.TryGetItemAsync($"error{idx}.txt")) is null)
                break; // file not exists.

            var errorLog = await storageFolder.GetFileAsync($"error{idx}.txt");
            message += await FileIO.ReadTextAsync(errorLog) + "\n\n";
            await errorLog.DeleteAsync();

            idx++;
        }

        if (idx is 0) return;

        var smtp = FeedbackDialog.PrepareSendMail(message,
    $"GGHS Time Table UNSENT {idx} EXCEPTIONS in V{Info.Version}", out var msg);
        Task mail = smtp.SendAsync(msg);
        await mail;
        SqlConnection sql = new(ChatMessageDac.ConnectionString);
        ChatMessageDac chat = new(sql);
        await sql.OpenAsync();
        await chat.InsertAsync((byte)ChatMessageDac.Sender.GttBot, $"⛔ERROR⛔ 카루님, 전송되지 못한 {idx}개의 에러가 보고되었습니다. 확인해주세요.");
        sql.Close();
    }
}



[Serializable, ErrorCode("2xxx")]
public class TableCellException : TimeTableException
{
    public TableCellException() { }
    public TableCellException(string message, int errorCode = 2000) : base(message, errorCode) { }
    public TableCellException(string message, Exception inner) : base(message, inner) { }
    protected TableCellException(SerializationInfo info, StreamingContext context) : base(info, context) { }
}

[Serializable, ErrorCode("3xxx")]
public class DataAccessException : TimeTableException
{
    public DataAccessException() { }
    public DataAccessException(string message, int errorCode = 3000) : base(message, errorCode) { }
    public DataAccessException(string message, Exception inner) : base(message, inner) { }
    protected DataAccessException(SerializationInfo info, StreamingContext context) : base(info, context) { }
}

[Serializable, ErrorCode("4xxx")]
public class ComboBoxDataException : TimeTableException
{
    public ComboBoxDataException() { }
    public ComboBoxDataException(string message, int errorCode = 4000) : base(message, errorCode) { }
    public ComboBoxDataException(string message, Exception inner) : base(message, inner) { }
    protected ComboBoxDataException(SerializationInfo info, StreamingContext context) : base(info, context) { }
}
