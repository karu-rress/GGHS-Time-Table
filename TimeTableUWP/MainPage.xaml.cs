using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Text;
using GGHS.Grade2;
using System.ComponentModel;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml.Documents;
using GGHS;

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
    namespace ComboboxItem
    {
        public static class Grade
        {
            public const string Grade1 = "Grade 1";
            public const string Grade2 = "Grade 2";
            public const string Grade3 = "Grade 3";
        }
        public static class Class
        {
            public const string Class1 = "Class 1";
            public const string Class2 = "Class 2";
            public const string Class3 = "Class 3";
            public const string Class4 = "Class 4";
            public const string Class5 = "Class 5";
            public const string Class6 = "Class 6";
            public const string Class7 = "Class 7";
            public const string Class8 = "Class 8";
        } 
    }
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    /// 
    public sealed partial class MainPage : Page
    {
        private int grade;
        private int @class = 8;
        private bool use24hour = false;

        enum DateType
        {
            YYYYMMDD,
            MMDDYYYY,
            YYYYMMDD2
        }
        DateType dateType = DateType.MMDDYYYY;
        DateTime now = DateTime.Now;

        static class MessageTitle
        {
            public const string FeatrueNotImplemented = "Featrue not implemented yet";
        }

        public MainPage()
        {
            this.InitializeComponent();

            RefreshTime();
            DrawTimeTable();

            classComboBox.IsEnabled
            = langComboBox.IsEnabled
            = s1comboBox.IsEnabled
            = s2comboBox.IsEnabled
            = scComboBox.IsEnabled = false;
        }

        private void EmptyComboBox(ref ComboBox cb)
        => cb.SelectedIndex = -1;

        private async void ShowMessage(string context, string title = "")
        => await new MessageDialog(context) { Title = title }.ShowAsync();

        private async void ShowContent(string content, string title = "")
            => await new ContentDialog() { Title = title, Content = content, CloseButtonText = "OK" }.ShowAsync();

        private void ShowErrorMessage(Exception arg)
        => ShowMessage(arg.ToString(), "An error has occured.");

        private void RefreshTime()
        => _ = LoopTime();

        private async Task LoopTime() => await Task.Factory.StartNew(() =>
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
                Thread.Sleep(100);
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
                () => table[pos.day - 1, pos.time - 1].Background = new SolidColorBrush(Color.FromArgb(0xFF, 0x6D, 0x6D, 0xBD)));
            }
        });

        private void DrawTimeTable()
        {
            TimeTables.ResetByClass(@class);
            string[,] table = @class switch
            {
                1 => TimeTables.Class1.Clone() as string[,],
                2 => TimeTables.Class2.Clone() as string[,],
                3 => TimeTables.Class3.Clone() as string[,],
                4 => TimeTables.Class4.Clone() as string[,],
                5 => TimeTables.Class5.Clone() as string[,],
                6 => TimeTables.Class6.Clone() as string[,],
                7 => TimeTables.Class7.Clone() as string[,],
                8 => TimeTables.Class8.Clone() as string[,],
                _ => throw new NotImplementedException()
            };

            // 월 6, 7 / 금 5, 6은 어차피 창체, 금 7도 어차피 홈커밍
            mon6Button.Content = mon7Button.Content = fri5Button.Content = fri6Button.Content = Subjects.CellName.Others;
            fri7Button.Content = Subjects.CellName.HomeComing;

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

        #region ComboBox
        private void EnableAllCombobox()
        => langComboBox.IsEnabled
            = s1comboBox.IsEnabled 
            = s2comboBox.IsEnabled
            = scComboBox.IsEnabled
            = true;

        private void gradeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // EnableAllCombobox();
            if (gradeComboBox.SelectedItem is null)
                return;

            grade = (((gradeComboBox.SelectedItem as string)?[6]) ?? '8') - '0';
            if (grade is not 2)
            {
                ShowMessage($"Sorry, Grade {grade} has not been implemented yet.", MessageTitle.FeatrueNotImplemented);
                EmptyComboBox(ref gradeComboBox);
            }

            /* TODO: for future
            EmptyComboBox(ref classComboBox);
            EmptyComboBox(ref langComboBox);
            EmptyComboBox(ref scComboBox);
            EmptyComboBox(ref s1comboBox);
            EmptyComboBox(ref s2comboBox);

            */
            classComboBox.IsEnabled = true;
        }

        private void SetComboBoxAsClass1()
        {
            s1comboBox.SelectedItem = Subjects.ComboBoxName.Ethics;
            s2comboBox.SelectedItem = Subjects.ComboBoxName.Politics;
            langComboBox.SelectedItem = Subjects.ComboBoxName.Spanish;
            scComboBox.SelectedItem = Subjects.ComboBoxName.Biology;

            s1comboBox.IsEnabled = false;
            s2comboBox.IsEnabled = false;
            langComboBox.IsEnabled = false;
            scComboBox.IsEnabled = false;
        }
        private void SetComboBoxAsClass2()
        {
            s1comboBox.SelectedItem = Subjects.ComboBoxName.Ethics;
            scComboBox.SelectedItem = Subjects.ComboBoxName.Biology;

            s1comboBox.IsEnabled = false;
            scComboBox.IsEnabled = false;

            EmptyComboBox(ref langComboBox);
            EmptyComboBox(ref s2comboBox);
        }
        private void SetComboBoxAsClass3()
        {
            s1comboBox.SelectedItem = Subjects.ComboBoxName.Ethics;
            s1comboBox.IsEnabled = false;

            EmptyComboBox(ref langComboBox);
            EmptyComboBox(ref scComboBox);
            EmptyComboBox(ref s2comboBox);
        }
        private void SetComboBoxAsClass4()
        {
            s1comboBox.SelectedItem = Subjects.ComboBoxName.Ethics;
            langComboBox.SelectedItem = Subjects.ComboBoxName.Chinese;
            scComboBox.SelectedItem = Subjects.ComboBoxName.Biology;

            s1comboBox.IsEnabled = false;
            langComboBox.IsEnabled = false;
            scComboBox.IsEnabled = false;

            EmptyComboBox(ref s2comboBox);
        }
        private void SetComboBoxAsClass5()
        {
            langComboBox.SelectedItem = Subjects.ComboBoxName.Spanish;
            langComboBox.IsEnabled = false;

            EmptyComboBox(ref scComboBox);
            EmptyComboBox(ref s1comboBox);
            EmptyComboBox(ref s2comboBox);
        }
        private void SetComboBoxAsClass6()
        {
            s1comboBox.SelectedItem = Subjects.ComboBoxName.Environment;
            langComboBox.SelectedItem = Subjects.ComboBoxName.Spanish;
            scComboBox.SelectedItem = Subjects.ComboBoxName.Biology;

            s1comboBox.IsEnabled = false;
            langComboBox.IsEnabled = false;
            scComboBox.IsEnabled = false;

            EmptyComboBox(ref s2comboBox);
        }
        private void SetComboBoxAsClass7()
        {
            EmptyComboBox(ref langComboBox);
            EmptyComboBox(ref scComboBox);
            EmptyComboBox(ref s1comboBox);
            EmptyComboBox(ref s2comboBox);
        }
        private void SetComboBoxAsClass8()
        {
            s1comboBox.SelectedItem = Subjects.ComboBoxName.Environment;
            s1comboBox.IsEnabled = false;

            EmptyComboBox(ref langComboBox);
            EmptyComboBox(ref scComboBox);
            EmptyComboBox(ref s2comboBox);
        }

        delegate void SetSubjectDelegate();
        private void classComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (classComboBox.SelectedItem is null)
                return;
            EnableAllCombobox();
            Subjects.Clear();

            // Get & Set Class
            @class = (classComboBox.SelectedItem as string)[6] - '0';
            TimeTables.ResetByClass(@class);
            Action setComboBox = @class switch
            {
                1 => SetComboBoxAsClass1,
                2 => SetComboBoxAsClass2,
                3 => SetComboBoxAsClass3,
                4 => SetComboBoxAsClass4,
                5 => SetComboBoxAsClass5,
                6 => SetComboBoxAsClass6,
                7 => SetComboBoxAsClass7,
                8 => SetComboBoxAsClass8,
                _ => throw new IndexOutOfRangeException()
            };
            setComboBox();
            DrawTimeTable();
        }

        [RefersToComboBoxName]
        private void langComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (langComboBox.SelectedItem is null)
                return;
            Subjects.Languages.Selected = langComboBox.SelectedItem as string;
            DrawTimeTable();
        }

        [RefersToCellName]
        private void s1comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (s1comboBox.SelectedItem is null)
                return;
            Subjects.Social1.Selected = (s1comboBox.SelectedItem as string) switch
            {
                Subjects.ComboBoxName.Ethics => Subjects.Social1.Ethics,
                Subjects.ComboBoxName.Environment => Subjects.Social1.Environment,
                _ => throw new Exception("ComboBox.SelectedItem returned wrong value")
            };
            DrawTimeTable();
        }

        [RefersToComboBoxName]
        private void s2comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (s2comboBox.SelectedItem is null)
                return;
            Subjects.Social2.Selected = s2comboBox.SelectedItem as string;
            DrawTimeTable();
        }

        [RefersToComboBoxName]
        private void scComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (scComboBox.SelectedItem is null)
                return;
            Subjects.Sciences.Selected = scComboBox.SelectedItem as string;
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

        [RefersToComboBoxName]
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            gradeComboBox.SelectedItem = Grade.Grade2;
            classComboBox.SelectedItem = Class.Class4;
            s2comboBox.SelectedItem = Subjects.ComboBoxName.Politics;
            DrawTimeTable();
        }

        [RefersToComboBoxName]
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            gradeComboBox.SelectedItem = Grade.Grade2;
            classComboBox.SelectedItem = Class.Class8;
            langComboBox.SelectedItem = Subjects.ComboBoxName.Chinese;
            s2comboBox.SelectedItem = Subjects.ComboBoxName.Economy;
            scComboBox.SelectedItem = Subjects.ComboBoxName.Chemistry;
            DrawTimeTable();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
        }

        private async Task ShowSubjectZoom(string subjectCellName)
        {
            if (@class != 4 && @class != 8)
            {
                ShowMessage($"Sorry, displaying Zoom link is not available in class {@class}.", MessageTitle.FeatrueNotImplemented);
                return;
            }
            ZoomLinks.ZoomInfo zoomInfo;
            var thisClass = @class switch
            {
                4 => ZoomLinks.Class4,
                8 => ZoomLinks.Class8,
                _ => throw new NotImplementedException()
            };

            if (thisClass.TryGetValue(subjectCellName, out zoomInfo) is false)
            {
                ShowMessage($"Zoom Link for {subjectCellName} is not available.", "No Data for Zoom Link");
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

Click 'Open Zoom Meeting' or the link above to join the zoom." });
            ContentDialog contentDialog = new()
            {
                Title = $"{subjectCellName} Zoom Link",
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
        private void mon6Button_Click(object sender, RoutedEventArgs e) => _ = ShowSubjectZoom(mon6Button.Content as string);
        private void tue6Button_Click(object sender, RoutedEventArgs e) => _ = ShowSubjectZoom(tue6Button.Content as string);
        private void wed6Button_Click(object sender, RoutedEventArgs e) => _ = ShowSubjectZoom(wed6Button.Content as string);
        private void thu6Button_Click(object sender, RoutedEventArgs e) => _ = ShowSubjectZoom(thu6Button.Content as string);
        private void fri6Button_Click(object sender, RoutedEventArgs e) => ShowMessage("각자 정규동아리 부장들에게 문의해주세요.", "정규동아리 활동 시간"); 
        private void mon7Button_Click(object sender, RoutedEventArgs e) => _ = ShowSubjectZoom(mon7Button.Content as string);
        private void tue7Button_Click(object sender, RoutedEventArgs e) => _ = ShowSubjectZoom(tue7Button.Content as string);
        private void wed7Button_Click(object sender, RoutedEventArgs e) => _ = ShowSubjectZoom(wed7Button.Content as string);
        private void thu7Button_Click(object sender, RoutedEventArgs e) => _ = ShowSubjectZoom(thu7Button.Content as string);
        private void fri7Button_Click(object sender, RoutedEventArgs e) => ShowMessage("즐거운 홈커밍 데이 :)", "Homecoming");
        #endregion
    }
}
