#nullable enable

using Windows.UI.Xaml.Media.Animation;
using TimeTableUWP.Todo;

namespace TimeTableUWP.Pages;

public sealed partial class AddPage : Page
{
    public static TodoTask? Task { get; set; } = null;

    public AddPage()
    {
        InitializeComponent();
        RequestedTheme = Info.Settings.Theme;

        DueDatePicker.MinYear = DateTimeOffset.Now;
        DueDatePicker.MaxYear = DateTimeOffset.Now.AddYears(2);

        SaveButton.BorderBrush = Info.Settings.Brush;

        if (Task is not null) // Not creating, but modifying
        {
            DeleteButton.Visibility = Visibility.Visible;
            mainText.Text = "Modify Task";
            DueDatePicker.Date = Task.DueDate;
            TitleTextBox.Text = Task.Title;
            BodyTextBox.Text = Task.Body ?? string.Empty;
        }
    }

    private async void SaveButton_Click(object sender, RoutedEventArgs e)
    {
        if (DueDatePicker.SelectedDate is null || TitleTextBox.IsNullOrWhiteSpace())
        {
            await ShowMessageAsync("Date and title are required.", "Error", Info.Settings.Theme);
            return;
        }

        DateTime date = DueDatePicker.Date.DateTime;
        TodoTask task = new(new(date.Year, date.Month, date.Day), TitleTextBox.Text,
            BodyTextBox.IsNullOrWhiteSpace() ? null : BodyTextBox.Text);

        if (Task is not null)
            TodoListPage.TaskList[TodoListPage.TaskList.FindIndex(x => x == Task)] = task;
        else
            TodoListPage.TaskList.Add(task);

        TodoListPage.TaskList.Sort();
        Close();
    }

    private void CancelButton_Click(object sender, RoutedEventArgs e) => Close();

    private void Close() { Task = null; Frame.Navigate(typeof(TodoListPage), null, new DrillInNavigationTransitionInfo()); }

    private async void DeleteButton_Click(object sender, RoutedEventArgs e)
    {
        if (Modified)
        {
            await ShowMessageAsync("This task has been modified. Save or discard changes and try again.", "Couldn't delete", Info.Settings.Theme);
            return;
        }

        if (await TaskList.DeleteTask(TitleTextBox.Text, Task!) is true)
            Close();
    }

    private bool Modified => Task is not null && (DueDatePicker.Date.DateTime != Task.DueDate
                || TitleTextBox.Text != Task.Title
                || (!BodyTextBox.IsSameWith(Task.Body)));
}
