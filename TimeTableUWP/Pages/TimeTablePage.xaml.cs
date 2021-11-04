﻿#nullable enable

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using GGHS;
using GGHS.Grade2.Semester2;
using static RollingRess.StaticClass;
// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace TimeTableUWP.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TimeTablePage : Page
    {
        private int grade;
        private int @class = 8;

        private DateTime Now { get; set; } = DateTime.Now;

        public enum LoadStatus
        {
            NewUser,
            Updated,
            Default
        }

        public static LoadStatus Status { get; set; }
        /// <summary>
        /// Current ComboBox values
        /// </summary>
        public static (int grade, int @class, int lang, int special1, int special2, int science) ComboBoxSelection { get; set; } = (0, -1, -1, -1, -1, -1);

        private TimeTables TimeTable => new();
        private ZoomLinks ZoomLink => new();

        public TimeTablePage()
        {
            InitializeComponent();
            RequestedTheme = MainPage.Theme;

            _ = LoopTimeAsync(); // detach.

            if (Status is not LoadStatus.NewUser)
            {
                SaveData.SetGradeAndClass(ref grade, ref @class);
                SetComboBoxAsClass();
                SaveData.SetComboBoxes(ComboBoxes);
            }
            InitializeUI();
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
                1 => TimeTable.Class1.Clone() as string[,],
                2 => TimeTable.Class2.Clone() as string[,],
                3 => TimeTable.Class3.Clone() as string[,],
                4 => TimeTable.Class4.Clone() as string[,],
                5 => TimeTable.Class5.Clone() as string[,],
                6 => TimeTable.Class6.Clone() as string[,],
                7 => TimeTable.Class7.Clone() as string[,],
                8 => TimeTable.Class8.Clone() as string[,],
                _ => throw new DataAccessException($"SetArrayByClass(): @class: 1~8 expected, but given {@class}.")
            };
            if (ret is null)
                throw new NullReferenceException($"SetArrayByClass(): Class{@class}.Clone() got null.");

            return ret;
        }

        #region ComboBox
        private void EnableAllCombobox()
        => Enable(langComboBox, special1ComboBox, special2ComboBox, scienceComboBox);

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
            TimeTable.ResetByClass(@class);
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
            void SetComboBoxAsClass2() => Empty(special1ComboBox, special2ComboBox, scienceComboBox, langComboBox);
            void SetComboBoxAsClass3() => Empty(special1ComboBox, special2ComboBox, scienceComboBox, langComboBox);
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
            void SetComboBoxAsClass7() => Empty(special1ComboBox, special2ComboBox, scienceComboBox, langComboBox);
            void SetComboBoxAsClass8() => Empty(special1ComboBox, special2ComboBox, scienceComboBox, langComboBox);
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

            // switch is not available -- not a constant
            if (SaveData.Special1ComboBoxText == Subjects.RawName.GlobalEconomics || SaveData.Special1ComboBoxText == Subjects.RawName.GlobalPolitics
                || SaveData.Special1ComboBoxText == Subjects.RawName.CompareCulture + "A" || SaveData.Special1ComboBoxText == Subjects.RawName.EasternHistory)
                Subjects.Specials1.Selected = SaveData.Special1ComboBoxText;
            else if (SaveData.Special1ComboBoxText == Subjects.RawName.HistoryAndCulture)
                Subjects.Specials1.Selected = Subjects.Specials1.HistoryAndCulture;
            else if (SaveData.Special1ComboBoxText == Subjects.RawName.PoliticsPhilosophy)
                Subjects.Specials1.Selected = Subjects.Specials1.PoliticsPhilosophy;
            else if (SaveData.Special1ComboBoxText == Subjects.RawName.RegionResearch)
                Subjects.Specials1.Selected = Subjects.Specials1.RegionResearch;

            DrawTimeTable();
        }

        private void special2ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (special2ComboBox.SelectedItem is null)
                return;
            SaveData.Special2ComboBoxText = special2ComboBox.SelectedItem as string
                ?? throw new NullReferenceException("special2ComboBox.SelectedItem is null.");

            // switch is not available -- not a constant
            if (SaveData.Special2ComboBoxText == Subjects.RawName.GlobalEconomics || SaveData.Special2ComboBoxText == Subjects.RawName.GlobalPolitics
                || SaveData.Special2ComboBoxText == Subjects.RawName.CompareCulture + "B" || SaveData.Special2ComboBoxText == Subjects.RawName.EasternHistory)
                Subjects.Specials2.Selected = SaveData.Special2ComboBoxText;
            else if (SaveData.Special2ComboBoxText == Subjects.RawName.HistoryAndCulture)
                Subjects.Specials2.Selected = Subjects.Specials2.HistoryAndCulture;
            else if (SaveData.Special2ComboBoxText == Subjects.RawName.PoliticsPhilosophy)
                Subjects.Specials2.Selected = Subjects.Specials2.PoliticsPhilosophy;
            //else if (SaveData.Special2ComboBoxText == Subjects.RawName.RegionResearch)
            //Subjects.Specials2.Selected = Subjects.Specials2.RegionResearch;
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
                await ShowMessageAsync("Please select your grade and class first.", "Error", MainPage.Theme);
                return;
            }

            if (SaveData.IsActivated is false)
            {
                // 인증을 하지 않았다면 return
                if (await ActivateAsync() is false)
                    return;
            }
            SetSubText();
            if (GetClassZoomLink().TryGetValue(subjectCellName, out var zoomInfo) is false || (zoomInfo is null))
            {
                // TODO: 선택과목 클릭했을 때는 알림을 조금 다르게...
                await ShowMessageAsync($"Zoom Link for {subjectCellName} is currently not available.\n"
                    + "카루에게 줌 링크 추가를 요청해보세요.", "No Data for Zoom Link", MainPage.Theme);
                return;
            }

            ZoomDialog contentDialog = new(@class, subjectCellName, zoomInfo);
            await contentDialog.ShowAsync();
        }

        /// <summary>
        /// Shows activation dialog and activate.
        /// </summary>
        /// <param name="msg">The first line showing in activation dialog. If null is given, then shows defualt message</param>
        /// <returns>true if activated. Otherwise, false</returns>
        public static async Task<bool> ActivateAsync(string? msg = null)
        {
            ActivateDialog activateDialog = msg is null ? new() : new(msg);
            var activeSelection = await activateDialog.ShowAsync();

            if (activeSelection is not ContentDialogResult.Primary || SaveData.IsActivated is false)
                return false;

            string license = SaveData.ActivateStatus switch
            {
                ActivateLevel.Developer => "developer",
                ActivateLevel.Grade2 => "GGHS 10th",
                ActivateLevel.Insider => "GTT Insider",
                _ => throw new Exception("MainPage.Activate(): ActivateLevel value error"),
            };
            await ShowMessageAsync($"Activated as {license}.", "Activated successfully", MainPage.Theme);
            return true;
        }

        private Dictionary<string, ZoomInfo?> GetClassZoomLink() => @class switch
        {
            1 => ZoomLink.Class1,
            2 => ZoomLink.Class2,
            3 => ZoomLink.Class3,
            4 => ZoomLink.Class4,
            5 => ZoomLink.Class5,
            6 => ZoomLink.Class6,
            7 => ZoomLink.Class7,
            8 => ZoomLink.Class8,
            _ => throw new DataAccessException(
                $"GetClassZoomLink(): Class out of range: 1 to 8 expected, but given {@class}")
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
                await ShowMessageAsync(msg, txt, MainPage.Theme);
            }
        }
    }
}