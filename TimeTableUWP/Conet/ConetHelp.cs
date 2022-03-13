#nullable enable

namespace TimeTableUWP.Conet;

public record struct Student(int id, string name)
{
    public override string ToString() => $"{id} {name}";
}

public class ConetHelp
{
    [Obsolete]
    public ConetHelp(string title, Student uploader)
    {
        UploadDate = DateTime.Now;
        Title = title;
        Uploader = uploader;
    }

    public ConetHelp(string uploader, string title, string? body, string? price)
        : this(new Student(Convert.ToInt32(uploader[0..4]), uploader[5..]),
              title,
              body,
              string.IsNullOrEmpty(price) ? null : new(Convert.ToUInt32(price)))
    {  }

    public ConetHelp(Student uploader, string title, string? body = null, Egg? price = null)
    {
        UploadDate= DateTime.Now;
        Title= title;
        Body = body;
        Uploader = uploader;
        Price = price;
    }

    public DateTime UploadDate { get; set; }
    [Obsolete]
    public Egg? Price { get; set; }
    public string Title { get; set; }
    public string? Body { get; set; }
    public Student Uploader { get; set; }

// 관계 연산자는 UploadDate 기준으로.
}