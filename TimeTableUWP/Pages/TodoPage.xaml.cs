#nullable enable

using Windows.UI.Xaml.Media.Animation;
using TimeTableUWP.Todo;

namespace TimeTableUWP.Pages;

public sealed partial class TodoListPage : Page
{
    public static TaskList TaskList { get; set; } = new();

    public TodoListPage()
    {
        InitializeComponent();
        RequestedTheme = Info.Settings.Theme;
    }

    private void Page_Loaded(object sender, RoutedEventArgs e)
    {
        LoadTasks();
    }

    /// <summary>
    /// Adds task buttons to the grid.
    /// </summary>
    private void LoadTasks()
    {
        if (TaskList.IsNullOrEmpty)
            return;

        TaskList.Sort();
        int buttons = 0;
        foreach (var task in TaskList)
            TaskGrid.Children.Add(new TaskButton(task, TaskButton_Click, buttons++));
    }

    /// <summary>
    /// Removes all buttons in the grid and reload it.
    /// </summary>
    private void ReloadTasks()
    {
        TaskGrid.Children.Clear();
        LoadTasks();
    }

    private void AddButton_Click(object _, RoutedEventArgs e)
        => Frame.Navigate(typeof(AddPage), null, new DrillInNavigationTransitionInfo());

    private async Task DeleteTasks(Predicate<TodoTask>? match)
    {
        if (TaskList.IsNullOrEmpty)
        {
            await ShowMessageAsync("Nothing to delete.", "Delete", theme: Info.Settings.Theme);
            return;
        }

        int cnt = TaskList.CountAll(match);
        if (cnt is 0)
        {
            await ShowMessageAsync("Nothing to delete.", "Delete", theme: Info.Settings.Theme);
            return;
        }

        const string title = "Delete";
        ContentDialog contentDialog = new()
        {
            Content = $"Are you sure want to delete {cnt} {"task".PutS(cnt)}?",
            Title = title,
            CloseButtonText = "Cancel",
            PrimaryButtonText = "Yes, delete",
            DefaultButton = ContentDialogButton.Primary,
            RequestedTheme = Info.Settings.Theme,
        };
        if (await contentDialog.ShowAsync() is ContentDialogResult.None)
            return;

        TaskList.RemoveAll(match);

        ReloadTasks();
        contentDialog = new ContentMessageDialog($"Successfully deleted {cnt} {"task".PutS(cnt)}.", title, "Close");
        await contentDialog.ShowAsync();
    }

    private async void DeletePastButton_Click(object _, RoutedEventArgs e)
        => await DeleteTasks(x => x.DueDate.Date < DateTime.Now.Date);

    private async void DeleteAllButton_Click(object _, RoutedEventArgs e) 
        => await DeleteTasks(null);

    private async void SelectDate_Click(object _, RoutedEventArgs e)
    {
        DateSelectDialog dialog = new();
        if (await dialog.ShowAsync() is ContentDialogResult.None)
            return;

        var date = dialog.SelectedDate;
        await DeleteTasks(x => x.DueDate == date);
    }

    private async void SelectSubject_Click(object _, RoutedEventArgs e)
    {
        SubjectSelectDialog dialog = new();
        if (await dialog.ShowAsync() is ContentDialogResult.None)
            return;

        await DeleteTasks(x => x.Subject == dialog.SelectedSubject);
    }

    private void TaskButton_Click(object sender, RoutedEventArgs e)
    {
        if (sender is TaskButton tb)
        {
            AddPage.Task = tb.Task;
            Frame.Navigate(typeof(AddPage));
        }
    }

    private async void UndoButton_Click(object sender, RoutedEventArgs e)
    {
        int result = TaskList.Undo();
        if (result is 0)
        {
            await ShowMessageAsync("Nothing to restore.", "Undo Delete", theme: Info.Settings.Theme);
            return;
        }
        ReloadTasks();
        ContentMessageDialog msg = new($"Successfully restored {result} {"item".PutS(result)}.", "Undo Delete");
        await msg.ShowAsync();
    }
}
