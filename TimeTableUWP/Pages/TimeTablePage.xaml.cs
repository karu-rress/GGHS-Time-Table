#nullable enable

using TimeTableCore.Grade3.Semester1;

namespace TimeTableUWP.Pages;
public sealed partial class TimeTablePage : Page
{
    private TimeTables TimeTable { get; } = new();
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
            classComboBox.SelectedIndex = Info.User.Class - 1;
            if (Korean.Selected != Korean.Default)
                korComboBox.SelectedItem = Subjects.Korean.FullName;

            if (ttc::Math.Selected != ttc::Math.Default)
                mathComboBox.SelectedItem = Subjects.Math.FullName;

            if (Social.Selected != Social.Default)
                socialComboBox.SelectedItem = Subjects.Social.FullName;

            if (ttc::Language.Selected != ttc::Language.Default)
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
    private IEnumerable<Border> DayBorders
    {
        get
        {
            yield return monBorder;
            yield return tueBorder;
            yield return wedBorder;
            yield return thuBorder;
            yield return friBorder;
        }
    }

    private IEnumerable<ComboBox> ComboBoxes
    {
        get
        {
            yield return classComboBox;
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

    private void EnableAllCombobox()
    => Enable(korComboBox, mathComboBox, socialComboBox, langComboBox, global1ComboBox, global2ComboBox);

    // TODO: need ref?
    private void DisableComboBoxByClass(ComboBox cb, Subject subject)
    {
        cb.Text = subject.FullName;
        Disable(cb);
    }

    // 값 넣고 -> Disable(comboBox) -> Empty(comboBox)
    private void DisableComboBoxBySubjects()
    {
        if (TimeTable.Table is null)
            return; // Class is not yet selected, or a bug.

        EnableAllCombobox();
        if (TimeTable.Table.CommonSubject.HasFlag(Common.Korean))
            DisableComboBoxByClass(korComboBox, Korean.Selected);

        if (TimeTable.Table.CommonSubject.HasFlag(Common.Math))
            DisableComboBoxByClass(mathComboBox, ttc::Math.Selected);

        if (TimeTable.Table.CommonSubject.HasFlag(Common.Social))
            DisableComboBoxByClass(socialComboBox, Social.Selected);

        if (TimeTable.Table.CommonSubject.HasFlag(Common.Language))
            DisableComboBoxByClass(langComboBox, ttc::Language.Selected);

        if (TimeTable.Table.CommonSubject.HasFlag(Common.Global1))
            DisableComboBoxByClass(global1ComboBox, Global1.Selected);

        if (TimeTable.Table.CommonSubject.HasFlag(Common.Global2))
            DisableComboBoxByClass(global2ComboBox, Global2.Selected);
    }

    private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs _)
    {
        if (sender is not ComboBox comboBox || comboBox.SelectedItem is not string selected)
            return;

        switch (comboBox.Name)
        {
            case "classComboBox":
                EnableAllCombobox();
                // TODO: 여기서 초기화하면 시작시에도 날아간다...? 해결좀.
                Subjects.ResetSelectiveSubjects();

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
            if (await User.ActivateAsync() is false)
                return;

            SetSubText();
        }
        
        if (GetClassZoomLink().TryGetValue(subjectCellName, out var online) is false || (online is null))
        {
            // TODO: 선택과목 클릭했을 때는 알림을 조금 다르게...
            await ShowMessageAsync($"Links for {subjectCellName} is currently not available.\n"
                + "카루에게 줌 링크 추가를 요청해보세요.", "No data", Info.Settings.Theme);
            return;
        }

        ZoomDialog contentDialog = new(Info.User.Class, subjectCellName, online);
        await contentDialog.ShowAsync();
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
        
        (string msg, string txt) = btn.Name switch
        {
            "fri5Button" or "fri6Button" => ("창의적 체험활동 시간입니다.", "창의적 체험활동"),
            "mon6Button" or "mon7Button" => ("창의적 체험활동 시간입니다.", "창의적 체험활동"),
            "fri7Button" => ("즐거운 홈커밍 데이 :)", "Homecoming"),
            _ => throw new TableCellException($"SpecialButtons_Click(): No candidate to show for button '{btn.Name}'")
        };
        await ShowMessageAsync(msg, txt, Info.Settings.Theme);
    }
}
