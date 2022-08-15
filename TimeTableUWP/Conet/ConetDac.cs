#nullable enable

using RollingRess.Security;

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

public class ConetUserDac
{
    private SqlConnection Sql { get; set; }
    public string Id { get; set; }
    public string EncryptedPassword { get; }
    public const int DefaultEggs = 50;

    public ConetUserDac(SqlConnection sql, ConetUser user)
    {
        Sql = sql;
        Id = $"{user.Id} {user.Name}";
        EncryptedPassword = "";
    }

    public ConetUserDac(SqlConnection sql, string id, string pw)
    {
        Sql = sql;
        Id = id;
        EncryptedPassword = Encryptor.SHA256(pw);
    }

    public async Task<bool> IdExistsAsync()
    {
        const string query = "SELECT COUNT(*) FROM users WHERE Student = @Student";

        SqlCommand cmd = new(query, Sql);
        SqlParameter pStudent = new("Student", SqlDbType.NChar, 10) { Value = Id };
        cmd.Parameters.Add(pStudent);

        return await cmd.ExecuteScalarAsync() is int i && i != 0;
    }

    public async Task<bool> PasswordMachesAsync()
    {
        const string query = "SELECT * FROM users WHERE Student = @Student";

        SqlCommand cmd = new(query, Sql);
        SqlParameter pStudent = new("Student", SqlDbType.NChar, 10) { Value = Id };
        cmd.Parameters.Add(pStudent);

        using SqlDataReader reader = await cmd.ExecuteReaderAsync();
        reader.Read();
        return reader.GetString(1).Trim() == EncryptedPassword;
    }

    public async Task InsertAsync()
    {
        // TODO: 정식 버전에선 30에그로 줄이기
        string query = $"INSERT INTO users VALUES(@Student, @Password, { DefaultEggs })";

        SqlCommand cmd = new(query, Sql);

        SqlParameter pStudent = new("Student", SqlDbType.NChar, 10) { Value = Id };
        SqlParameter pPassword = new("Password", SqlDbType.Char, 44) { Value = EncryptedPassword };

        cmd.Parameters.Add(pStudent);
        cmd.Parameters.Add(pPassword);

        await cmd.ExecuteNonQueryAsync();
    }

    public async Task<Egg> GetEggAsync()
    {
        const string query = "SELECT Eggs FROM users WHERE Student = @Student";

        SqlCommand cmd = new(query, Sql);
        SqlParameter pStudent = new("Student", SqlDbType.NChar, 10) { Value = Id };
        cmd.Parameters.Add(pStudent);

        return new((short)await cmd.ExecuteScalarAsync());
    }

    public async Task UpdateEggAsync(Egg egg)
    {
        const string query = "UPDATE users SET Eggs = @Eggs WHERE Student = @Student";

        SqlCommand cmd = new(query, Sql);

        SqlParameter pStudent = new("Student", SqlDbType.NChar, 10) { Value = Id };
        SqlParameter pEggs = new("Eggs", SqlDbType.SmallInt) { Value = (short)egg.Value };

        cmd.Parameters.Add(pStudent);
        cmd.Parameters.Add(pEggs);

        await cmd.ExecuteNonQueryAsync();
    }
}