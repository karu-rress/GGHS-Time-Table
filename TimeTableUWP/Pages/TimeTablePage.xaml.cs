#nullable enable

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

using TimeTableCore.Grade3.Semester1;
using TimeTableCore;

using static RollingRess.StaticClass;
using System.Diagnostics.CodeAnalysis;
using GTT = TimeTableCore;

namespace TimeTableUWP.Pages
{


    public sealed class GTT5Attribute : Attribute { }
    public sealed partial class TimeTablePage : Page
    {
        [GTT5]
        private TimeTables TimeTable { get; } = new();
        public static DataSaver SaveData { get; } = new();
        private OnlineLinks Online { get; } = new();

        public TimeTablePage()
        {
            InitializeComponent();
            RequestedTheme = Info.Settings.Theme;
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            _ = LoopTimeAsync().ConfigureAwait(false); // detach.

            if (Info.User.Status is not LoadStatus.NewlyInstalled)
            {
                // TODO
                // 몇 반인지는 이미 Info.User.Class에 Load됨.

                // 선택과목 로드: 이거 아예 LoadAsync()에서 끝낼까봄.
                Subjects.Korean.SetAs(SaveData.Korean ?? Korean.Default);
                Subjects.Math.SetAs(SaveData.Math ?? TimeTableCore.Math.Default);
                Subjects.Social.SetAs(SaveData.Social ?? Social.Default);
                Subjects.Language.SetAs(SaveData.Language ?? TimeTableCore.Language.Default);
                Subjects.Global1.SetAs(SaveData.Global1 ?? Global1.Default);
                Subjects.Global2.SetAs(SaveData.Global2 ?? Global2.Default);

                // 콤보박스에 이전에 선택한 string 띄우기
                // classcombobox.selectedindex = info.user.class
                classComboBox.SelectedIndex = Info.User.Class - 1;
                if (Korean.Selected != Korean.Default)
                    korComboBox.SelectedItem = Subjects.Korean.FullName;

                if (GTT.Math.Selected != TimeTableCore.Math.Default)
                    mathComboBox.SelectedItem = Subjects.Math.FullName;

                if (Social.Selected != Social.Default)
                    socialComboBox.SelectedItem = Subjects.Social.FullName;

                if (GTT.Language.Selected != TimeTableCore.Language.Default)
                    langComboBox.SelectedItem = Subjects.Language.FullName;

                if (Global1.Selected != Global1.Default)
                    global1ComboBox.SelectedItem = Subjects.Global1.FullName;

                if (Global2.Selected != Global2.Default)
                    global2ComboBox.SelectedItem = Subjects.Global2.FullName;
                // 공통 콤보박스 제한
                DisableComboBoxBySubjects();
            }

            await InitializeUIAsync();
            if (Info.User.Class != 0)
                DrawTimeTable();
        }

        #region ENEMERATORS

