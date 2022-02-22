#nullable enable

using System;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using RollingRess;
using TimeTableUWP.Todo;

namespace TimeTableUWP.Pages
{
    public sealed partial class AddPage : Page
    {
        public static TodoTask? Task { get; set; } = null;

        public List<string> Subjects { get; } = new() // Don't make this as static
        {
            "언어와 매체",
            "화법과 작문",
            "확률과 통계",
            "미적분",
            "영미 문학 읽기",
            "동아시아사",
            "한국지리",
            "사회/문화",
            "체육",
            "스페인어권 문화",
            "일본문화",
            "중국문화",
            "심화영어Ⅱ",
            "독서와 의사소통",
            "사회 탐구 방법",
            "한국 사회의 이해",
            "세계 문제와 미래사회",
            "윤리학 연습",
            "기타"
        };

        

        public AddPage()
        {
            InitializeComponent();
            RequestedTheme = Info.Settings.Theme;

            DueDatePicker.MinYear = DateTimeOffset.Now;
            DueDatePicker.MaxYear = DateTimeOffset.Now.AddYears(2);

            SubjectPicker.ItemsSource = Subjects;

            if (Task is not null) // Not creating, but modifying
            {
                DeleteButton.Visibility = Visibility.Visible;
                mainText.Text = "Modify Task";
                DueDatePicker.Date = Task.DueDate;
                SubjectPicker.SelectedItem = Task.Subject;
                TitleTextBox.Text = Task.Title;
                if (Task.Body is not null)
                {
                    BodyTextBox.Text = Task.Body;
                }
            }
        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (DueDatePicker.SelectedDate is null || SubjectPicker.SelectedIndex is -1 || TitleTextBox.IsNullOrWhiteSpace())
            {
                ContentMessageDialog content = new("Date, subject and title are required.", "Error");
                await content.ShowAsync();
                return;
            }

            DateTime date = DueDatePicker.Date.DateTime;
            TodoTask task = new(new(date.Year, date.Month, date.Day), SubjectPicker.GetSelectedString(), TitleTextBox.Text,
                string.IsNullOrWhiteSpace(BodyTextBox.Text) || BodyTextBox.IsNullOrWhiteSpace() ? null : BodyTextBox.Text);

            if (Task is not null)
            {
                TodoListPage.TaskList[TodoListPage.TaskList.FindIndex(x => x == Task)] = task;
            }
            else
            {
                TodoListPage.TaskList.Add(task);
            }
            TodoListPage.TaskList.Sort();
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e) => Close();

        private void Close() { Task = null; Frame.Navigate(typeof(TodoListPage), null, new DrillInNavigationTransitionInfo()); }

        private async void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (Modified)
            {
                ContentMessageDialog content = new("This task has been modified. Save or discard changes and try again.", "Couldn't delete");
                _ = await content.ShowAsync();
                return;
            }

            if (await TaskList.DeleteTask(TitleTextBox.Text, Task!) is true)
            {
                Close();
            }
        }

        private bool Modified => Task is null ? false
                    : DueDatePicker.Date.DateTime != Task.DueDate
                    || SubjectPicker.GetSelectedString() != Task.Subject
                    || TitleTextBox.Text != Task.Title
                    || (BodyTextBox.IsNullOrWhiteSpace() ? null : BodyTextBox.Text) != Task.Body;
    }
}
