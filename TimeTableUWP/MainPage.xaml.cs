#nullable enable

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.ApplicationModel;
using GGHS;
using GGHS.Grade2.Semester2;
using static RollingRess.StaticClass;
using System.Threading;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace TimeTableUWP
{

    public sealed partial class MainPage : Page
    {

        private int grade;
        private int @class = 8;
        private DateTime now = DateTime.Now;
        private static bool hasReadFile = false;
        private static PackageVersion version => Package.Current.Id.Version;

        /// <summary>
        /// GGHS Time Table's version: string value with the format "X.X.X"
        /// </summary>
        public static string Version => $"{version.Major}.{version.Minor}.{version.Build}";

        /// <summary>
        /// Current ComboBox values
        /// </summary>
        static (int grade, int @class, int lang, int special1, int special2, int science) comboBoxSelection = (-1, -1, -1, -1, -1, -1);

        private TimeTables timeTable => new();
        private ZoomLinks zoomLink => new();

        public MainPage()
        {
            InitializeComponent();
            _ = LoopTimeAsync(); // detach.
            if (!hasReadFile)
            {
                _ = ReadFile();
                Thread.Sleep(100);
                hasReadFile = true;
            }
            InitializeUI();

            async Task ReadFile() => await LoadDataFromFileAsync();
        }

        private async Task LoadDataFromFileAsync()
        {
            if (await SaveData.LoadDataAsync() is true)
            {
                SaveData.SetGradeAndClass(ref grade, ref @class);
                SetComboBoxAsClass();
                SaveData.SetComboBoxes(ComboBoxes);
            }
        }

        #region ENEMERATORS

        private IEnumerable<ComboBox> ComboBoxes
        {
            get
            {
                yield return gradeComboBox;
                yield return classComboBox;
                yield return langComboBox;
                yield return special1ComboBox;
                yield return special2ComboBox;
                yield return scienceComboBox;
            }
        }

        // Buttons enumerator. Mon1 -> Mon2 -> ...
        private IEnumerable<Button> Buttons
        {
            get
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
        }


        #endregion

        private string[,] SetArrayByClass()
        {
            var ret = @class switch
            {
                1 => timeTable.Class1.Clone() as string[,],
                2 => timeTable.Class2.Clone() as string[,],
                3 => timeTable.Class3.Clone() as string[,],
                4 => timeTable.Class4.Clone() as string[,],
                5 => timeTable.Class5.Clone() as string[,],
                6 => timeTable.Class6.Clone() as string[,],
                7 => timeTable.Class7.Clone() as string[,],
                8 => timeTable.Class8.Clone() as string[,],
                _ => throw new DataAccessException($"SetArrayByClass(): @class: 1~8 expected, but given {@class}.")
            };
            if (ret is null)
                throw new NullReferenceException($"SetArrayByClass(): Class{@class}.Clone() got null.");

            return ret;
        }

        #region ComboBox
        private void EnableAllCombobox()
        => Enable(langComboBox, special1ComboBox, special2ComboBox, scienceComboBox);

        private void gradeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (gradeComboBox.SelectedItem is not string strgrade)
                return;

            SaveData.GradeComboBoxText = strgrade;
            grade = SaveData.GradeComboBoxText[6] - '0';
            Enable(classComboBox);
            /*
            else
            {
                ShowMessage($"Sorry, Grade {grade} has not been implemented yet.", MessageTitle.FeatureNotImplemented);
                Empty(gradeComboBox);
                Disable(classComboBox);
            }*/
        }
        

        private void classComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (classComboBox.SelectedItem is null)
                return;
            
            EnableAllCombobox();
            Subjects.Clear();

            // Get & Set Class
            SaveData.ClassComboBoxText = classComboBox.SelectedItem as string
                ?? throw new NullReferenceException("classComboBox.SelectedItem is null.");
            @class = SaveData.ClassComboBoxText[6] - '0';
            timeTable.ResetByClass(@class);
            SetComboBoxAsClass();
            DrawTimeTable();
        }

        // TODO: 이걸 아예 TimeTable.cs 의 ResetByClass() 와 합칠까?
        // 그래서 관할은 그쪽에서 하고, 여기선 tuple 반환값으로 채우기만 하게...
        // 값 넣고 -> Disable(comboBox) -> Empty(comboBox)
        private void SetComboBoxAsClass()
        {
            void SetComboBoxAsClass1()
            {
                scienceComboBox.SelectedIndex = 1;
                langComboBox.SelectedIndex = 0;
                Disable(scienceComboBox, langComboBox);
                Empty(special1ComboBox, special2ComboBox);
            }
            void SetComboBoxAsClass2()
            {
                Empty(special1ComboBox, special2ComboBox, scienceComboBox, langComboBox);
            }
            void SetComboBoxAsClass3()
            {
                Empty(special1ComboBox, special2ComboBox, scienceComboBox, langComboBox);
            }
            void SetComboBoxAsClass4()
            {
                langComboBox.SelectedIndex = 1;
                Disable(langComboBox);
                Empty(special1ComboBox, special2ComboBox, scienceComboBox);
            }
            void SetComboBoxAsClass5()
            {
                langComboBox.SelectedIndex = 0;
                Disable(langComboBox);
                Empty(special1ComboBox, special2ComboBox, scienceComboBox);
            }
            void SetComboBoxAsClass6()
            {
                scienceComboBox.SelectedIndex = 1;
                langComboBox.SelectedIndex = 0;
                Disable(scienceComboBox, langComboBox);
                Empty(special1ComboBox, special2ComboBox);
            }
            void SetComboBoxAsClass7()
            {
                Empty(special1ComboBox, special2ComboBox, scienceComboBox, langComboBox);
            }
            void SetComboBoxAsClass8()
            {
                Empty(special1ComboBox, special2ComboBox, scienceComboBox, langComboBox);
            }
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
            if (langComboBox.SelectedItem is string language)
            {
                SaveData.LangComboBoxText = Subjects.Languages.Selected = language
                    ?? throw new NullReferenceException("langComboBox.SelectedItem is null.");
                DrawTimeTable();
            }
        }

        private void special1ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (special1ComboBox.SelectedItem is null)
                return;
            SaveData.Special1ComboBoxText = special1ComboBox.SelectedItem as string
                ?? throw new NullReferenceException("special1ComboBox.SelectedItem is null.");

            // switch expression is not available -- not a constant
            if (SaveData.Special1ComboBoxText == Subjects.RawName.GlobalEconomics || SaveData.Special1ComboBoxText == Subjects.RawName.GlobalPolitics
                || SaveData.Special1ComboBoxText == Subjects.RawName.CompareCulture || SaveData.Special1ComboBoxText == Subjects.RawName.EasternHistory)
                Subjects.Specials1.Selected = SaveData.Special1ComboBoxText;
            else if (SaveData.Special1ComboBoxText == Subjects.RawName.HistoryAndCulture)
                Subjects.Specials1.Selected = Subjects.Specials1.HistoryAndCulture;
            else if (SaveData.Special1ComboBoxText == Subjects.RawName.PoliticsPhilosophy)
                Subjects.Specials1.Selected = Subjects.Specials1.PoliticsPhilosophy;
            else if (SaveData.Special1ComboBoxText == Subjects.RawName.RegionResearch)
                Subjects.Specials1.Selected = Subjects.Specials1.RegionResearch;
            else if (SaveData.Special1ComboBoxText == Subjects.RawName.GISAnalyze)
                Subjects.Specials1.Selected = Subjects.Specials1.GISAnalyze;

            DrawTimeTable();
        }

        private void special2ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (special2ComboBox.SelectedItem is null)
                return;
            SaveData.Special2ComboBoxText = special2ComboBox.SelectedItem as string
                ?? throw new NullReferenceException("special2ComboBox.SelectedItem is null.");

            // switch expression is not available -- not a constant
            if (SaveData.Special2ComboBoxText == Subjects.RawName.GlobalEconomics || SaveData.Special2ComboBoxText == Subjects.RawName.GlobalPolitics
                || SaveData.Special2ComboBoxText == Subjects.RawName.CompareCulture || SaveData.Special2ComboBoxText == Subjects.RawName.EasternHistory)
                Subjects.Specials2.Selected = SaveData.Special2ComboBoxText;
            else if (SaveData.Special2ComboBoxText == Subjects.RawName.HistoryAndCulture)
                Subjects.Specials2.Selected = Subjects.Specials2.HistoryAndCulture;
            else if (SaveData.Special2ComboBoxText == Subjects.RawName.PoliticsPhilosophy)
                Subjects.Specials2.Selected = Subjects.Specials2.PoliticsPhilosophy;
            else if (SaveData.Special2ComboBoxText == Subjects.RawName.RegionResearch)
                Subjects.Specials2.Selected = Subjects.Specials2.RegionResearch;
            else if (SaveData.Special2ComboBoxText == Subjects.RawName.GISAnalyze)
                Subjects.Specials2.Selected = Subjects.Specials2.GISAnalyze;

            DrawTimeTable();
        }

        private void scienceComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (scienceComboBox.SelectedItem is null)
                return;
            SaveData.ScienceComboBoxText = scienceComboBox.SelectedItem as string
                ?? throw new NullReferenceException("scienceComboBox.SelectedItem is null.");

            if (SaveData.ScienceComboBoxText == Subjects.RawName.ScienceHistory)
                Subjects.Sciences.Selected = SaveData.ScienceComboBoxText;
            else if (SaveData.ScienceComboBoxText == Subjects.RawName.LifeAndScience)
                Subjects.Sciences.Selected = Subjects.Sciences.LifeAndScience;

            DrawTimeTable();
        }
        #endregion


        private async Task ShowSubjectZoom(string subjectCellName)
        {
            if (subjectCellName is null)
            {
                await ShowMessageAsync("Please select your grade and class first.", "Error");
                return;
            }

            /*
            if (@class is not (3 or 4 or 5 or 6 or 8))
            {
                await ShowMessageAsync($"Sorry, displaying Zoom link is currently not available in class {@class}.\n" + 
                    "Please wait until the update will be underway.", MessageTitle.FeatureNotImplemented);
                return;
            }*/

            if (SaveData.IsActivated is false)
            {
                ActivateDialog activateDialog = new();
                var activeSelection = await activateDialog.ShowAsync();

                // 인증을 하지 않았다면 return
                if (activeSelection is not ContentDialogResult.Primary || SaveData.IsActivated is false)
                    return;
            }

            if (GetClassZoomLink().TryGetValue(subjectCellName, out ZoomInfo zoomInfo) is false || (zoomInfo is null))
            {
                // TODO: 선택과목 클릭했을 때는 알림을 조금 다르게...
                await ShowMessageAsync($"Zoom Link for {subjectCellName} is currently not available.\n" + "개발자에게 줌 링크 추가를 요청해보세요.", "No Data for Zoom Link");
                return;
            }

            ZoomDialog contentDialog = new(@class, subjectCellName, zoomInfo);
            await contentDialog.ShowAsync();
        }

        private Dictionary<string, ZoomInfo> GetClassZoomLink() => @class switch
        {
            3 => zoomLink.Class3,
            4 => zoomLink.Class4,
            5 => zoomLink.Class5,
            6 => zoomLink.Class6,
            8 => zoomLink.Class8,
            _ => throw new DataAccessException(
                $"GetClassZoomLink(): Class out of range: 3, 4, 5, 8, 6 expected, but given {@class}")
        };

        private void TableButtons_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.Content is string cellName)
                _ = ShowSubjectZoom(cellName); 
        }

        private async void SpecialButtons_Click(object sender, RoutedEventArgs _)
        {
            if (sender is Button btn)
            {
                var (msg, txt) = btn.Name switch
                {
                    "fri5Button" or "fri6Button" => ("각자 정규동아리 부장들에게 문의해주세요.", "정규동아리 활동 시간"),
                    "mon6Button" or "mon7Button" => ("창의적 체험활동 시간입니다.", "창의적 체험활동"),
                    "fri7Button" => ("즐거운 홈커밍 데이 :)", "Homecoming"),
                    _ => throw new TableCellException($"SpecialButtons_Click(): No candidate to show for button '{btn.Name}'")
                };
                await ShowMessageAsync(msg, txt);
            }
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            comboBoxSelection = (gradeComboBox.SelectedIndex, classComboBox.SelectedIndex, langComboBox.SelectedIndex,
                special1ComboBox.SelectedIndex, special2ComboBox.SelectedIndex, scienceComboBox.SelectedIndex);
            _ = Frame.Navigate(typeof(SettingsPage));
        }
    }
}
