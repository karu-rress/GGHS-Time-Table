using System;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using TimeTableUWP.ComboboxItem;
using Windows.ApplicationModel.Core;
using Windows.UI.Xaml.Documents;
using GGHS;
using GGHS.Grade2;
using RollingRess;
using static RollingRess.Librarys;
using System.Collections.Generic;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409


/// <TODO>
/// 
/// 
/// BORDER BRUSH: 버튼 이거를 화이트로 강조할 것
/// 
// 
/// foreground가 글자색
/// 
/// </TODO>

namespace TimeTableUWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    /// 


    public sealed partial class MainPage : Page
    {
        private int grade;
        private int @class = 8;
        private bool use24hour = false;

        DateType dateType = DateType.MMDDYYYY;
        DateTime now = DateTime.Now;

        public MainPage()
        {
            InitializeComponent();
            _ = LoopTimeAsync();
            Disable(classComboBox, langComboBox, s1comboBox, s2comboBox, scComboBox);
            _ = LoadDataFromFileAsync();
        }

        private async Task LoadDataFromFileAsync()
        {
            if (await SaveData.LoadDataAsync() is true)
            {
                SaveData.SetGradeAndClass(ref grade, ref @class);
                SetComboBoxAsClass();
                SaveData.SetComboBoxes(
                    ref gradeComboBox,
                    ref classComboBox,
                    ref s1comboBox,
                    ref s2comboBox,
                    ref langComboBox,
                    ref scComboBox
                    );
            }
        }

        private async void ShowMessage(string context, string title = "")
        => await new MessageDialog(context) { Title = title }.ShowAsync();

        private async Task LoopTimeAsync() => await Task.Factory.StartNew(() =>
        {
            (int day, int time) pos;
            Button[,] table =
                {
                    { mon1Button, mon2Button, mon3Button,mon4Button,mon5Button,mon6Button,mon7Button},
                    { tue1Button, tue2Button, tue3Button, tue4Button, tue5Button, tue6Button, tue7Button, },
                    { wed1Button, wed2Button, wed3Button, wed4Button, wed5Button, wed6Button, wed7Button},
                    { thu1Button, thu2Button, thu3Button, thu4Button, thu5Button, thu6Button, thu7Button },
                    { fri1Button, fri2Button, fri3Button, fri4Button, fri5Button, fri6Button, fri7Button, }
                };
            while (true)
            {
                Thread.Sleep(200);
                now = DateTime.Now;
                _ = CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    clock.Text = now.ToString(use24hour ? "HH:mm" : "hh:mm");
                    amorpmBox.Text = use24hour ? string.Empty : now.ToString("tt", CultureInfo.InvariantCulture);
                    dateBlock.Text = now.ToString(dateType switch
                    {
                        DateType.MMDDYYYY => "MM/dd/yyyy",
                        DateType.YYYYMMDD => "yyyy/MM/dd",
                        DateType.YYYYMMDD2 => "yyyy-MM-dd",
                        _ => throw new NotImplementedException()
                    });
                    dayBlock.Text = now.ToString("ddd", CultureInfo.CreateSpecificCulture("en-US"));
                }
                );

                if (now.DayOfWeek is DayOfWeek.Sunday or DayOfWeek.Saturday ||
                    now.Hour is >= 17 or < 9 or 13)
                {
                    continue;
                }

                pos.day = now.DayOfWeek switch
                {
                    DayOfWeek.Monday => 1,
                    DayOfWeek.Tuesday => 2,
                    DayOfWeek.Wednesday => 3,
                    DayOfWeek.Thursday => 4,
                    DayOfWeek.Friday => 5,
                    _ => throw new IndexOutOfRangeException()
                };

                pos.time = now.Hour switch
                {
                    9 => 1,
                    10 => 2,
                    11 => 3,
                    12 => 4,
                    14 => 5,
                    15 => 6,
                    16 => 7,
                    _ => throw new IndexOutOfRangeException()
                };

                _ = CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
                () => {
                    foreach (var item in table)
                    {
                            item.Background = new SolidColorBrush(Colors.Black);
                            item.Foreground = new SolidColorBrush(Colors.White);
                    }
                    table[pos.day - 1, pos.time - 1].Background =
                    new SolidColorBrush(Color.FromArgb(0xFF, 0x6D, 0x6D, 0xBD));
                    if (pos.time is <= 6) {
                        table[pos.day - 1, pos.time].Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0x6D, 0x6D, 0xBD));
                    }
                    });
            }
        });

        private void DrawTimeTable()
        {
            TimeTables.ResetByClass(@class);
            string[,] table = SetArrayByClass();

            // 월 6, 7 / 금 5, 6은 어차피 창체, 금 7도 어차피 홈커밍
            mon6Button.Content = mon7Button.Content = fri5Button.Content = fri6Button.Content = Subjects.CellName.Others;
            fri7Button.Content = Subjects.CellName.HomeComing;
            AssignButtonsByTable(table);
        }

        private void AssignButtonsByTable(string[,] table)
        {
            (mon1Button.Content, mon2Button.Content, mon3Button.Content, mon4Button.Content, mon5Button.Content) =
                            (table[0, 0], table[0, 1], table[0, 2], table[0, 3], table[0, 4]);

            (tue1Button.Content, tue2Button.Content, tue3Button.Content, tue4Button.Content, tue5Button.Content, tue6Button.Content, tue7Button.Content) =
                (table[1, 0], table[1, 1], table[1, 2], table[1, 3], table[1, 4], table[1, 5], table[1, 6]);

            (wed1Button.Content, wed2Button.Content, wed3Button.Content, wed4Button.Content, wed5Button.Content, wed6Button.Content, wed7Button.Content) =
                (table[2, 0], table[2, 1], table[2, 2], table[2, 3], table[2, 4], table[2, 5], table[2, 6]);

            (thu1Button.Content, thu2Button.Content, thu3Button.Content, thu4Button.Content, thu5Button.Content, thu6Button.Content, thu7Button.Content) =
                (table[3, 0], table[3, 1], table[3, 2], table[3, 3], table[3, 4], table[3, 5], table[3, 6]);

            (fri1Button.Content, fri2Button.Content, fri3Button.Content, fri4Button.Content) =
                (table[4, 0], table[4, 1], table[4, 2], table[4, 3]);
        }

        private string[,] SetArrayByClass() => @class switch
        {
            1 => TimeTables.Class1.Clone() as string[,],
            2 => TimeTables.Class2.Clone() as string[,],
            3 => TimeTables.Class3.Clone() as string[,],
            4 => TimeTables.Class4.Clone() as string[,],
            5 => TimeTables.Class5.Clone() as string[,],
            6 => TimeTables.Class6.Clone() as string[,],
            7 => TimeTables.Class7.Clone() as string[,],
            8 => TimeTables.Class8.Clone() as string[,],
            _ => throw new Exception()
        };

        #region ComboBox
        private void EnableAllCombobox()
        => Librarys.Enable(langComboBox, s1comboBox, s2comboBox, scComboBox);

        private void gradeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // EnableAllCombobox();
            if (gradeComboBox.SelectedItem is null)
            {
                return;
            }

            SaveData.GradeComboBoxText = gradeComboBox.SelectedItem as string;      
            grade = SaveData.GradeComboBoxText[6] - '0';
            if (grade is 2)
            {
                Librarys.Enable(classComboBox);
            }
            else
            {
                ShowMessage($"Sorry, Grade {grade} has not been implemented yet.", MessageTitle.FeatrueNotImplemented);
                Librarys.Empty(gradeComboBox);
                Librarys.Disable(classComboBox);
            }
            // TODO: for future, empty all combobox except grade & class
        }

        private void SetComboBoxAsClass1()
        {
            s1comboBox.SelectedItem = Subjects.RawName.Ethics;
            s2comboBox.SelectedItem = Subjects.RawName.Politics;
            langComboBox.SelectedItem = Subjects.RawName.Spanish;
            scComboBox.SelectedItem = Subjects.RawName.Biology;

            Librarys.Disable(s1comboBox, s2comboBox, langComboBox, scComboBox);
        }
        private void SetComboBoxAsClass2()
        {
            s1comboBox.SelectedItem = Subjects.RawName.Ethics;
            scComboBox.SelectedItem = Subjects.RawName.Biology;

            Librarys.Disable(s1comboBox, scComboBox);
            Librarys.Empty(langComboBox, s2comboBox);
        }
        private void SetComboBoxAsClass3()
        {
            s1comboBox.SelectedItem = Subjects.RawName.Ethics;
            Librarys.Disable(s1comboBox);
            Librarys.Empty(langComboBox, scComboBox, s2comboBox);
        }
        private void SetComboBoxAsClass4()
        {
            s1comboBox.SelectedItem = Subjects.RawName.Ethics;
            langComboBox.SelectedItem = Subjects.RawName.Chinese;
            scComboBox.SelectedItem = Subjects.RawName.Biology;
            Librarys.Disable(s1comboBox, langComboBox, scComboBox);
            Librarys.Empty(s2comboBox);
        }
        private void SetComboBoxAsClass5()
        {
            langComboBox.SelectedItem = Subjects.RawName.Spanish;
            Librarys.Disable(langComboBox);
            Librarys.Empty(scComboBox, s1comboBox, s2comboBox);
        }
        private void SetComboBoxAsClass6()
        {
            s1comboBox.SelectedItem = Subjects.RawName.Environment;
            langComboBox.SelectedItem = Subjects.RawName.Spanish;
            scComboBox.SelectedItem = Subjects.RawName.Biology;
            Librarys.Disable(s1comboBox, langComboBox, scComboBox);
            Librarys.Empty(s2comboBox);
        }
        private void SetComboBoxAsClass7()
        {
            Librarys.Empty(langComboBox, scComboBox, s1comboBox, s2comboBox);
        }
        private void SetComboBoxAsClass8()
        {
            s1comboBox.SelectedItem = Subjects.RawName.Environment;
            Librarys.Disable(s1comboBox);
            Librarys.Empty(langComboBox, scComboBox, s2comboBox);
        }

        private void classComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (classComboBox.SelectedItem is null)
            {
                return;
            }
            EnableAllCombobox();
            Subjects.Clear();

            // Get & Set Class
            SaveData.ClassComboBoxText = classComboBox.SelectedItem as string;
            @class = SaveData.ClassComboBoxText[6] - '0';
            TimeTables.ResetByClass(@class);
            SetComboBoxAsClass();
            DrawTimeTable();
        }

        private void SetComboBoxAsClass()
        {
            switch (@class)
            {
                case 1:
                    SetComboBoxAsClass1();
                    return;
                case 2:
                    SetComboBoxAsClass2();
                    return;
                case 3:
                    SetComboBoxAsClass3();
                    return;
                case 4:
                    SetComboBoxAsClass4();
                    return;
                case 5:
                    SetComboBoxAsClass5();
                    return;
                case 6:
                    SetComboBoxAsClass6();
                    return;
                case 7:
                    SetComboBoxAsClass7();
                    return;
                case 8:
                    SetComboBoxAsClass8();
                    return;
            }
        }

        private void langComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (langComboBox.SelectedItem is null)
            {
                return;
            }
            SaveData.LangComboBoxText = Subjects.Languages.Selected = langComboBox.SelectedItem as string;
            DrawTimeTable();
        }

        private void s1comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (s1comboBox.SelectedItem is null)
                return;
            SaveData.SpecialComboBoxText = s1comboBox.SelectedItem as string;
            Subjects.Specials.Selected = SaveData.SpecialComboBoxText switch
            {
                Subjects.RawName.Ethics => Subjects.Specials.Ethics,
                Subjects.RawName.Environment => Subjects.Specials.Environment,
                _ => throw new Exception("ComboBox.SelectedItem returned wrong value")
            };
            DrawTimeTable();
        }

        private void s2comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (s2comboBox.SelectedItem is null)
                return;
            SaveData.SocialComboBoxText = Subjects.Socials.Selected = s2comboBox.SelectedItem as string;
            DrawTimeTable();
        }

        private void scComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (scComboBox.SelectedItem is null)
                return;
            SaveData.ScienceComboBoxText = Subjects.Sciences.Selected = scComboBox.SelectedItem as string;
            DrawTimeTable();
        }
        #endregion

        #region Button

        private void Button_Click(object sender, RoutedEventArgs e)
        => dateType = dateType switch {
                DateType.YYYYMMDD => DateType.MMDDYYYY,
                DateType.MMDDYYYY => DateType.YYYYMMDD2,
                DateType.YYYYMMDD2 => DateType.YYYYMMDD,
                _ => throw new NotImplementedException()
            };

        private void Button_Click_1(object sender, RoutedEventArgs e)
        => use24hour = !use24hour;
        #endregion

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            gradeComboBox.SelectedItem = Grade.Grade2;
            classComboBox.SelectedItem = Class.Class4;
            s2comboBox.SelectedItem = Subjects.RawName.Politics;
            DrawTimeTable();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            gradeComboBox.SelectedItem = Grade.Grade2;
            classComboBox.SelectedItem = Class.Class8;
            langComboBox.SelectedItem = Subjects.RawName.Chinese;
            s2comboBox.SelectedItem = Subjects.RawName.Economy;
            scComboBox.SelectedItem = Subjects.RawName.Chemistry;
            DrawTimeTable();
        }

        private async void Button_Click_4(object sender, RoutedEventArgs e)
        {
            Hyperlink hyperlink = new() { NavigateUri = new("https://blog.naver.com/nsun527") };
            hyperlink.Inlines.Add(new Run() { Text = "개발자 블로그 1 (네이버: 카루)" });

            Hyperlink hyperlink2 = new() { NavigateUri = new("https://rress.tistory.com") };
            hyperlink2.Inlines.Add(new Run() { Text = "개발자 블로그 2 (티스토리: Rolling Ress)" });

            TextBlock tb = new();
            tb.Inlines.Add(new Run()
            {
                Text = @"환영합니다, Rolling Ress의 카루입니다.

GGHS Time Table을 설치해주셔서 감사합니다. 가능하다면 가능한 많은 분들께
이 프로그램을 알려주세요.
기능에 문제가 있거나, 줌 링크가 누락이 된 반 혹은 과목이 있다면
인스타그램 @nsun527로 제보해주시면 감사하겠습니다.

카루 블로그 링크:
"
            });
            tb.Inlines.Add(hyperlink);
            tb.Inlines.Add(new Run() { Text = "\n" });
            tb.Inlines.Add(hyperlink2);

            ContentDialog contentDialog = new()
            {
                Title = "About GGHS Time Table",
                Content = tb,
                PrimaryButtonText = "Open Naver Blog",
                SecondaryButtonText = "Open Tistory",
                CloseButtonText = "Close",
                DefaultButton = ContentDialogButton.Primary,
            };
            var selection = await contentDialog.ShowAsync();

            if (selection is ContentDialogResult.Primary)
            {
                await Windows.System.Launcher.LaunchUriAsync(new("https://blog.naver.com/nsun527"));
            }
            if (selection is ContentDialogResult.Secondary)
            {
                await Windows.System.Launcher.LaunchUriAsync(new("https://rress.tistory.com"));
            }
        }

        private async Task ShowSubjectZoom(string subjectCellName)
        {
            if (subjectCellName is null)
            {
                ShowMessage("Please select your grade and class first.", "Error");
                return;
            }
            if (@class is not (3 or 4 or 5 or 6 or 8))
            {
                ShowMessage($"Sorry, displaying Zoom link is not available in class {@class}.\n" + "개발자에게 줌 링크 추가를 요청해보세요.", MessageTitle.FeatrueNotImplemented);
                return;
            }

            if (SaveData.IsActivated is false)
            {
                ActivateDialog activateDialog = new();
                var activeSelection = await activateDialog.ShowAsync();

                // 인증을 하지 않았다면 return
                if (activeSelection is not ContentDialogResult.Primary || SaveData.IsActivated is false)
                {
                    return;
                }
            }

            // TODO: Activate Dialog 개발자, 3학년, 2학년, 1학년 따라 나누는 것도 해야 함.

            if (GetClassZoomLink().TryGetValue(subjectCellName, out var zoomInfo) is false || (zoomInfo is null))
            {
                ShowMessage($"Zoom Link for {subjectCellName} is not available.\n" + "개발자에게 줌 링크 추가를 요청해보세요.", "No Data for Zoom Link");
                return;
            }

            TextBlock tb = new();
            Hyperlink hyperlink = new();
            hyperlink.NavigateUri = new(zoomInfo.Link);
            hyperlink.Inlines.Add(new Run() { Text = zoomInfo.Link });
            //TODO: 링크가 잘림. split?
            tb.Inlines.Add(new Run() { Text = "LINK: " });
            tb.Inlines.Add(hyperlink);
            tb.Inlines.Add(new Run() { Text = @$"

ID: {zoomInfo.Id}
PW: {zoomInfo.Pw}
Teacher: {zoomInfo.Teacher} 선생님

Click 'Open Zoom Meeting' or the link above to join the zoom." });
            ContentDialog contentDialog = new()
            {
                Title = $"{classComboBox.SelectedItem as string} {subjectCellName} Zoom Link",
                Content = tb,
                PrimaryButtonText = "Open Zoom Meeting",
                CloseButtonText = "Close",
                DefaultButton = ContentDialogButton.Primary,
            };
            var selection = await contentDialog.ShowAsync();

            if (selection is ContentDialogResult.Primary)
            {
                await Windows.System.Launcher.LaunchUriAsync(new(zoomInfo.Link));
            }
        }

        private Dictionary<string, ZoomLinks.ZoomInfo> GetClassZoomLink()
        {
            return @class switch
            {
                3 => ZoomLinks.Class3,
                4 => ZoomLinks.Class4,
                5 => ZoomLinks.Class5,
                6 => ZoomLinks.Class6,
                8 => ZoomLinks.Class8,
                _ => throw new NotImplementedException()
            };
        }

        #region Cell Buttons
        private void mon1Button_Click(object sender, RoutedEventArgs e) => _ = ShowSubjectZoom(mon1Button.Content as string);
        private void tue1Button_Click(object sender, RoutedEventArgs e) => _ = ShowSubjectZoom(tue1Button.Content as string);
        private void wed1Button_Click(object sender, RoutedEventArgs e) => _ = ShowSubjectZoom(wed1Button.Content as string);
        private void thu1Button_Click(object sender, RoutedEventArgs e) => _ = ShowSubjectZoom(thu1Button.Content as string);
        private void fri1Button_Click(object sender, RoutedEventArgs e) => _ = ShowSubjectZoom(fri1Button.Content as string);
        private void mon2Button_Click(object sender, RoutedEventArgs e) => _ = ShowSubjectZoom(mon2Button.Content as string);
        private void tue2Button_Click(object sender, RoutedEventArgs e) => _ = ShowSubjectZoom(tue2Button.Content as string);
        private void wed2Button_Click(object sender, RoutedEventArgs e) => _ = ShowSubjectZoom(wed2Button.Content as string);
        private void thu2Button_Click(object sender, RoutedEventArgs e) => _ = ShowSubjectZoom(thu2Button.Content as string);
        private void fri2Button_Click(object sender, RoutedEventArgs e) => _ = ShowSubjectZoom(fri2Button.Content as string);
        private void mon3Button_Click(object sender, RoutedEventArgs e) => _ = ShowSubjectZoom(mon3Button.Content as string);
        private void tue3Button_Click(object sender, RoutedEventArgs e) => _ = ShowSubjectZoom(tue3Button.Content as string);
        private void wed3Button_Click(object sender, RoutedEventArgs e) => _ = ShowSubjectZoom(wed3Button.Content as string);
        private void thu3Button_Click(object sender, RoutedEventArgs e) => _ = ShowSubjectZoom(thu3Button.Content as string);
        private void fri3Button_Click(object sender, RoutedEventArgs e) => _ = ShowSubjectZoom(fri3Button.Content as string);
        private void mon4Button_Click(object sender, RoutedEventArgs e) => _ = ShowSubjectZoom(mon4Button.Content as string);
        private void tue4Button_Click(object sender, RoutedEventArgs e) => _ = ShowSubjectZoom(tue4Button.Content as string);
        private void wed4Button_Click(object sender, RoutedEventArgs e) => _ = ShowSubjectZoom(wed4Button.Content as string);
        private void thu4Button_Click(object sender, RoutedEventArgs e) => _ = ShowSubjectZoom(thu4Button.Content as string);
        private void fri4Button_Click(object sender, RoutedEventArgs e) => _ = ShowSubjectZoom(fri4Button.Content as string);
        private void mon5Button_Click(object sender, RoutedEventArgs e) => _ = ShowSubjectZoom(mon5Button.Content as string);
        private void tue5Button_Click(object sender, RoutedEventArgs e) => _ = ShowSubjectZoom(tue5Button.Content as string);
        private void wed5Button_Click(object sender, RoutedEventArgs e) => _ = ShowSubjectZoom(wed5Button.Content as string);
        private void thu5Button_Click(object sender, RoutedEventArgs e) => _ = ShowSubjectZoom(thu5Button.Content as string);
        private void fri5Button_Click(object sender, RoutedEventArgs e) => ShowMessage("각자 정규동아리 부장들에게 문의해주세요.", "정규동아리 활동 시간");
        private void mon6Button_Click(object sender, RoutedEventArgs e) => ShowMessage("인문학 / 세계시민 프로젝트 시간입니다.", "창의적 체험활동");
        private void tue6Button_Click(object sender, RoutedEventArgs e) => _ = ShowSubjectZoom(tue6Button.Content as string);
        private void wed6Button_Click(object sender, RoutedEventArgs e) => _ = ShowSubjectZoom(wed6Button.Content as string);
        private void thu6Button_Click(object sender, RoutedEventArgs e) => _ = ShowSubjectZoom(thu6Button.Content as string);
        private void fri6Button_Click(object sender, RoutedEventArgs e) => ShowMessage("각자 정규동아리 부장들에게 문의해주세요.", "정규동아리 활동 시간"); 
        private void mon7Button_Click(object sender, RoutedEventArgs e) => ShowMessage("인문학 / 세계시민 프로젝트 시간입니다.", "창의적 체험활동");
        private void tue7Button_Click(object sender, RoutedEventArgs e) => _ = ShowSubjectZoom(tue7Button.Content as string);
        private void wed7Button_Click(object sender, RoutedEventArgs e) => _ = ShowSubjectZoom(wed7Button.Content as string);
        private void thu7Button_Click(object sender, RoutedEventArgs e) => _ = ShowSubjectZoom(thu7Button.Content as string);
        private void fri7Button_Click(object sender, RoutedEventArgs e) => ShowMessage("즐거운 홈커밍 데이 :)", "Homecoming");
        #endregion
    }
}
