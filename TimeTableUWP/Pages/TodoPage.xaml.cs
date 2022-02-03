#nullable enable

using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.ApplicationModel;
using RollingRess;
using Windows.UI.Xaml.Media.Animation;
using TimeTableUWP.Todo;
using System.Threading.Tasks;



namespace TimeTableUWP.Pages
{
    public enum Grades
    {
        Grade1,
        Grade2,
        Grade3,
        None,
    }

    public sealed partial class TodoPage : Page
    {
        public static TaskList TaskList { get; set; } = new();

        public static Grades Grade { get; set; } = Grades.None;

        public TodoPage()
        {
            InitializeComponent();
            RequestedTheme = MainPage.Theme;
            LoadTasks();
        }

        /// <summary>
        /// Adds task buttons to the grid.
        /// </summary>
        private void LoadTasks()
        {
            if (TaskList.IsNullOrEmpty)
                return;

            int buttons = 0;
            foreach (var task in TaskList)
            {
                TaskGrid.Children.Add(new TaskButton(task, TaskButton_Click, buttons++));
            }
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
                await NothingToDelete();
                return;
            }

            int cnt = TaskList.CountAll(match);
            if (cnt is 0)
            {
                await NothingToDelete();
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
                RequestedTheme = MainPage.Theme,
            };
            if (await contentDialog.ShowAsync() is ContentDialogResult.None)
                return;

            TaskList.RemoveAll(match);

            ReloadTasks();
            contentDialog = new ContentMessageDialog($"Successfully deleted {cnt} {"task".PutS(cnt)}.", title, "Close");
            await contentDialog.ShowAsync();

            static async Task NothingToDelete()
            {
                ContentMessageDialog message = new("Nothing to delete.", "Delete");
                await message.ShowAsync();
            }
        }

        private async void DeletePastButton_Click(object _, RoutedEventArgs e)
            => await DeleteTasks(x => x.DueDate.Date < DateTime.Now.Date);

        private async void DeleteAllButton_Click(object _, RoutedEventArgs e) => await DeleteTasks(null);

        private async void SelectDate_Click(object _, RoutedEventArgs e)
        {
            GGHS_Todo.DateSelectDialog dialog = new();
            if (await dialog.ShowAsync() is ContentDialogResult.None)
                return;

            var date = dialog.SelectedDate;
            await DeleteTasks(x => x.DueDate == date);
        }

        private async void SelectSubject_Click(object _, RoutedEventArgs e)
        {
            GGHS_Todo.SubjectSelectDialog dialog = new();
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
                var message = new ContentMessageDialog("Nothing to restore.", "Undo Delete", theme: MainPage.Theme);
                await message.ShowAsync();
                return;
            }
            ReloadTasks();
            ContentMessageDialog msg = new($"Successfully restored {result} {"item".PutS(result)}.", "Undo Delete");
            await msg.ShowAsync();
        }
    }
}