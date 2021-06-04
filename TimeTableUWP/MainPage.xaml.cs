using System;
using System.Collections.Generic;
using System.Collections;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.ApplicationModel;
using Windows.UI.Xaml.Media;
using Windows.ApplicationModel.Core;
using Windows.UI.Xaml.Documents;
using GGHS;
using GGHS.Grade2;
using TimeTableUWP.ComboboxItem;
using RollingRess;
using static RollingRess.Librarys;


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

        DateTime now = DateTime.Now;

        bool hasReadFile = false;

        static PackageVersion version = Package.Current.Id.Version;
        public static string Version { get => $"{version.Major}.{version.Minor}.{version.Build}.{version.Revision}"; }

        public MainPage()
        {
            InitializeComponent();
            SetColor();
            _ = LoopTimeAsync();
            Disable(classComboBox, langComboBox, s1comboBox, s2comboBox, scComboBox);
            if (hasReadFile is false)
            {
                _ = LoadDataFromFileAsync();
                hasReadFile = true;
            }
        }

        private void SetColor()
        {
            foreach (var border in new[] { monBorder, tueBorder, wedBorder, thuBorder, friBorder })
            {
                border.Background = new SolidColorBrush(SettingsPage.ColorType);
            }
            foreach (var comboBox in new[] {gradeComboBox, classComboBox, s1comboBox, s2comboBox, langComboBox, scComboBox})
            {
                comboBox.BorderBrush = new SolidColorBrush(SettingsPage.ColorType);
            }
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

        private async Task LoopTimeAsync() => await Task.Run(() =>
        {
            (int day, int time) pos;
            while (true)
            {
                Thread.Sleep(200);
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
                }
                );

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
                    _ => throw new IndexOutOfRangeException()
                };

                _ = CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
                () => {
                    foreach (var item in GetButtons())
                    {
                        item.Background = new SolidColorBrush(Colors.Black);
                        item.Foreground = new SolidColorBrush(Colors.White);
                    }
                    // [a, b] => 7a + b
                    // [pos.day - 1, pos.time - 1] => 7*(pos.day - 1) + (pos.time - 1)
                    GetButtons().ElementAt((7 * (pos.day-1)) + (pos.time-1)).Background =
                        new SolidColorBrush(SettingsPage.ColorType);
                    if (pos.time is <= 6) {
                        GetButtons().ElementAt((7 * (pos.day - 1)) + pos.time).Foreground = new SolidColorBrush(SettingsPage.ColorType);
                    }
                    });
            }
        });

        private void DrawTimeTable()
        {
            TimeTables.ResetByClass(@class);
            string[,] table = SetArrayByClass();

            // 월 6, 7 / 금 5, 6은 어차피 창체, 금 7도 어차피 홈커밍
            AssignButtonsByTable(table);
            mon6Button.Content = mon7Button.Content = fri5Button.Content = fri6Button.Content = Subjects.CellName.Others;
            fri7Button.Content = Subjects.CellName.HomeComing;
        }

        private void AssignButtonsByTable(string[,] table)
        {
            var subjects = ((IEnumerable)table).Cast<string>();
            var lists = GetButtons().Zip(subjects, (Button btn, string tb) => (btn, tb));
            foreach (var item in lists)
            {
                item.btn.Content = item.tb;
            }
        }

        private IEnumerable<Button> GetButtons()
        {
            yield return mon1Button;
            yield return mon2Button;
            yield return mon3Button;
            yield return mon4Button;
            yield return mon5Button;
            yield return mon6Button;
            yield return mon7Button;

            yield return tue1Button;
            yield return tue2Button;
            yield return tue3Button;
            yield return tue4Button;
            yield return tue5Button;
            yield return tue6Button;
            yield return tue7Button;

            yield return wed1Button;
            yield return wed2Button;
            yield return wed3Button;
            yield return wed4Button;
            yield return wed5Button;
            yield return wed6Button;
            yield return wed7Button;

            yield return thu1Button;
            yield return thu2Button;
            yield return thu3Button;
            yield return thu4Button;
            yield return thu5Button;
            yield return thu6Button;
            yield return thu7Button;

            yield return fri1Button;
            yield return fri2Button;
            yield return fri3Button;
            yield return fri4Button;
            yield return fri5Button;
            yield return fri6Button;
            yield return fri7Button;
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
            _ => throw new DataAccessException($"SetArrayByClass(): @class: 1~8 expected, but given {@class}.")
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
            Action[] setComboBox = {
                SetComboBoxAsClass1,
                SetComboBoxAsClass2,
                SetComboBoxAsClass3,
                SetComboBoxAsClass4,
                SetComboBoxAsClass5,
                SetComboBoxAsClass6,
                SetComboBoxAsClass7,
                SetComboBoxAsClass8,
            };
            setComboBox[@class - 1]();
        }

        private void langComboBox_SelectionChanged(object sender, SelectionChangedEventArgs _)
        {
            if (langComboBox.SelectedItem is not null)
            {
                SaveData.LangComboBoxText = Subjects.Languages.Selected = langComboBox.SelectedItem as string;
                DrawTimeTable();
            }
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
                _ => throw new ComboBoxDataException("ComboBox.SelectedItem returned wrong value")
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



        private async Task ShowSubjectZoom(string subjectCellName)
        {
            if (subjectCellName is null)
            {
                ShowMessage("Please select your grade and class first.", "Error");
                return;
            }
            if (@class is not (3 or 4 or 5 or 6 or 8))
            {
                ShowMessage($"Sorry, displaying Zoom link is not available in class {@class}.\n" + 
                    "개발자에게 줌 링크 추가를 요청해보세요.", MessageTitle.FeatrueNotImplemented);
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

            ZoomDialog contentDialog = new(@class, subjectCellName, zoomInfo);
            await contentDialog.ShowAsync();
        }

        private Dictionary<string, ZoomLinks.ZoomInfo> GetClassZoomLink() => @class switch
        {
            3 => ZoomLinks.Class3,
            4 => ZoomLinks.Class4,
            5 => ZoomLinks.Class5,
            6 => ZoomLinks.Class6,
            8 => ZoomLinks.Class8,
            _ => throw new DataAccessException(
                $"GetClassZoomLink(): Class out of range: 3, 4, 5, 8, 6 expected, but given {@class}")
        };
        
        private void TableButtons_Click(object sender, RoutedEventArgs e)
        => _ = ShowSubjectZoom((sender as Button).Content as string);
        
        private void SpecialButtons_Click(object sender, RoutedEventArgs _)
        {
            var (msg, txt) = (sender as Button).Name switch
            {
                "fri5Button" or "fri6Button" => ("각자 정규동아리 부장들에게 문의해주세요.", "정규동아리 활동 시간"),
                "mon6Button" or "mon7Button" => ("창의적 체험활동 시간입니다.", "창의적 체험활동"),
                "fri7Button" => ("즐거운 홈커밍 데이 :)", "Homecoming"),
                _ => throw new TableCellException($"SpecialButtons_Click(): No candidate to show for button '{(sender as Button).Name}'")
            };
            ShowMessage(msg, txt);
        }

        private void Button_Click_5(object sender, RoutedEventArgs e) => Frame.Navigate(typeof(SettingsPage));
    }
}
