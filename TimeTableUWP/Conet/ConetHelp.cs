#nullable enable

using System.Runtime.Serialization;

namespace TimeTableUWP.Conet;

[DataContract(Name = "ConetUser")]
public class ConetUser
{
    [DataMember] public int Id { get; init; }
    [DataMember] public string Name { get; init; }
    [DataMember] public Egg Eggs { get; set; }
    public ConetUser(int id, string name) { Id = id; Name = name; }

    /// <returns>a string like "3116 나선우"</returns>
    public override string ToString() => $"{Id} {Name}";
}

public class ConetHelp
{
    public ConetHelp(DateTime upload, string uploader, string title, string body, string? price)
        : this(upload, new ConetUser(Convert.ToInt32(uploader[0..4]), uploader[5..].TrimEnd()),
              title, body, string.IsNullOrEmpty(price) ? null : new((uint)Convert.ToInt16(price)))
    {
    }

    public ConetHelp(DateTime upload, ConetUser uploader, string title, string body = "", Egg? price = null)
    {
        UploadDate = upload;
        Title = title;
        Body = body;
        Uploader = uploader;
        Price = price;
    }

    public DateTime UploadDate { get; }
    public Egg? Price { get; set; }
    public string Title { get; set; }
    public string? Body { get; set; }
    public ConetUser Uploader { get; set; }

    // 관계 연산자는 UploadDate 기준으로.
    public static bool operator <(ConetHelp c1, ConetHelp c2) => c1.UploadDate < c2.UploadDate;
    public static bool operator >(ConetHelp c1, ConetHelp c2) => c1.UploadDate > c2.UploadDate;
}