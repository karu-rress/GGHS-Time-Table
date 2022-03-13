#nullable enable

using OnlineDacDict = System.Collections.Generic.Dictionary<(int cls, string subject), TimeTableCore.OnlineLink?>;

namespace TimeTableUWP.Helpers;

[Flags]
internal enum Classes : byte
{
    Class1 = 1,
    Class2 = 2,
    Class3 = 4,
    Class4 = 8,
    Class5 = 16,
    Class6 = 32,
    Class7 = 64,
    Class8 = 128,
}

internal class OnlineLinksDac
{
    public OnlineDacDict Dictionary { get; set; } = new();
    public async Task LoadLinksAsync()
    {
        if (!Connection.IsInternetAvailable)
            return;

        DataTable dt = new();
        using (SqlConnection sql = new(ChatMessageDac.ConnectionString))
        {
            await sql.OpenAsync();
            const string query = "SELECT * FROM online";

            SqlDataAdapter sda = new(query, sql);
            sda.Fill(dt);
        }

        foreach (DataRow row in dt.Rows)
        {
            Dictionary.Add(((byte)row["Class"], row["Subject"].ToString()), new()
            {
                Zoom = row["Zoom"] as string,
                Id = row["Id"] as string,
                Password = row["Password"] as string,
                Classroom = row["Classroom"] as string,
                Teacher = row["Teacher"].ToString(),
            });
        }
    }

    public Dictionary<string, OnlineLink> GetLinks(int @class)
    {
        Classes selected = @class switch
        {
            1 => Classes.Class1,
            2 => Classes.Class2,
            3 => Classes.Class3,
            4 => Classes.Class4,
            5 => Classes.Class5,
            6 => Classes.Class6,
            7 => Classes.Class7,
            8 => Classes.Class8,
        };
        var result = from row in Dictionary 
                     where ((Classes)row.Key.cls).HasFlag(selected)
                    select row;
        return result.ToDictionary(x => x.Key.subject, x => x.Value);
    }
}
