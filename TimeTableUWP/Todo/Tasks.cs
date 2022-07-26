#nullable enable

using System.Collections;

namespace TimeTableUWP.Todo;

using Match = Predicate<TodoTask>;
public class TaskList : IEnumerable<TodoTask>
{
    // Constructor
    public TaskList() { }

    // Properties
    public List<TodoTask> List { get; set; } = new();
    private Stack<List<TodoTask>> TaskStack { get; } = new(); // not using => here

    // Index & Interfaces
    public TodoTask this[int i] { get => List[i]; set => List[i] = value; }
    public IEnumerator<TodoTask> GetEnumerator() => List.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    // Lambdas & Get-only properties
    public bool IsNullOrEmpty => List is null || !List.Any();
    public int Count => List.Count;
    public int CountAll(Match? match) => match is null ? Count : List.FindAll(match).Count;

    /// <summary>
    /// Sorts tasks by due date
    /// </summary>
    public void Sort() => List.Sort((x, y) => x.DueDate.CompareTo(y.DueDate));
    public int FindIndex(Match? match) => List.FindIndex(match);
    public void Add(TodoTask task) => List.Add(task);

    /// <summary>
    /// Finds all that matches the Predicate of Task and return as List of TodoTask
    /// </summary>
    /// <param name="match">Expression to find</param>
    /// <returns>List of Tasks. If match is null, then an empty List of TodoTask</returns>
    public List<TodoTask> FindAll(Match? match) => match is null ? new() : List.FindAll(match);


    public void Remove(TodoTask task)
    {
        TaskStack.Push(new() { task });
        List.Remove(task);
    }

    /// <summary>
    /// Remove all that matches the exprssion. If null is given, same to Clear()
    /// </summary>
    /// <param name="match">Expression to delete</param>
    public void RemoveAll(Match? match)
    {
        if (IsNullOrEmpty)
            return;

        if (match is null)
        {
            TaskStack.Push(new(List));
            List.Clear();
            return;
        }

        List<TodoTask>? list = FindAll(match);
        TaskStack.Push(list);
        List = List.Except(list).ToList();
    }

    public static async Task<bool> DeleteTask(string taskName, TodoTask task)
    {
        ContentDialog contentDialog = new()
        {
            Title = "Delete",
            Content = $"Are you sure want to delete '{taskName}'?",
            PrimaryButtonText = "Yes",
            DefaultButton = ContentDialogButton.Primary,
            CloseButtonText = "No",
            RequestedTheme = Info.Settings.Theme,
        };
        if (await contentDialog.ShowAsync() is not ContentDialogResult.Primary)
            return false;

        TodoListPage.TaskList.Remove(task);
        return true;
    }

    public int Undo()
    {
        if (TaskStack.Count is 0)
            return 0;

        List<TodoTask> list = TaskStack.Pop();
        if (list.Count is 0)
            return 0;

        List.AddRange(list);
        return list.Count;
    }
}
