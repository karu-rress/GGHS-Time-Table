#nullable enable

using System;
using System.Collections;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.ApplicationModel.Core;
using static RollingRess.StaticClass;
using GGHS.Grade2.Semester2;

namespace TimeTableUWP
{

    public sealed partial class MainPage : Page
    {
        void InitializeUI()
        {
            Disable(classComboBox, langComboBox, special1ComboBox, special2ComboBox, scienceComboBox);
            RequestedTheme = SettingsPage.IsDarkMode ? ElementTheme.Dark : ElementTheme.Light;
            SetColor();
            if (comboBoxSelection.grade is not -1)
            {
                (gradeComboBox.SelectedIndex, classComboBox.SelectedIndex, langComboBox.SelectedIndex, special1ComboBox.SelectedIndex,
                    special2ComboBox.SelectedIndex, scienceComboBox.SelectedIndex) = comboBoxSelection;
            }
        }

        private void SetColor()
        {
            foreach (var border in new[] { monBorder, tueBorder, wedBorder, thuBorder, friBorder })
                border.Background = new SolidColorBrush(SaveData.ColorType);

            foreach (var comboBox in ComboBoxes)
                comboBox.BorderBrush = new SolidColorBrush(SaveData.ColorType);
        }

        private async void ShowMessage(string context, string title = "")
        {
            ContentDialog contentDialog = new()
            {
                Title = title,
                Content = context,
                CloseButtonText = "OK",
            };
            await contentDialog.ShowAsync();
        }

        private void DrawTimeTable()
        {
            timeTable.ResetByClass(@class);
            string[,] subjectTable = SetArrayByClass();

            // 월 6, 7 / 금 5, 6은 어차피 창체, 금 7도 어차피 홈커밍
            AssignButtonsByTable(subjectTable);
            mon6Button.Content = mon7Button.Content = fri5Button.Content = fri6Button.Content = Subjects.CellName.Others;
            fri7Button.Content = Subjects.CellName.HomeComing;
        }

        private void AssignButtonsByTable(string[,] subjectTable)
        {
            var subjects = ((IEnumerable)subjectTable).Cast<string>();
            var lists = Buttons.Zip(subjects, (Button btn, string subject) => (btn, subject));
            foreach (var (btn, subject) in lists)
                btn.Content = subject;
        }

        private async Task LoopTimeAsync()
        {
            await Task.Run(() => LoopTime());
        }

        private void LoopTime()
        {
            (int day, int time) pos;
            while (true)
            {
                Thread.Sleep(100);
                now = DateTime.Now;
                _ = CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    clock.Text = now.ToString(SettingsPage.Use24Hour ? "HH:mm" : "hh:mm");
                    amorpmBox.Text = SettingsPage.Use24Hour ? string.Empty : now.ToString("tt", CultureInfo.InvariantCulture);
                    dateBlock.Text = now.ToString(SettingsPage.DateFormat switch
                    {
                        DateType.MMDDYYYY => "MM/dd/yyyy",
                        DateType.YYYYMMDD => "yyyy/MM/dd",
                        DateType.YYYYMMDD2 => "yyyy-MM-dd",
                        _ => throw new NotImplementedException()
                    });
                    dayBlock.Text = now.ToString("ddd", CultureInfo.CreateSpecificCulture("en-US"));
                });

                if (now.DayOfWeek is DayOfWeek.Sunday or DayOfWeek.Saturday ||
                now.Hour is >= 17 or < 9 or 13)
                {
                    continue;
                }

                pos.day = (int)now.DayOfWeek;
                pos.time = now.Hour switch
                {
                    9 or 10 or 11 or 12 => now.Hour - 8,
                    14 or 15 or 16 => now.Hour - 9,
                    _ => throw new DataAccessException($"Hour is not in 9, 10, 11, 12, 14, 15, 16. given {now.Hour}.")
                };

                DispatchedHandler ChangeColor = delegate()
                {
                    foreach (var item in Buttons)
                    {
                        (item.Background, item.Foreground) = (new SolidColorBrush(Color.FromArgb(0xEE, 0xF4, 0xF4, 0xF4)), new SolidColorBrush(Colors.Black));
                    }
                    var brush = new SolidColorBrush(SaveData.ColorType);
                    Buttons.ElementAt((7 * (pos.day - 1)) + (pos.time - 1)).Background = brush;
                    if (pos.time is <= 6)
                    {
                        Buttons.ElementAt((7 * (pos.day - 1)) + pos.time).Foreground = brush;
                    }
                };
                _ = CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, ChangeColor);

                // 토스트 Notification도 고려해볼 것.
            }
        }
    }
}