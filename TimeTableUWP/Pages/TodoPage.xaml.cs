#nullable enable

using Windows.UI.Xaml.Media.Animation;
using TimeTableUWP.Todo;
using Windows.Storage;
using static RollingRess.Serializer;

namespace TimeTableUWP.Pages;

public sealed partial class TodoListPage : Page
{
    public static TaskList TaskList { get; set; } = new();
    private readonly DateTime sat = new(2022, 11, 17);

    public TodoListPage()
    {
        InitializeComponent();
        RequestedTheme = Info.Settings.Theme;
    }

    private void Page_Loaded(object sender, RoutedEventArgs e)
    {
        LoadTasks();
        var now = DateTime.Now;
        int days = (new DateTime(now.Year, now.Month, now.Day) - sat).Days;
        dDayText.Text = days switch
        {
            < 0 => $"D{days}",
            0 => "D-Day",
            > 0 => $"🎓🎉"
        };
    }

    /// <summary>
    /// Adds task buttons to the grid.
    /// </summary>
    private void LoadTasks()
    {
        if (TaskList.IsNullOrEmpty)
            return;

        TaskList.Sort();
        foreach (TodoTask? task in TaskList)
            TaskGrid.Children.Add(new TaskButton(task, TaskButton_Click));
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
        await ShowMessageAsync($"Successfully deleted {cnt} {"task".PutS(cnt)}.", title, Info.Settings.Theme);
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

        DateTime date = dialog.SelectedDate;
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
            AddPage.Task = tb.TodoTask;
            Frame.Navigate(typeof(AddPage));
        }
    }

    private async void UndoButton_Click(object sender, RoutedEventArgs e)
    {
        int result = TaskList.Undo();
        if (result is 0)
        {
            await ShowMessageAsync("Nothing to restore.", "Undo Delete", Info.Settings.Theme);
            return;
        }
        ReloadTasks();
        await ShowMessageAsync($"Successfully restored {result} {"item".PutS(result)}.", "Undo Delete", Info.Settings.Theme);
    }

    const string todo = "TodoList";
    private async void BackupButton_Click(object sender, RoutedEventArgs e)
    {
        if (!Connection.IsInternetAvailable)
        {
            await ShowMessageAsync("Internet connection needed.", "Backup Error", Info.Settings.Theme);
            return;
        }
        ApplicationDataContainer roamingSettings = ApplicationData.Current.RoamingSettings;
        roamingSettings.Values[todo] = Serialize(TaskList.List);

        await ShowMessageAsync("Successfully backed up your list.", "GGHS Todo", Info.Settings.Theme);
    }

    private async void RestoreButton_Click(object sender, RoutedEventArgs e)
    {
        if (!Connection.IsInternetAvailable)
        {
            await ShowMessageAsync("Internet connection needed.", "Restore Error", Info.Settings.Theme);
            return;
        }
        ApplicationDataContainer roamingSettings = ApplicationData.Current.RoamingSettings;
        if (Deserialize<List<TodoTask>>(roamingSettings.Values[todo]) is not List<TodoTask> tasklist)
        {
            await ShowMessageAsync("Nothing to restore", "GGHS Todo", Info.Settings.Theme);
            return;
        }
        else
            TaskList.List = tasklist;

        ReloadTasks();
        await ShowMessageAsync("Successfully backed up your list.", "GGHS Todo", Info.Settings.Theme);
    }
}
