#nullable enable

using Windows.UI.Xaml.Media.Animation;
using TimeTableUWP.Todo;

namespace TimeTableUWP.Pages;

public sealed partial class AddPage : Page
{
    public static TodoTask? Task { get; set; } = null;

    public List<string> Subjects { get; } = new() // Don't make this as static
    {
        "동아시아사",
        "한국지리",
        "사회/문화",
        "스페인어권 문화",
        "일본문화",
        "중국문화",
        "사회 탐구 방법",
        "한국 사회의 이해",
        "세계 문제와 미래사회",
        "윤리학 연습",

        "논리적 글쓰기",
        "독서와 의사소통",
        "전통 예술과 사상",
        "심화 영어 독해Ⅱ",
        "통계로 바라보는 국제 문제",
        "체육",
        "기타"
    };

    public AddPage()
    {
        InitializeComponent();
        RequestedTheme = Info.Settings.Theme;

        DueDatePicker.MinYear = DateTimeOffset.Now;
        DueDatePicker.MaxYear = DateTimeOffset.Now.AddYears(2);

        SubjectPicker.ItemsSource = new List<string>()
        {
            Social.Selected.FullName,
            ttc::Language.Selected.FullName,
            Global1.Selected.FullName,
            Global2.Selected.FullName,
            "논리적 글쓰기",
            "독서와 의사소통",
            "전통 예술과 사상",
            "심화 영어 독해Ⅱ",
            "통계로 바라보는 국제 문제",
            "체육",
            "기타"
        };
        SaveButton.BorderBrush = Info.Settings.Brush;

        if (Task is not null) // Not creating, but modifying
        {
            DeleteButton.Visibility = Visibility.Visible;
            mainText.Text = "Modify Task";
            DueDatePicker.Date = Task.DueDate;
            SubjectPicker.SelectedItem = Task.Subject;
            TitleTextBox.Text = Task.Title;
            BodyTextBox.Text = Task.Body ?? string.Empty;
        }
    }

    private async void SaveButton_Click(object sender, RoutedEventArgs e)
    {
        if (DueDatePicker.SelectedDate is null || SubjectPicker.SelectedIndex is -1 || TitleTextBox.IsNullOrWhiteSpace())
        {
            await ShowMessageAsync("Date, subject and title are required.", "Error", Info.Settings.Theme);
            return;
        }

        DateTime date = DueDatePicker.Date.DateTime;
        TodoTask task = new(new(date.Year, date.Month, date.Day), SubjectPicker.GetSelectedString(), TitleTextBox.Text,
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
                || SubjectPicker.GetSelectedString() != Task.Subject
                || TitleTextBox.Text != Task.Title
                || (!BodyTextBox.IsSameWith(Task.Body)));
}
