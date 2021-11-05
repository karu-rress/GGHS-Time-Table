#nullable enable

using System;
using System.Collections;
using System.Globalization;
using System.Threading.Tasks;
using System.Linq;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.ApplicationModel.Core;
using static RollingRess.StaticClass;
using static System.DayOfWeek;
using GGHS.Grade2.Semester2;
using Microsoft.Toolkit.Uwp.Notifications;
using Windows.ApplicationModel.Background;
using System.Collections.Generic;

namespace TimeTableUWP.Pages
{
    public sealed partial class TimeTablePage : Page
    {
        private string[,]? SubjectTable { get; set; } // string[5, 7]

        void InitializeUI()
        {
            // Set Color
            foreach (var border in new[] { monBorder, tueBorder, wedBorder, thuBorder, friBorder })
                border.Background = new SolidColorBrush(SaveData.ColorType);

            SetSubText();

            if (Status is LoadStatus.NewUser)
            {
                _ = ShowMessageAsync(Messages.Welcome, Messages.GGHSTimeTableWithVer, MainPage.Theme);
            }
            else if (Status is LoadStatus.Updated)
            {
                _ = ShowMessageAsync(Messages.Updated, "New version installed");
            }
            Status = LoadStatus.Default;
        }

        private void SetSubText() => mainText2.Text = SaveData.ActivateStatus switch
        {
            _ => "Welcome back, with the task manager"
            /*
            ActivateLevel.Developer => "Welcome, Karu",
            ActivateLevel.Grade2 => "We're the ones who've made it this far",
            ActivateLevel.Insider => "GTT3 Insider Preview",
            _ => string.Empty
            */
        };

        private void DrawTimeTable()
        {
            TimeTable.ResetByClass(@class);
            SubjectTable = SetArrayByClass();

            // 월 6, 7 / 금 5, 6은 어차피 창체, 금 7도 어차피 홈커밍
            AssignButtonsByTable(SubjectTable);

            mon6Button.Content = mon7Button.Content = fri5Button.Content = fri6Button.Content =
            SubjectTable[0, 5] = SubjectTable[0, 6] = SubjectTable[4, 4] = SubjectTable[4, 5] = Subjects.CellName.Others;
            fri7Button.Content = SubjectTable[4, 6] = Subjects.CellName.HomeComing;
        }

        private void AssignButtonsByTable(string[,] subjectTable)
        {
            IEnumerable<string>? subjects = ((IEnumerable)subjectTable).Cast<string>();
            var lists = Buttons.Zip(subjects, (Button btn, string subject) => (btn, subject));
            foreach (var (btn, subject) in lists)
                btn.Content = subject;
        }

        private async Task LoopTimeAsync()
        {
            (int day, int time) pos;
            bool invoke = true;

            while (true)
            {
                try
                {
                    await Task.Delay(300); // 300ms 마다 반복하기
                    Now = DateTime.Now;

                    void SetClock()
                    {
                        clock.Text = Now.ToString(SettingsPage.Use24Hour ? "HH:mm" : "hh:mm");
                        amorpmBox.Text = SettingsPage.Use24Hour
                            ? string.Empty : Now.ToString("tt", CultureInfo.InvariantCulture);
                        dateBlock.Text = Now.ToString(SettingsPage.DateFormat switch
                        {
                            DateType.MMDDYYYY => "MM/dd/yyyy",
                            DateType.YYYYMMDD => "yyyy/MM/dd",
                            DateType.YYYYMMDD2 => "yyyy-MM-dd",
                            _ => throw new NotImplementedException()
                        });
                        dayBlock.Text = Now.ToString("ddd", CultureInfo.CreateSpecificCulture("en-US"));
                    }
                    await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, SetClock);

                    void RefreshColor()
                    {
                        foreach (var item in Buttons)
                        {
                            item.RequestedTheme = SettingsPage.IsDarkMode ? ElementTheme.Dark : ElementTheme.Light;
                            item.Background = new SolidColorBrush(SettingsPage.IsDarkMode
                                ? Color.FromArgb(0xEE, 0x34, 0x34, 0x34)
                                : Color.FromArgb(0xEE, 0xF4, 0xF4, 0xF4));
                            item.Foreground = new SolidColorBrush(SettingsPage.IsDarkMode ? Colors.White : Colors.Black);
                        }
                    }
                    await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, RefreshColor);


                    if (Now.DayOfWeek is Sunday or Saturday || Now.Hour is >= 17 or < 9 or 13)
                        continue;

                    pos.day = (int)Now.DayOfWeek;
                    pos.time = Now.Hour switch
                    {
                        9 or 10 or 11 or 12 => Now.Hour - 8,
                        14 or 15 or 16 => Now.Hour - 9,
                        _ => throw new DataAccessException($"Hour is not in 9, 10, 11, 12, 14, 15, 16. given {Now.Hour}.")
                    };

                    void ChangeCellColor()
                    {
                        var brush = new SolidColorBrush(SaveData.ColorType);
                        Buttons.ElementAt((7 * (pos.day - 1)) + (pos.time - 1)).Background = brush;
                        if (pos.time <= 6)
                            Buttons.ElementAt((7 * (pos.day - 1)) + pos.time).Foreground = brush;
                    }
                    await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, ChangeCellColor);


