#nullable enable

using Windows.Storage;

namespace TimeTableUWP.Conet;


public class ConetHelpDac : IDisposable
{
    private SqlConnection Sql { get; set; }
    public Action? Prepare { get; set; }
    public Action? Finally { get; set; }

    // private static DateTime LastSqlTime;

    public ConetHelpDac(SqlConnection sql, Action? prepare = null, Action? dispose = null)
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
    /// Delete message from SQL server.
    /// </summary>
    public async Task DeleteAsync(DateTime upload)
    {
        const string query = "DELETE FROM conet WHERE UploadDate=@UploadDate";

        SqlCommand cmd = new(query, Sql);
        SqlParameter pTime = new("UploadDate", SqlDbType.DateTime) { Value = upload };
        cmd.Parameters.Add(pTime);

        await cmd.ExecuteNonQueryAsync();
    }

    public async Task InsertAsync(ConetHelp conet)
    {
        const string query = "INSERT INTO conet VALUES(@UploadDate, @Price, @Title, @Body, @Uploader)";

        SqlCommand cmd = new(query, Sql);

        SqlParameter pUpload = new("UploadDate", SqlDbType.DateTime) { Value = conet.UploadDate };
        SqlParameter pPrice = new("Price", SqlDbType.SmallInt) { Value = (object?)conet.Price?.Value ?? DBNull.Value };
        SqlParameter pTitle = new("Title", SqlDbType.NVarChar, 50) { Value = conet.Title };
        SqlParameter pBody = new("Body", SqlDbType.NVarChar, 180) { Value = (object?)conet.Body ?? DBNull.Value };
        SqlParameter pUploader = new("Uploader", SqlDbType.NChar, 10) { Value = conet.Uploader.ToString() };

        cmd.Parameters.Add(pUpload);
        cmd.Parameters.Add(pPrice);
        cmd.Parameters.Add(pTitle);
        cmd.Parameters.Add(pBody);
        cmd.Parameters.Add(pUploader);

        await cmd.ExecuteNonQueryAsync();
    }

    public async Task UpdateAsync(DateTime upload, ConetHelp conet)
    {
        const string query = @"UPDATE conet
SET Price = @Price, Title = @Title, Body = @Body
WHERE UploadDate = @UploadDate";

        SqlCommand cmd = new(query, Sql);

        SqlParameter pUpload = new("UploadDate", SqlDbType.DateTime) { Value = upload };
        SqlParameter pPrice = new("Price", SqlDbType.SmallInt) { Value = (object?)conet.Price?.Value ?? DBNull.Value };
        SqlParameter pTitle = new("Title", SqlDbType.NVarChar, 50) { Value = conet.Title };
        SqlParameter pBody = new("Body", SqlDbType.NVarChar, 180) { Value = (object?)conet.Body ?? DBNull.Value };

        cmd.Parameters.Add(pUpload);
        cmd.Parameters.Add(pPrice);
        cmd.Parameters.Add(pTitle);
        cmd.Parameters.Add(pBody);

        await cmd.ExecuteNonQueryAsync();
    }

    public void SelectAll(DataTable table)
    {
        const string query = "SELECT * FROM conet ORDER BY UploadDate DESC";

        SqlDataAdapter sda = new(query, Sql);
        sda.Fill(table);
    }
}
