#nullable enable

using Windows.UI.Xaml.Documents;
using Windows.System;

namespace TimeTableUWP.Pages;

public sealed partial class SettingsPage : Page
{
    public Dictionary<DateType, int> DateFormatDict { get; } = new()
    {
        [DateType.YYYYMMDD] = 0,
        [DateType.YYYYMMDD2] = 1,
        [DateType.MMDDYYYY] = 2,
    };
    private bool selfToggled = false;

    private IEnumerable<Button> buttons
    {
        get
        {
            yield return aboutButton;
            yield return feedbackButton;
            yield return activateButton;
            yield return troubleButton;
            yield return howtoButton;
        }
    }

    public SettingsPage()
    {
        InitializeComponent();
        use24Toggle.IsOn = Info.Settings.Use24Hour;
        RequestedTheme = Info.Settings.Theme;
        dateFormatRadio.SelectedIndex = DateFormatDict[Info.Settings.DateFormat];
        colorPicker.Color = Info.Settings.ColorType;
        SilentToggle.IsOn = Info.Settings.SilentMode;
        SetDarkToggle(Info.Settings.IsDarkMode);

        SetButtonColor();
    }

    private void SetButtonColor()
    {
        foreach (Button button in buttons)
            button.BorderBrush = new SolidColorBrush(Info.Settings.ColorType);
    }

    private void ToggleSwitch_Toggled(object _, RoutedEventArgs e) => Info.Settings.Use24Hour = use24Toggle.IsOn;

    private void dateFormatRadio_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (sender is not muxc::RadioButtons rb || rb.SelectedItem is not string selected)
            return;

        Info.Settings.DateFormat = selected switch
        {
            "2020/06/03" => DateType.YYYYMMDD,
            "2020-06-03" => DateType.YYYYMMDD2,
            "06/03/2020" => DateType.MMDDYYYY,
            _ => throw new DataAccessException($"dateFormatRadio_SelectionChanged(): SelectedItem is '{selected}'.")
        };
    }

    private void ColorPicker_ColorChanged(muxc::ColorPicker sender, muxc::ColorChangedEventArgs args)
    {
        Info.Settings.ColorType = colorPicker.Color;
        SetButtonColor();
    }

    private async void Button_Click_1(object sender, RoutedEventArgs e)
    {
        Hyperlink hyperlink = new() { NavigateUri = new("https://blog.naver.com/nsun527") };
        hyperlink.AddText("개발자 블로그 1 (네이버: 카루)");

        Hyperlink hyperlink2 = new() { NavigateUri = new("https://rress.tistory.com") };
        hyperlink2.AddText("개발자 블로그 2 (티스토리: Rolling Ress)");

        TextBlock tb = new();
        tb.AddText(Messages.About);
        tb.AddHyperlink(hyperlink);
        tb.AddTextLine();
        tb.AddHyperlink(hyperlink2);

        ContentDialog contentDialog = new()
        {
            Title = "About GGHS Time Table",
            Content = tb,
            PrimaryButtonText = "Open Naver Blog",
            SecondaryButtonText = "Open Tistory",
            CloseButtonText = "Close",
            DefaultButton = ContentDialogButton.Primary,
            RequestedTheme = Info.Settings.IsDarkMode ? ElementTheme.Dark : ElementTheme.Light,

        };

        switch (await contentDialog.ShowAsync())
        {
            case ContentDialogResult.Primary:
                await Launcher.LaunchUriAsync(new("https://blog.naver.com/nsun527"));
                return;
            case ContentDialogResult.Secondary:
                await Launcher.LaunchUriAsync(new("https://rress.tistory.com"));
                return;
        }
    }

    private async void Button_Click_2(object sender, RoutedEventArgs e)
    {
        FeedbackDialog feedbackDialog = new();
        await feedbackDialog.ShowAsync();
    }

    private async void DarkToggleSwitch_Toggled(object _, RoutedEventArgs __)
    {
        if (selfToggled)
        {
            selfToggled = false;
            return;
        }

        if (await User.AuthorAsync() is false)
        {
            SetDarkToggle(false);
            return;
        }

        Info.Settings.IsDarkMode = darkToggle.IsOn;
        RequestedTheme = Info.Settings.IsDarkMode ? ElementTheme.Dark : ElementTheme.Light;
    }

    private void SetDarkToggle(bool value)
    {
        if (darkToggle.IsOn == value)
            return;
        selfToggled = true;
        darkToggle.IsOn = value;
    }

    private async void Button_Click_4(object sender, RoutedEventArgs e)
    => await User.ActivateAsync("인증 레벨을 바꾸고 싶으신가요?");

    private void SilentToggle_Toggled(object sender, RoutedEventArgs e)
    => Info.Settings.SilentMode = SilentToggle.IsOn;

    private async void Button_Click_5(object sender, RoutedEventArgs e)
    => await ShowMessageAsync(string.Format(Messages.Troubleshoot), "Troubleshoot");

    private void Button_Click_6(object sender, RoutedEventArgs e)
    => colorPicker.Color = Info.Settings.ColorType = Settings.DefaultColor;

    private async void Button_Click_7(object sender, RoutedEventArgs e)
    {

        Hyperlink hyperlink = new() { NavigateUri = new("https://blog.naver.com/nsun527/222659315481") };
        hyperlink.AddText($"{Datas.GTTWithVer}의 자세한 사용법을 보려면 여기를 클릭하세요.");

        TextBlock tb = new();
        tb.AddHyperlink(hyperlink);
        tb.AddTextLine("\n\n- GGHS Time Table");
        tb.AddTextLine("자신의 반과 선택과목을 선택한 뒤, 시간표의 칸을 누르면");
        tb.AddTextLine("클래스룸 링크와 ZOOM 링크를 띄워줍니다. (ZOOM 링크 계획 미정)\n");

        tb.AddTextLine("- GGHS Todo");
        tb.AddTextLine("수행평가 일정 및 To do 리스트를 등록할 수 있습니다.");
        tb.AddTextLine("다양한 삭제 옵션을 사용할 수 있으며, 메인화면에서 각 항목을 클릭하거나");
        tb.AddTextLine("마우스 오른쪽을 클릭하면 수정 및 삭제를 할 수 있습니다.\n");

        tb.AddTextLine("- GGHS Anonymous");
        tb.AddTextLine("10기 전용 익명 채팅방입니다. Azure / Bisque의 경우 채팅이 가능하며,");
        tb.AddTextLine("Coral 레벨은 현재 공지 읽기만 가능합니다.");

        ContentDialog contentDialog = new()
        {
            Title = "How To Use",
            Content = tb,
            PrimaryButtonText = "자세한 사용법 보기",
            CloseButtonText = "닫기",
            DefaultButton = ContentDialogButton.Primary,
            RequestedTheme = Info.Settings.IsDarkMode ? ElementTheme.Dark : ElementTheme.Light,

        };

        if (await contentDialog.ShowAsync() is ContentDialogResult.Primary)
            await Launcher.LaunchUriAsync(new("https://blog.naver.com/nsun527/222659315481"));
    }
}