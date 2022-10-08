#nullable enable

namespace TimeTableUWP.Todo;

// TODO: readonly(X), record
public class TodoTask : IButtonData
{
    public DateTime DueDate { get; set; }
    public string Title { get; set; }
    public string? Body { get; set; }

    public TodoTask() : this(DateTime.Now, "", default) { }
    public TodoTask(in DateTime date, string title, string? body)
    {
        DueDate = date;
        Title = title;
        Body = body;
    }
}