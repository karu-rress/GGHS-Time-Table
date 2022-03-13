#nullable enable

namespace TimeTableUWP.Conet;

public record Student(int id, string name);

public class ConetHelp
{
    public ConetHelp(string title, Student uploader)
    {
        UploadDate = DateTime.Now;
        Title = title;
        Uploader = uploader;
    }
    public DateTime UploadDate { get; set; }
    [Obsolete]
    public Egg? Price { get; set; }
    public string Title { get; set; }
    public string? Body { get; set; }
    public Student Uploader { get; set; }

// 관계 연산자는 UploadDate 기준으로.
}