                    //
                    // 토스트 메시지 출력
                    //
                    //

                    // GTT 4부터 모든 사용자에게 3분전 알림 개방
                    // if (SaveData.IsNotDeveloperOrInsider) continue;

                    // 동아리(2)나 홈커밍일 때는 토스트 알림 없음.
                    if (pos is ((int)Friday, 7) or ((int)Friday, 6)) continue;

                    // 4시에는 실행하면 안 된다!
                    if (Now.Hour is 16) continue;

                    Now = DateTime.Now;
                    if (Now.Minute is 57 && invoke) // second is 0
                    {
                        await SendToast(); // 여기까진 알고리즘 완벽.
                        invoke = false;
                    }
                    if (Now.Minute is 58 && invoke is false)
                    {
                        invoke = true; // 알람 폭탄 방지
                    }

                    async Task SendToast()
                    {
                        int hour = Now.Hour + 1; // 현재의 다음 시간이니까 +1

                        // TODO: 이때는 뭘 하지 버튼 없는 토스트?
                        if (pos is ((int)Monday, 6) or ((int)Monday, 7) or ((int)Friday, 5))
                            return;

                        // TimeTable 표에서 현재 날짜와 시간을 통해 과목 꺼내기. time은 하나 +1 시켜야 함.
                        string? subject = SubjectTable?[pos.day - 1, pos.time];
                        if (subject is null)
                            return;

                        ToastContentBuilder toast = new ToastContentBuilder()
                            .AddAppLogoOverride(new Uri("ms-appx:///Assets/SecurityAndMaintenance.png")) // 동그라미 i 표시
                            .AddText("다음 수업이 3분 이내에 시작됩니다.", hintMaxLines: 1); // 안내문

                        // 무음모드가 아닐 때만 알람음 설정
                        if (SettingsPage.SilentMode is false)
                        {
                            toast.AddAudio(new Uri("ms-appx:///Assets/Alarm01.wav")); // 알람소리
                        }

                        if (GetClassZoomLink().TryGetValue(subject, out var zoomInfo) is false || (zoomInfo is null))
                        {
                            toast.AddText($"[{subject}]")
                                 .AddText(hour > 12 ?
                                $"{hour - 12}:00 PM - {hour - 12}:50 PM" :
                                $"{hour}:00 AM - {hour}:50 AM").Show();
                            return;
                        }

                        toast.AddText($"[{subject}] - {zoomInfo.Teacher} 선생님") // 과목 및 선생님
                            .AddText(hour > 12 ?
                                $"{hour - 12}:00 PM - {hour - 12}:50 PM" :
                                $"{hour}:00 AM - {hour}:50 AM");

                        if (zoomInfo.Link is not null)
                            toast.AddButton(new ToastButton()
                                .SetContent("ZOOM 열기")
                                .AddArgument("action", "zoom")
                                .AddArgument("zoomUrl", zoomInfo.Link)
                                .SetBackgroundActivation());

                        if (zoomInfo.ClassRoom is not null)
                            toast.AddButton(new ToastButton()
                                .SetContent("클래스룸 열기")
                                .AddArgument("action", "classRoom")
                                .AddArgument("classRoomUrl", zoomInfo.ClassRoom)
                                .SetBackgroundActivation());

                        toast.Show();

                        const string taskName = "ToastZoomOpen";

                        if (BackgroundTaskRegistration.AllTasks.Any(i => i.Value.Name.Equals(taskName)))
                            return;

                        BackgroundAccessStatus status = await BackgroundExecutionManager.RequestAccessAsync();
                        BackgroundTaskBuilder builder = new BackgroundTaskBuilder()
                        {
                            Name = taskName
                        };
                        builder.SetTrigger(new ToastNotificationActionTrigger());
                        BackgroundTaskRegistration registration = builder.Register();
                    }
                }
                catch (Exception e)
                {
                    await TimeTableException.HandleException(e);
                }
            }

            
        }
    }
}