#nullable enable

using System.Text;
using TimeTableUWP.Pages;

namespace TimeTableUWP;

// sql dac 만들고
// timetableexception 그거처리할 수 ㅣㅇㅆ도록- 

public class ChatMessageDac : IDisposable
{
    private SqlConnection Sql { get; set; }
    public Action? Prepare { get; set; }
    public Action? Finally { get; set; }

    private static DateTime LastSqlTime;

    public ChatMessageDac(SqlConnection sql, Action? prepare = null, Action? dispose = null)
    {
        prepare?.Invoke();
        Sql = sql;
        Finally = dispose;
    }

    public void Dispose()
    {
        Finally?.Invoke();
    }

    /// <summary>
    /// Runs SQL Command
    /// </summary>
    public async Task RunSqlCommand(string query)
    {
        SqlCommand cmd = new(query, Sql);
        await cmd.ExecuteNonQueryAsync();
    }

    /// <summary>
    /// Delete message from SQL server.
    /// </summary>
    /// <param name="message">string to delete</param>
    public async Task DeleteAsync(string message)
    {
        const string query = "DELETE FROM chatmsg WHERE Message=@Message";

        SqlCommand cmd = new(query, Sql);
        SqlParameter pMessage = new("Message", SqlDbType.NVarChar, 80) { Value = message };
        cmd.Parameters.Add(pMessage);

        await cmd.ExecuteNonQueryAsync();
    }

    public async Task InsertAsync(byte sender, string message)
    {
        const string query = "INSERT INTO chatmsg(Sender, Time, Message) VALUES(@Sender, @Time, @Message)";

        SqlCommand cmd = new(query, Sql);
        SqlParameter pSender = new("Sender", SqlDbType.TinyInt) { Value = sender };
        SqlParameter pTime = new("Time", SqlDbType.DateTime) { Value = DateTime.Now };
        SqlParameter pMsg = new("Message", SqlDbType.NVarChar, 80) { Value = message };

        cmd.Parameters.Add(pSender);
        cmd.Parameters.Add(pTime);
        cmd.Parameters.Add(pMsg);

        await cmd.ExecuteNonQueryAsync();
    }



    private async Task GetMaxTimeAsync()
    {
        const string query = "select max(Time) from chatmsg";

        using SqlCommand cmd = new(query, Sql);
        LastSqlTime = (DateTime)await cmd.ExecuteScalarAsync();
    }

    public async Task SelectAll(DataTable table)
    {
        const string query = "SELECT * FROM chatmsg ORDER BY Time";

        SqlDataAdapter sda = new(query, Sql);
        sda.Fill(table);
        await GetMaxTimeAsync();
    }

    public Task<object> GetNewMessagesCountAsync()
    {
        const string query = $"SELECT COUNT(*) FROM chatmsg WHERE Time > @Time";

        SqlCommand cmd = new(query, Sql);
        SqlParameter pTime = new("Time", SqlDbType.DateTime) { Value = LastSqlTime };
        cmd.Parameters.Add(pTime);

        return cmd.ExecuteScalarAsync();
    }

    public async Task<string> GetNewMessagesAsync()
    {
        const string query = $"SELECT * FROM chatmsg WHERE Time > @Time ORDER BY Time";

        SqlCommand cmd = new(query, Sql);
        SqlParameter pTime = new("Time", SqlDbType.DateTime) { Value = LastSqlTime };
        cmd.Parameters.Add(pTime);

        StringBuilder sb = new();
        using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
        {
            while (reader.Read())
                sb.AppendFormat(Datas.ChatFormat, reader.GetDateTime(1), ChattingPage.Convert(reader.GetByte(0)), reader.GetString(2));
        }

        await GetMaxTimeAsync();
        return sb.ToString();
    }
}
