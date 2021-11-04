#nullable enable

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;


using Microsoft.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using RollingRess;
using System.Threading.Tasks;
using TimeTableUWP.Todo;
// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace TimeTableUWP.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AddPage : Page
    {
        public static TodoTask? Task { get; set; } = null;

        public List<string> Subjects { get; } = new()
        {
            "독서",
            "수학Ⅱ",
            "수학과제탐구",
            "과학사",
            "생활과 과학",
            "운동과 건강",
            "창의적문제해결기법",
            "스페인어Ⅰ",
            "중국어Ⅰ",
            "일본어Ⅰ",
            "심화영어Ⅰ",
            "국제경제",
            "국제정치",
            "비교문화",
            "동양근대사",
            "세계 역사와 문화",
            "현대정치철학의 이해",
            "세계 지역 연구",
            "공간 정보와 공간 분석",
            "기타"
        };

        public AddPage()
        {
            InitializeComponent();
            RequestedTheme = MainPage.Theme;

            DueDatePicker.MinYear = DateTimeOffset.Now;
            DueDatePicker.MaxYear = DateTimeOffset.Now.AddYears(2);
            SubjectPicker.ItemsSource = Subjects;

            if (Task is not null) // Clicked a button
            {
                DeleteButton.Visibility = Visibility.Visible;
                mainText.Text = "Modify Task";
                DueDatePicker.Date = Task.DueDate;
                SubjectPicker.SelectedItem = Task.Subject;
                TitleTextBox.Text = Task.Title;
                if (Task.Body is not null)
                    BodyTextBox.Text = Task.Body;
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
                TodoPage.TaskList[TodoPage.TaskList.FindIndex(x => x == Task)] = task;
            }
            else
            {
                TodoPage.TaskList.Add(task);
            }
            TodoPage.TaskList.Sort();
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e) => Close();

        private void Close() { Task = null; Frame.Navigate(typeof(TodoPage), null, new DrillInNavigationTransitionInfo()); }

        private async void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (Modified)
            {
                ContentMessageDialog content = new("This task has been modified. Save or discard changes and try again.", "Couldn't delete");
                await content.ShowAsync();
                return;
            }

            await TaskList.DeleteTask(TitleTextBox.Text, Task!);
            Close();
        }

        private bool Modified
        {
            get
            {
                if (Task is null)
                    return false;

                TodoTask task = new(DueDatePicker.Date.DateTime, SubjectPicker.GetSelectedString(), TitleTextBox.Text,
        string.IsNullOrWhiteSpace(BodyTextBox.Text) || BodyTextBox.IsNullOrWhiteSpace() ? null : BodyTextBox.Text);
                return task != Task;
            }
        }
    }
}