        private IEnumerable<ComboBox> ComboBoxes
        {
            get
            {
                yield return korComboBox;
                yield return mathComboBox;
                yield return socialComboBox;
                yield return langComboBox;
                yield return global1ComboBox;
                yield return global2ComboBox;
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

        [GTT5]
        // Withtout class combobox
        private void EnableAllCombobox()
        => Enable(korComboBox, mathComboBox, socialComboBox, 
            langComboBox, global1ComboBox, global2ComboBox);

        // TODO: need ref?
        private void DisableComboBoxByClass(ComboBox cb, Subject subject)
        {
            cb.Text = subject.FullName;
            Disable(cb);
        }

        // 값 넣고 -> Disable(comboBox) -> Empty(comboBox)
        [GTT5]
        private void DisableComboBoxBySubjects()
        {
            if (TimeTable is null)
                throw new TimeTableException("SetComboBoxAsClass(): TimeTable is null.");

            if (TimeTable.Table is null)
                return; // Class is not yet selected, or a bug.

            EnableAllCombobox();
            if (TimeTable.Table.CommonSubject.HasFlag(Common.Korean))
                DisableComboBoxByClass(korComboBox, Korean.Selected);

            if (TimeTable.Table.CommonSubject.HasFlag(Common.Math))
                DisableComboBoxByClass(mathComboBox, GTT.Math.Selected);

            if (TimeTable.Table.CommonSubject.HasFlag(Common.Social))
                DisableComboBoxByClass(socialComboBox, Social.Selected);

            if (TimeTable.Table.CommonSubject.HasFlag(Common.Language))
                DisableComboBoxByClass(langComboBox, GTT.Language.Selected);

            if (TimeTable.Table.CommonSubject.HasFlag(Common.Global1))
                DisableComboBoxByClass(global1ComboBox, Global1.Selected);

            if (TimeTable.Table.CommonSubject.HasFlag(Common.Global2))
                DisableComboBoxByClass(global2ComboBox, Global2.Selected);
        }

        [GTT5]
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs _)
        {
            if (sender is not ComboBox comboBox || comboBox.SelectedItem is not string selected)
                return;

            switch (comboBox.Name)
            {
                case "classComboBox":
                    EnableAllCombobox();
                    Subjects.ResetSelectiveSubjects();

                    // SaveData.ClassComboBoxText = selected;
                    Info.User.Class = comboBox.SelectedIndex + 1;
                    TimeTable.ResetClass(Info.User.Class);
                    DisableComboBoxBySubjects();
                    break;

                case "korComboBox":
                    Subjects.Korean.SetAs(selected);
                    break;

                case "mathComboBox":
                    Subjects.Math.SetAs(selected);
                    break;

                case "socialComboBox": 
                    Subjects.Social.SetAs(selected);
                    break;

                case "langComboBox":
                    Subjects.Language.SetAs(selected);
                    break;

                case "global1ComboBox":
                    Subjects.Global1.SetAs(selected);
                    break;

                case "global2ComboBox":
                    Subjects.Global2.SetAs(selected);
                    break;
            }
            DrawTimeTable();
        }

        private async Task ShowSubjectZoom(string subjectCellName)
        {
            if (Info.User.ActivationLevel is ActivationLevel.None)
            {
                // 인증을 하지 않았다면 return
                if (await ActivateAsync() is false)
                    return;

                SetSubText();
            }
            
            if (GetClassZoomLink().TryGetValue(subjectCellName, out var online) is false || (online is null))
            {
                // TODO: 선택과목 클릭했을 때는 알림을 조금 다르게...
                await ShowMessageAsync($"Zoom Link for {subjectCellName} is currently not available.\n"
                    + "카루에게 줌 링크 추가를 요청해보세요.", "No Data for Zoom Link", Info.Settings.Theme);
                return;
            }

            ZoomDialog contentDialog = new(Info.User.Class, subjectCellName, online);
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

            if (activeSelection is not ContentDialogResult.Primary || Info.User.ActivationLevel is ActivationLevel.None)
                return false;

            string license = Info.User.ActivationLevel switch
            {
                ActivationLevel.Developer => "developer",
                ActivationLevel.Azure => "Azure",
                ActivationLevel.Bisque => "Bisque",
                ActivationLevel.Coral => "Coral",
                ActivationLevel.None or _ => throw new DataAccessException("MainPage.Activate(): ActivationLevel value error"),
            };
            await ShowMessageAsync($"Activated as {license}.", "Activated successfully", Info.Settings.Theme);
            return true;
        }

        private Dictionary<string, OnlineLink?> GetClassZoomLink() => Info.User.Class switch
        {
            1 => Online.Class1,
            2 => Online.Class2,
            3 => Online.Class3,
            4 => Online.Class4,
            5 => Online.Class5,
            6 => Online.Class6,
            7 => Online.Class7,
            8 => Online.Class8,
            _ => throw new DataAccessException(
                $"GetClassZoomLink(): Class out of range: 1 to 8 expected, but given {Info.User.Class}")
        };

        private async void TableButtons_Click(object sender, RoutedEventArgs e)
        {
            if (sender is not Button btn)           
                return;
            
            if (btn.Content is string cellName)
                await ShowSubjectZoom(cellName);

            else if (btn.Content is null)
                await ShowMessageAsync("Please select your class first.", "Error", Info.Settings.Theme);
        }

        private async void SpecialButtons_Click(object sender, RoutedEventArgs _)
        {
            if (sender is not Button btn)
                return;
            
            var (msg, txt) = btn.Name switch
            {
                "fri5Button" or "fri6Button" => ("창의적 체험활동 시간입니다.", "창의적 체험활동"),
                "mon6Button" or "mon7Button" => ("창의적 체험활동 시간입니다.", "창의적 체험활동"),
                "fri7Button" => ("즐거운 홈커밍 데이 :)", "Homecoming"),
                _ => throw new TableCellException($"SpecialButtons_Click(): No candidate to show for button '{btn.Name}'")
            };
            await ShowMessageAsync(msg, txt, Info.Settings.Theme);
        }

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {/*
            SaveData.SetSubjects(Korean.Selected, GTT.Math.Selected, GTT.Social.Selected,
    GTT.Language.Selected, GTT.Global1.Selected, GTT.Global2.Selected);
            SaveData.UserData = Info.User;
            */
        }
    }
}
