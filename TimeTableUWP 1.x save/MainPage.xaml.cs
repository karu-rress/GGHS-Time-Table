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
using SubjectDll.Grade2;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

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

        readonly string[,] class1 = new string[5, 7]
        {
            { Subjects.Social2Tmp, Subjects.LanguageTmp, Subjects.Literature, Subjects.CriticalEnglish + "B", Subjects.CriticalEnglish + "C", Subjects.Others, Subjects.Others },
            { Subjects.Social1Tmp, Subjects.CreativeSolve, Subjects.ScienceTmp, Subjects.Mathematics, Subjects.Literature, Subjects.CriticalEnglish + "A", Subjects.Social2Tmp },
            { Subjects.CreativeSolve, Subjects.Mathematics, Subjects.MathResearch, Subjects.Social2Tmp, Subjects.Literature, Subjects.Sport, Subjects.ScienceTmp },
            { Subjects.LanguageTmp, Subjects.Sport, Subjects.Social1Tmp, Subjects.CriticalEnglish + "A", Subjects.Literature, Subjects.Mathematics, Subjects.CriticalEnglish + "D"},
            { Subjects.LanguageTmp, Subjects.Social1Tmp, Subjects.Mathematics, Subjects.ScienceTmp, Subjects.Others, Subjects.Others, Subjects.HomeComing }
        };
        readonly string[,] class2 = new string[5, 7]
        {
            { Subjects.LanguageTmp, Subjects.CriticalEnglish + "A", Subjects.Mathematics, Subjects.Literature, Subjects.Social2Tmp, Subjects.Others, Subjects.Others },
            { Subjects.Literature, Subjects.Social1Tmp, Subjects.Sport, Subjects.CreativeSolve, Subjects.ScienceTmp, Subjects.Mathematics, Subjects.CriticalEnglish + "D" },
            { Subjects.ScienceTmp, Subjects.Sport, Subjects.Literature, Subjects.CriticalEnglish + "B", Subjects.MathResearch, Subjects.LanguageTmp, Subjects.Social1Tmp },
            { Subjects.Literature, Subjects.CreativeSolve, Subjects.CriticalEnglish + "C", Subjects.Mathematics, Subjects.Social1Tmp, Subjects.LanguageTmp, Subjects.Social2Tmp},
            { Subjects.ScienceTmp, Subjects.Mathematics, Subjects.Social2Tmp, Subjects.CriticalEnglish + "A", Subjects.Others, Subjects.Others, Subjects.HomeComing }
        };
        readonly string[,] class3 = new string[5, 7]
        {
            { Subjects.Mathematics, Subjects.ScienceTmp, Subjects.CreativeSolve, Subjects.Sport,  Subjects.Social1Tmp,  Subjects.Others,  Subjects.Others }, 
            { Subjects.CriticalEnglish + "D",  Subjects.Literature,  Subjects.Mathematics,  Subjects.Social2Tmp,  Subjects.CriticalEnglish + "B",  Subjects.Social1Tmp, Subjects.LanguageTmp},
            { Subjects.Sport, Subjects.MathResearch, Subjects.CriticalEnglish+"A", Subjects.LanguageTmp, Subjects.ScienceTmp, Subjects.Literature, Subjects.Mathematics },
            { Subjects.Mathematics, Subjects.Social1Tmp, Subjects.Literature, Subjects.Social2Tmp, Subjects.ScienceTmp, Subjects.CriticalEnglish + "A", Subjects.LanguageTmp},
            { Subjects.CriticalEnglish + "C", Subjects.CreativeSolve, Subjects.Literature, Subjects.Social2Tmp, Subjects.Others, Subjects.Others, Subjects.HomeComing }
        };
        readonly string[,] class4 = new string[5, 7]
        {
            { Subjects.CreativeSolve, Subjects.Social1Tmp, Subjects.ScienceTmp, Subjects.Literature, Subjects.Social2Tmp, Subjects.Others, Subjects.Others },
            { Subjects.LanguageTmp, Subjects.Mathematics, Subjects.CriticalEnglish + "B", Subjects.Literature, Subjects.Sport, Subjects.CriticalEnglish + "C", Subjects.Social1Tmp },
            { Subjects.CriticalEnglish +"A", Subjects.CreativeSolve, Subjects.MathResearch, Subjects.Mathematics, Subjects.Literature, Subjects.Social1Tmp, Subjects.Sport },
            { Subjects.CriticalEnglish+"A", Subjects.CriticalEnglish+"D", Subjects.Mathematics, Subjects.Literature, Subjects.LanguageTmp, Subjects.ScienceTmp, Subjects.Social2Tmp},
            { Subjects.LanguageTmp, Subjects.ScienceTmp, Subjects.Social2Tmp, Subjects.Mathematics, Subjects.Others, Subjects.Others, Subjects.HomeComing }
        };
        readonly string[,] class5 = new string[5, 7]
{
            { Subjects.CriticalEnglish + "C", Subjects.ScienceTmp, Subjects.Social1Tmp, Subjects.LanguageTmp, Subjects.Mathematics, Subjects.Others, Subjects.Others },
            { Subjects.Mathematics, Subjects.Literature, Subjects.LanguageTmp, Subjects.Social2Tmp, Subjects.CriticalEnglish + "D", Subjects.CriticalEnglish + "B", Subjects.CreativeSolve},
            { Subjects.Literature, Subjects.LanguageTmp, Subjects.MathResearch, Subjects.CriticalEnglish + "A", Subjects.ScienceTmp, Subjects.Sport, Subjects.Mathematics },
            { Subjects.CriticalEnglish + "A", Subjects.Sport, Subjects.Literature, Subjects.Social2Tmp, Subjects.ScienceTmp, Subjects.CreativeSolve, Subjects.Social1Tmp},
            { Subjects.Social1Tmp, Subjects.Mathematics, Subjects.Literature, Subjects.Social2Tmp, Subjects.Others, Subjects.Others, Subjects.HomeComing }
};
        readonly string[,] class6 = new string[5, 7]
        {
            { Subjects.Mathematics, Subjects.CriticalEnglish + "A", Subjects.Literature, Subjects.CreativeSolve,  Subjects.Social2Tmp,  Subjects.Others,  Subjects.Others },
            { Subjects.CriticalEnglish + "C",  Subjects.Social1Tmp,  Subjects.Sport,  Subjects.LanguageTmp,  Subjects.Mathematics,  Subjects.Literature, Subjects.ScienceTmp},
            { Subjects.MathResearch, Subjects.Sport, Subjects.Mathematics, Subjects.ScienceTmp, Subjects.Social1Tmp, Subjects.Literature, Subjects.CriticalEnglish + "D" },
            { Subjects.Literature, Subjects.LanguageTmp, Subjects.ScienceTmp, Subjects.CreativeSolve, Subjects.CriticalEnglish + "B", Subjects.Mathematics, Subjects.Social2Tmp},
            { Subjects.CriticalEnglish + "A", Subjects.LanguageTmp, Subjects.Social2Tmp, Subjects.Social1Tmp, Subjects.Others, Subjects.Others, Subjects.HomeComing }
        };
        readonly string[,] class7 = new string[5, 7]
        {
            { Subjects.LanguageTmp, Subjects.Mathematics, Subjects.Social1Tmp, Subjects.Sport, Subjects.ScienceTmp, Subjects.Others, Subjects.Others },
            { Subjects.CreativeSolve, Subjects.CriticalEnglish+"D", Subjects.CriticalEnglish + "C", Subjects.Social2Tmp, Subjects.CriticalEnglish + "A", Subjects.MathResearch, Subjects.Literature },
            { Subjects.Sport, Subjects.ScienceTmp, Subjects.CriticalEnglish + "B", Subjects.Literature, Subjects.Literature, Subjects.LanguageTmp, Subjects.CreativeSolve },
            { Subjects.ScienceTmp, Subjects.CriticalEnglish + "A", Subjects.Mathematics, Subjects.Social2Tmp, Subjects.Literature, Subjects.LanguageTmp, Subjects.Social1Tmp},
            { Subjects.Social1Tmp, Subjects.Mathematics, Subjects.Literature, Subjects.Social2Tmp, Subjects.Others, Subjects.Others, Subjects.HomeComing }
        };
        readonly string[,] class8 = new string[5, 7] {
            { Subjects.Literature, Subjects.CriticalEnglish + "D", Subjects.CriticalEnglish + "A", Subjects.Mathematics, Subjects.ScienceTmp, Subjects.Others, Subjects.Others },
            { Subjects.CriticalEnglish + "A", Subjects.Mathematics, Subjects.Literature, Subjects.Social2Tmp, Subjects.Sport, Subjects.Social1Tmp, Subjects.LanguageTmp },
            { Subjects.Literature, Subjects.ScienceTmp, Subjects.CriticalEnglish + "C", Subjects.LanguageTmp, Subjects.CreativeSolve, Subjects.MathResearch, Subjects.Sport},
            { Subjects.ScienceTmp, Subjects.Social1Tmp, Subjects.Literature, Subjects.Social2Tmp, Subjects.Mathematics, Subjects.CriticalEnglish + "B", Subjects.LanguageTmp},
            { Subjects.Mathematics, Subjects.Social1Tmp, Subjects.CreativeSolve, Subjects.Social2Tmp, Subjects.Others, Subjects.Others, Subjects.HomeComing}
        };

        readonly Dictionary<string, (string link, string id, string pw)> zoomLink = new()
        {
            [Subjects.Literature] = ("https://zoom.us/j/97317948690?pwd=WWovQUdBQS8rVVMzMUNyZmNPSDY3Zz09", "973 1794 8690", "626239"),
            [Subjects.RawName.Chemistry] = ("https://zoom.us/j/93595351190?pwd=eHVIMXVGSnFTaGhYWVprNm4xTEh0Zz09", "935 9535 1190", "2021gghs"),
            [Subjects.RawName.Economy] = ("https://zoom.us/j/2521095403?pwd=MVBmOURvRGU1azRwY0lnejVwa2tjUT09", "252 109 5403", "2021"),
            // [Subjects.RawName.Geography] = ("https://us02web.zoom.us/j/81307691161?pwd=Q2ZMOGtlTFdzUkJabHdwYmxXYmtCZz09", "813 0769 1161", "Y4UwM9"),
            [Subjects.RawName.Environment] = ("https://zoom.us/j/94849418747", "948 4941 8747", "geogeo"),
            [Subjects.RawName.Chinese] = ("https://zoom.us/j/99535123743?pwd=d0dPemVjNXIxcks5RCt0OFc1aGg0Zz09", "995 3512 3743", "1eMXJM"),
            [Subjects.CreativeSolve] = ("https://meet.google.com/lookup/bgt6c65ccm", "None", "None"),
            [Subjects.CriticalEnglish + "A"] = ("https://zoom.us/j/5031101343?pwd=QTRmbnRLSHFPamh4U3d2ZS9JTXdrUT09", "503 110 1343", "1111"),
            [Subjects.CriticalEnglish + "B"] = ("https://zoom.us/j/98351310802?pwd=ODlENFVJeGlqWG0wYmgyR2RNUURHUT09", "983 5131 0802", "8at5CP"),
            [Subjects.CriticalEnglish + "C"] = ("https://zoom.us/j/5365083101?pwd=VDV4VHA5MVUrcDV4cDV1RitZeHovZz09", "536 508 3101", "2021"),
        };

        static class MessageTitle
        {
            public const string FeatrueNotImplemented = "Featrue not implemented yet";
        }

        public MainPage()
        {
            this.InitializeComponent();

            RefreshTime();
        }



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
            string[,] table = @class switch
            {
                1 => class1.Clone() as string[,],
                2 => class2.Clone() as string[,],
                3 => class3.Clone() as string[,],
                4 => class4.Clone() as string[,],
                5 => class5.Clone() as string[,],
                6 => class6.Clone() as string[,],
                7 => class7.Clone() as string[,],
                8 => class8.Clone() as string[,],
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

        #region ComboBox
        private void EnableAllCombobox()
        => langComboBox.IsEnabled
            = s1comboBox.IsEnabled 
            = s2comboBox.IsEnabled
            = scComboBox.IsEnabled
            = true;

        private void gradeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            EnableAllCombobox();
            if (gradeComboBox.SelectedItem is null)
                return;

            grade = (gradeComboBox.SelectedItem as string)[6] - '0';

            if (grade is not 2)
            {
                ShowMessage($"Sorry, Grade {grade} has not been implemented yet.", MessageTitle.FeatrueNotImplemented);
                gradeComboBox.SelectedItem = null;
            }
        }

        private void SetSubjectAsClass1()
        {
            Subjects.SetSocial1Subject(Subjects.Social1.Ethics);
            Subjects.SetSocial2Subject(Subjects.Social2.Politics);
            Subjects.SetLanguageSubject(Subjects.Language.Spanish);
            Subjects.SetScienceSubject(Subjects.Science.Biology);

            s1comboBox.SelectedItem = Subjects.RawName.Ethics;
            s2comboBox.SelectedItem = Subjects.RawName.Politics;
            langComboBox.SelectedItem = Subjects.RawName.Spanish;
            scComboBox.SelectedItem = Subjects.RawName.Biology;

            s1comboBox.IsEnabled = false;
            s2comboBox.IsEnabled = false;
            langComboBox.IsEnabled = false;
            scComboBox.IsEnabled = false;
        }
        private void SetSubjectAsClass2()
        {
            Subjects.SetSocial1Subject(Subjects.Social1.Ethics);
            Subjects.SetScienceSubject(Subjects.Science.Biology);

            s1comboBox.SelectedItem = Subjects.RawName.Ethics;
            scComboBox.SelectedItem = Subjects.RawName.Biology;

            s1comboBox.IsEnabled = false;
            scComboBox.IsEnabled = false;
        }
        private void SetSubjectAsClass3()
        {
            Subjects.SetSocial1Subject(Subjects.Social1.Ethics);
            s1comboBox.SelectedItem = Subjects.RawName.Ethics;
            s1comboBox.IsEnabled = false;
        }
        private void SetSubjectAsClass4()
        {
            Subjects.SetSocial1Subject(Subjects.Social1.Ethics);
            Subjects.SetLanguageSubject(Subjects.Language.Chinese);
            Subjects.SetScienceSubject(Subjects.Science.Biology);

            s1comboBox.SelectedItem = Subjects.RawName.Ethics;
            langComboBox.SelectedItem = Subjects.RawName.Chinese;
            scComboBox.SelectedItem = Subjects.RawName.Biology;

            s1comboBox.IsEnabled = false;
            langComboBox.IsEnabled = false;
            scComboBox.IsEnabled = false;
        }
        private void SetSubjectAsClass5()
        {
            Subjects.SetLanguageSubject(Subjects.Language.Spanish);
            langComboBox.SelectedItem = Subjects.RawName.Spanish;
            langComboBox.IsEnabled = false;
        }
        private void SetSubjectAsClass6()
        {
            Subjects.SetSocial1Subject(Subjects.Social1.Environment);
            Subjects.SetLanguageSubject(Subjects.Language.Spanish);
            Subjects.SetScienceSubject(Subjects.Science.Biology);

            s1comboBox.SelectedItem = Subjects.RawName.Environment;
            langComboBox.SelectedItem = Subjects.RawName.Spanish;
            scComboBox.SelectedItem = Subjects.RawName.Biology;

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
            s1comboBox.SelectedItem = Subjects.RawName.Environment;
            s1comboBox.IsEnabled = false;
        }

        delegate void SetSubjectDelegate();
        private void classComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            EnableAllCombobox();

            // Get & Set Class
            @class = (classComboBox.SelectedItem as string)[6] - '0';
            Debug.Assert(@class is >= 1 and  <= 8);

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
            Subjects.SetLanguageSubject(langComboBox.SelectedItem as string switch
            {
                Subjects.RawName.Spanish => Subjects.Language.Spanish,
                Subjects.RawName.Chinese => Subjects.Language.Chinese,
                Subjects.RawName.Japanese => Subjects.Language.Japanese,
                _ => throw new NotImplementedException(),
            });
            DrawTimeTable();
        }

        private void s2comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Subjects.SetSocial2Subject(s2comboBox.SelectedItem as string switch
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
            Subjects.SetScienceSubject(scComboBox.SelectedItem as string switch
            {
                Subjects.RawName.Physics => Subjects.Science.Physics,
                Subjects.RawName.Chemistry => Subjects.Science.Chemistry,
                Subjects.RawName.Biology => Subjects.Science.Biology,
                _ => throw new NotImplementedException(),
            });
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
            foreach (var item in zoomLink)
            {
                sb.Append($"[{item.Key}] \n" +
$"ID: {item.Value.id} / PW: {item.Value.pw}\n\n");
            }
            ShowMessage(sb.ToString());
        }
    }
}
