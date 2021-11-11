#nullable enable

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

using GGHS;
using GGHS.Grade2.Semester2;

using static RollingRess.StaticClass;
using System.Diagnostics.CodeAnalysis;

namespace TimeTableUWP.Pages
{

    public sealed partial class TimeTablePage : Page
    {
        public enum LoadStatus
        {
            NewUser,
            Updated,
            Default
        }
        public static LoadStatus Status { get; set; }

        private int @class = 8;
        private DateTime Now { get; set; } = DateTime.Now;
        private TimeTables TimeTable => new();
        private ZoomLinks ZoomLink => new();

        /// <summary>
        /// Current ComboBox values
        /// </summary>
        public static (int grade, int @class, int lang, int special1, int special2, int science) ComboBoxSelection { get; set; } = (0, -1, -1, -1, -1, -1);

        public TimeTablePage()
        {
            InitializeComponent();
            RequestedTheme = MainPage.Theme;

            _ = LoopTimeAsync(); // detach.

            if (Status is not LoadStatus.NewUser)
            {
                SaveData.SetClass(ref @class);
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
            if (classComboBox.SelectedItem is not string ccb)
                return;

            EnableAllCombobox();
            Subjects.Clear();

            // Get & Set Class
            SaveData.ClassComboBoxText = ccb;
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
                SaveData.LangComboBoxText = Subjects.Languages.Selected = language;
                DrawTimeTable();
            }
        }

        private void special1ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (special1ComboBox.SelectedItem is not string special1)
                return;

            SaveData.Special1ComboBoxText = special1;
            Subjects.Specials1.Selected = SaveData.Special1ComboBoxText switch
            {
                SubjectsFullNames.GlobalEconomics or
                SubjectsFullNames.GlobalPolitics or
                SubjectsFullNames.CompareCulture or
                SubjectsFullNames.EasternHistory => SaveData.Special1ComboBoxText,

                SubjectsFullNames.HistoryAndCulture => Subjects.Specials1.HistoryAndCulture,
                SubjectsFullNames.PoliticsPhilosophy => Subjects.Specials1.PoliticsPhilosophy,
                SubjectsFullNames.RegionResearch => Subjects.Specials1.RegionResearch,
                _ => throw new TimeTableException($"special1ComboBox_SelectionChanged: No candidate for {SaveData.Special1ComboBoxText}."),
            };
            DrawTimeTable();
        }

        private void special2ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (special2ComboBox.SelectedItem is not string special2)
                return;

            SaveData.Special2ComboBoxText = special2;
            Subjects.Specials2.Selected = SaveData.Special2ComboBoxText switch
            {
                SubjectsFullNames.GlobalEconomics or
                SubjectsFullNames.GlobalPolitics or
                SubjectsFullNames.CompareCulture or
                SubjectsFullNames.EasternHistory => SaveData.Special2ComboBoxText,

                SubjectsFullNames.HistoryAndCulture => Subjects.Specials2.HistoryAndCulture,
                SubjectsFullNames.PoliticsPhilosophy => Subjects.Specials2.PoliticsPhilosophy,
                SubjectsFullNames.GISAnalyze => Subjects.Specials2.GISAnalyze,
                _ => throw new TimeTableException($"special2ComboBox_SelectionChanged: No candidate for {SaveData.Special2ComboBoxText}."),
            };
            DrawTimeTable();
        }

        private void scienceComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (scienceComboBox.SelectedItem is not string science)
                return;

            SaveData.ScienceComboBoxText = science;
            Subjects.Sciences.Selected = SaveData.ScienceComboBoxText;

            DrawTimeTable();
        }
        #endregion


        private async Task ShowSubjectZoom(string subjectCellName)
        {
            if (SaveData.IsActivated is false)
            {
                // 인증을 하지 않았다면 return
                if (await ActivateAsync() is false)
                    return;

                SetSubText();
            }
            
            if (subjectCellName is "비교문화")
            {
                CompareCultureDialog dialog = new(@class);
                await dialog.ShowAsync();
                return;
            }

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
                ActivateLevel.ShareTech => "ShareTech",
                ActivateLevel.None or _ => throw new DataAccessException("MainPage.Activate(): ActivateLevel value error"),
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

        private async void TableButtons_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn)
            {
                if (btn.Content is string cellName)
                    await ShowSubjectZoom(cellName);
                
                else if (btn.Content is null)
                    await ShowMessageAsync("Please select your class first.", "Error", MainPage.Theme);
            }
        }

        private async void SpecialButtons_Click(object sender, RoutedEventArgs _)
        {
            if (sender is Button btn)
            {
                var (msg, txt) = btn.Name switch
                {
                    "fri5Button" or "fri6Button" => ("인문학 울림 또는 창의진로프로젝트 시간입니다.", "창의적 체험활동"),
                    "mon6Button" or "mon7Button" => ("인문학 울림 또는 창의진로프로젝트 시간입니다.", "창의적 체험활동"),
                    "fri7Button" => ("즐거운 홈커밍 데이 :)", "Homecoming"),
                    _ => throw new TableCellException($"SpecialButtons_Click(): No candidate to show for button '{btn.Name}'")
                };
                await ShowMessageAsync(msg, txt, MainPage.Theme);
            }
        }
    }
}
