#nullable enable

using Windows.UI.Xaml.Documents;
using Windows.System;
using RollingRess.UWP.UI;

namespace TimeTableUWP.Pages;

public sealed partial class SettingsPage : Page
{
    public Dictionary<DateType, int> DateFormatDict { get; } = new()
    {
        [DateType.YYYYMMDD] = 0,
        [DateType.YYYYMMDD2] = 1,
        [DateType.MMDDYYYY] = 2,
    };

    private IEnumerable<Button> buttons
    {
        get
        {
            yield return aboutButton;
            yield return feedbackButton;
            yield return activateButton;
            yield return troubleButton;
            yield return farewellButton;
        }
    }

    public SettingsPage()
    {
        InitializeComponent();
        use24Toggle.IsOn = Info.Settings.Use24Hour;
        dateFormatRadio.SelectedIndex = DateFormatDict[Info.Settings.DateFormat];
        colorPicker.Color = Info.Settings.ColorType;
        SetDarkToggle(Info.Settings.IsDarkMode);

        SetButtonColor();
    }

    private void SetButtonColor()
    {
        foreach (Button button in buttons)
            button.BorderBrush = Info.Settings.Brush;
    }

    private void use24Toggle_Toggled(object _, RoutedEventArgs e) => Info.Settings.Use24Hour = use24Toggle.IsOn;

    private async void DarkToggleSwitch_Toggled(object _, RoutedEventArgs __)
    {
        if (await User.AuthorAsync() is false)
        {
            SetDarkToggle(false);
            return;
        }

        Info.Settings.IsDarkMode = darkToggle.IsOn;
        AppStyle.SetTheme(Info.Settings.Theme);
    }

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

    private void ResetColor_Click(object sender, RoutedEventArgs e)
=> colorPicker.Color = Info.Settings.ColorType = Settings.DefaultColor;

    private async void aboutButton_Click(object sender, RoutedEventArgs e)
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
            RequestedTheme = Info.Settings.Theme,

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

    private async void feedbackButton_Click(object sender, RoutedEventArgs e)
    {
        FeedbackDialog feedbackDialog = new();
        await feedbackDialog.ShowAsync();
    }

    private async void activateButton_Click(object sender, RoutedEventArgs e)
=> await User.ActivateAsync("인증 레벨을 바꾸고 싶으신가요?");

    private async void troubleButton_Click(object sender, RoutedEventArgs e)
=> await ShowMessageAsync(string.Format(Messages.Troubleshoot), "Troubleshoot", Info.Settings.Theme);

    private void SetDarkToggle(bool value)
    {
        if (darkToggle.IsOn == value)
            return;

        RoutedEventHandler handler = new(DarkToggleSwitch_Toggled);
        darkToggle.Toggled -= handler;
        darkToggle.IsOn = value;
        darkToggle.Toggled += handler;
    }

    private async void farewellButton_Click(object sender, RoutedEventArgs e)
    {
         ContentDialog contentDialog = new()
        {
            Title = "Farewell to GTT",
            Content = @"환영합니다, Rolling Ress의 카루입니다.

본 화면을 캡처하거나 스마트폰으로 찍어
저의 개인톡 또는 인스타 @karu.rress로 보내주세요.

이벤트에 대한 설명은 인스타그램을 참고해주세요!
많은 참여 부탁드립니다 😊",
            CloseButtonText = "닫기",
            DefaultButton = ContentDialogButton.Primary,
            RequestedTheme = Info.Settings.Theme
        };
        await contentDialog.ShowAsync();
    }
}