using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Threading;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using GGHS.Grade2;
using TimeTableWPF.ComboboxItem;

namespace TimeTableWPF
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
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

        public MainWindow()
        {
            InitializeComponent();

            RefreshTime();
        }

        private static MessageBoxResult ShowMessage(string context, string title = "", MessageBoxButton button = MessageBoxButton.OK, MessageBoxImage? icon = null)
            => icon == null ? MessageBox.Show(context, title, button) : MessageBox.Show(context, title, button, icon.Value);

        private void ShowErrorMessage(Exception arg)
        => ShowMessage(arg.ToString(), "An error has occured.", icon: MessageBoxImage.Error);

        private void RefreshTime()
        => _ = LoopTime();

        private async Task LoopTime() => await Task.Factory.StartNew(() =>
        {
            (int day, int time) pos;
            Border[,] table =
                {
                    { mon1Box, mon2Box, mon3Box,mon4Box,mon5Box,mon6Box,mon7Box},
                    { tue1Box, tue2Box, tue3Box, tue4Box, tue5Box, tue6Box, tue7Box, },
                    { wed1Box, wed2Box, wed3Box, wed4Box, wed5Box, wed6Box, wed7Box},
                    { thu1Box, thu2Box, thu3Box, thu4Box, thu5Box, thu6Box, thu7Box },
                    { fri1Box, fri2Box, fri3Box, fri4Box, fri5Box, fri6Box, fri7Box, }
                };
            while (true)
            {
                Thread.Sleep(100);
                now = DateTime.Now;
                Application.Current.Dispatcher.Invoke(() =>
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
                Application.Current.Dispatcher.Invoke(
                 () => table[pos.day - 1, pos.time - 1].Background = new SolidColorBrush(Color.FromArgb(0xFF, 0x6D, 0x6D, 0xBD)));
            }
        });

        private void DrawTimeTable()
        {
            string[,] table = @class switch
            {
                1 => Classes.Class1.Clone() as string[,],
                2 => Classes.Class2.Clone() as string[,],
                3 => Classes.Class3.Clone() as string[,],
                4 => Classes.Class4.Clone() as string[,],
                5 => Classes.Class5.Clone() as string[,],
                6 => Classes.Class6.Clone() as string[,],
                7 => Classes.Class7.Clone() as string[,],
                8 => Classes.Class8.Clone() as string[,],
                _ => throw new NotImplementedException()
            };

            // 월 6, 7 / 금 5, 6은 어차피 창체, 금 7도 어차피 홈커밍
            mon6Text.Text = mon7Text.Text = fri5Text.Text = fri6Text.Text = Subjects.Others;
            fri7Text.Text = Subjects.HomeComing;

            // 선택과목 업데이트
            for (int i = 0; i < table.GetLength(0); i++)
            {
                for (int j = 0; j < table.GetLength(1); j++)
                {
                    table[i, j] = table[i, j] switch
                    {
                        Subjects.LanguageTmp => Subjects.GetLanguageSubject(),
                        Subjects.ScienceTmp => Subjects.GetScienceSubject(),
                        Subjects.Social1Tmp => Subjects.GetSocial1Subject(),
                        Subjects.Social2Tmp => Subjects.GetSocial2Subject(),
                        _ => table[i, j]
                    };
                }
            }

            (mon1Text.Text, mon2Text.Text, mon3Text.Text, mon4Text.Text, mon5Text.Text) =
                (table[0, 0], table[0, 1], table[0, 2], table[0, 3], table[0, 4]);

            (tue1Text.Text, tue2Text.Text, tue3Text.Text, tue4Text.Text, tue5Text.Text, tue6Text.Text, tue7Text.Text) =
                (table[1, 0], table[1, 1], table[1, 2], table[1, 3], table[1, 4], table[1, 5], table[1, 6]);

            (wed1Text.Text, wed2Text.Text, wed3Text.Text, wed4Text.Text, wed5Text.Text, wed6Text.Text, wed7Text.Text) =
                (table[2, 0], table[2, 1], table[2, 2], table[2, 3], table[2, 4], table[2, 5], table[2, 6]);

            (thu1Text.Text, thu2Text.Text, thu3Text.Text, thu4Text.Text, thu5Text.Text, thu6Text.Text, thu7Text.Text) =
                (table[3, 0], table[3, 1], table[3, 2], table[3, 3], table[3, 4], table[3, 5], table[3, 6]);

            (fri1Text.Text, fri2Text.Text, fri3Text.Text, fri4Text.Text) =
                (table[4, 0], table[4, 1], table[4, 2], table[4, 3]);
        }

        private void EmptyComboBox(ref ComboBox cb)
        => cb.SelectedIndex = -1;
        

        private void EnableAllCombobox()
=> langComboBox.IsEnabled
    = s1comboBox.IsEnabled
    = s2comboBox.IsEnabled
    = scComboBox.IsEnabled
    = true;

        private void gradeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            EnableAllCombobox();
            if (string.IsNullOrEmpty(gradeComboBox.SelectedValue?.ToString()))
                return;

            grade = gradeComboBox.SelectedValue.ToString()[6] - '0';

            if (grade is not 2)
            {
                ShowMessage($"Sorry, Grade {grade} has not been implemented yet.", MessageTitle.FeatrueNotImplemented, icon: MessageBoxImage.Exclamation);
                EmptyComboBox(ref gradeComboBox);
            }
        }

        private void SetSubjectAsClass1()
        {
            Subjects.SetSocial1Subject(Subjects.Social1.Ethics);
            Subjects.SetSocial2Subject(Subjects.Social2.Politics);
            Subjects.SetLanguageSubject(Subjects.Language.Spanish);
            Subjects.SetScienceSubject(Subjects.Science.Biology);

            s1comboBox.SelectedValue = Subjects.RawName.Ethics;
            s2comboBox.SelectedValue = Subjects.RawName.Politics;
            langComboBox.SelectedValue = Subjects.RawName.Spanish;
            scComboBox.SelectedValue = Subjects.RawName.Biology;

            s1comboBox.IsEnabled = false;
            s2comboBox.IsEnabled = false;
            langComboBox.IsEnabled = false;
            scComboBox.IsEnabled = false;
        }
        private void SetSubjectAsClass2()
        {
            Subjects.SetSocial1Subject(Subjects.Social1.Ethics);
            Subjects.SetScienceSubject(Subjects.Science.Biology);

            s1comboBox.SelectedValue = Subjects.RawName.Ethics;
            scComboBox.SelectedValue = Subjects.RawName.Biology;

            s1comboBox.IsEnabled = false;
            scComboBox.IsEnabled = false;
        }
        private void SetSubjectAsClass3()
        {
            Subjects.SetSocial1Subject(Subjects.Social1.Ethics);
            s1comboBox.SelectedValue = Subjects.RawName.Ethics;
            s1comboBox.IsEnabled = false;
        }
        private void SetSubjectAsClass4()
        {
            Subjects.SetSocial1Subject(Subjects.Social1.Ethics);
            Subjects.SetLanguageSubject(Subjects.Language.Chinese);
            Subjects.SetScienceSubject(Subjects.Science.Biology);

            s1comboBox.SelectedValue = Subjects.RawName.Ethics;
            langComboBox.SelectedValue = Subjects.RawName.Chinese;
            scComboBox.SelectedValue = Subjects.RawName.Biology;

            s1comboBox.IsEnabled = false;
            langComboBox.IsEnabled = false;
            scComboBox.IsEnabled = false;
        }
        private void SetSubjectAsClass5()
        {
            Subjects.SetLanguageSubject(Subjects.Language.Spanish);
            langComboBox.SelectedValue = Subjects.RawName.Spanish;
            langComboBox.IsEnabled = false;
        }
        private void SetSubjectAsClass6()
        {
            Subjects.SetSocial1Subject(Subjects.Social1.Environment);
            Subjects.SetLanguageSubject(Subjects.Language.Spanish);
            Subjects.SetScienceSubject(Subjects.Science.Biology);

            s1comboBox.SelectedValue = Subjects.RawName.Environment;
            langComboBox.SelectedValue = Subjects.RawName.Spanish;
            scComboBox.SelectedValue = Subjects.RawName.Biology;

            s1comboBox.IsEnabled = false;
            langComboBox.IsEnabled = false;
            scComboBox.IsEnabled = false;
        }
        private void SetSubjectAsClass7()
        {
        }
        private void SetSubjectAsClass8()
        {
            Subjects.SetSocial1Subject(Subjects.Social1.Environment);
            s1comboBox.SelectedValue = Subjects.RawName.Environment;
            s1comboBox.IsEnabled = false;
        }

        delegate void SetSubjectDelegate();
        private void classComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            EnableAllCombobox();

            // Get & Set Class
            @class = (classComboBox.SelectedValue.ToString() as string)[6] - '0';
            Debug.Assert(@class is >= 1 and <= 8);

            SetSubjectDelegate setSubject = @class switch
            {
                1 => SetSubjectAsClass1,
                2 => SetSubjectAsClass2,
                3 => SetSubjectAsClass3,
                4 => SetSubjectAsClass4,
                5 => SetSubjectAsClass5,
                6 => SetSubjectAsClass6,
                7 => SetSubjectAsClass7,
                8 => SetSubjectAsClass8,
                _ => throw new IndexOutOfRangeException()
            };

            setSubject();
            DrawTimeTable();
        }

        private void langComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Subjects.SetLanguageSubject(langComboBox.SelectedValue.ToString() switch
            {
                Subjects.RawName.Spanish => Subjects.Language.Spanish,
                Subjects.RawName.Chinese => Subjects.Language.Chinese,
                Subjects.RawName.Japanese => Subjects.Language.Japanese,
                _ => throw new NotImplementedException(),
            });
            DrawTimeTable();
        }
        private void s1comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Subjects.SetSocial1Subject(s1comboBox.SelectedValue.ToString() switch
            {
                Subjects.RawName.Ethics => Subjects.Social1.Ethics,
                Subjects.RawName.Environment => Subjects.Social1.Environment,
                _ => throw new IndexOutOfRangeException(),
            });
            DrawTimeTable();
        }

        private void s2comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Subjects.SetSocial2Subject(s2comboBox.SelectedValue.ToString() switch
            {
                Subjects.RawName.Geography => Subjects.Social2.Geography,
                Subjects.RawName.History => Subjects.Social2.History,
                Subjects.RawName.Politics => Subjects.Social2.Politics,
                Subjects.RawName.Economy => Subjects.Social2.Economy,
                _ => throw new NotImplementedException(),
            });
            DrawTimeTable();
        }

        private void scComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Subjects.SetScienceSubject(scComboBox.SelectedValue.ToString() switch
            {
                Subjects.RawName.Physics => Subjects.Science.Physics,
                Subjects.RawName.Chemistry => Subjects.Science.Chemistry,
                Subjects.RawName.Biology => Subjects.Science.Biology,
                _ => throw new NotImplementedException(),
            });
            DrawTimeTable();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
               => dateType = dateType switch
               {
                   DateType.YYYYMMDD => DateType.MMDDYYYY,
                   DateType.MMDDYYYY => DateType.YYYYMMDD2,
                   DateType.YYYYMMDD2 => DateType.YYYYMMDD,
                   _ => throw new NotImplementedException()
               };

        private void Button_Click_1(object sender, RoutedEventArgs e)
        => use24hour = !use24hour;

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            gradeComboBox.SelectedValue = Grade.Grade2;
            classComboBox.SelectedValue = Class.Class4;
            s2comboBox.SelectedValue = Subjects.RawName.Politics;
            DrawTimeTable();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            gradeComboBox.SelectedValue = Grade.Grade2;
            classComboBox.SelectedValue = Class.Class8;
            langComboBox.SelectedValue = Subjects.RawName.Chinese;
            s2comboBox.SelectedValue = Subjects.RawName.Economy;
            scComboBox.SelectedValue = Subjects.RawName.Chemistry;
            DrawTimeTable();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            if (@class is not 8)
            {
                ShowMessage("Sorry, feature 'Get ZOOM Link' is only available in class 8.", MessageTitle.FeatrueNotImplemented);
                return;
            }

            // TryGetValue..근데 언제의 줌 링크를 가져오게?
            if (dayBlock.Text is "Sat" or "Sun")
            {
                ShowMessage("There's no class in weekend.", "Error");
            }

            StringBuilder sb = new();
            foreach ((string subject, Class8.ZoomInfo link) in Class8.ZoomLink)
            {
                sb.Append($"[{subject}] \n" +
$"ID: {link.Id} / PW: {link.Pw}\n\n");
            }
            ShowMessage(sb.ToString());
        }


    }
}
