#nullable enable

using System;
using System.Collections.Generic;

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Documents;
using Windows.System;

using muxc = Microsoft.UI.Xaml.Controls;
using static RollingRess.StaticClass;
using RollingRess;
using Windows.Storage;
using Windows.UI;

namespace TimeTableUWP.Pages
{
    public sealed partial class SettingsPage : Page
    {
        public static Color DefaultColor { get; } = Color.FromArgb(0xEE, 0xD9, 0xC0, 0xF9);
        public static bool Use24Hour { get; private set; } = false;
        public static bool IsDarkMode { get; private set; } = false;
        public static DateType DateFormat { get; private set; } = DateType.YYYYMMDD;
        private static bool selfToggled = false;

        private Dictionary<DateType, int> DateFormatDict { get; } = new()
        {
            [DateType.YYYYMMDD] = 0,
            [DateType.YYYYMMDD2] = 1,
            [DateType.MMDDYYYY] = 2,
        };
        public static bool SilentMode { get; private set; } = false;

        public SettingsPage()
        {
            InitializeComponent();
            use24Toggle.IsOn = Use24Hour;
            RequestedTheme = MainPage.Theme;
            dateFormatRadio.SelectedIndex = DateFormatDict[DateFormat];
            colorPicker.Color = SaveData.ColorType;
            SilentToggle.IsOn = SilentMode;
            SetDarkToggle(IsDarkMode);
        }

        private void ToggleSwitch_Toggled(object _, RoutedEventArgs e) => Use24Hour = use24Toggle.IsOn;

        private void dateFormatRadio_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is muxc.RadioButtons rb && rb.SelectedItem is string selected)
            {
                DateFormat = selected switch
                {
                    "2020/06/03" => DateType.YYYYMMDD,
                    "2020-06-03" => DateType.YYYYMMDD2,
                    "06/03/2020" => DateType.MMDDYYYY,
                    _ => throw new DataAccessException($"dateFormatRadio_SelectionChanged(): SelectedItem is '{selected}'.")
                };
            }
        }

        private void ColorPicker_ColorChanged(muxc.ColorPicker sender, muxc.ColorChangedEventArgs args) 
            => SaveData.ColorType = colorPicker.Color;

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Hyperlink hyperlink = new() { NavigateUri = new("https://blog.naver.com/nsun527") };
            hyperlink.Inlines.Add(new Run() { Text = "개발자 블로그 1 (네이버: 카루)" });

            Hyperlink hyperlink2 = new() { NavigateUri = new("https://rress.tistory.com") };
            hyperlink2.Inlines.Add(new Run() { Text = "개발자 블로그 2 (티스토리: Rolling Ress)" });

            TextBlock tb = new();
            tb.Inlines.Add(new Run()
            {
                Text = @$"GGHS Time Table V{MainPage.Version}

환영합니다, Rolling Ress의 카루입니다.

GGHS Time Table을 설치해주셔서 감사합니다.

자신의 선택과목을 선택하고, 시간표를 누르면 해당 시간의
줌 링크와 클래스룸 링크가 띄워집니다.

기능에 문제가 있거나, 줌 링크가 누락이 된 반 혹은 과목이 있다면
설정 창의 'Send Feedback' 버튼을 통해 제보해주시면 감사하겠습니다.

카루 블로그 링크:
" });
            tb.Inlines.Add(hyperlink);
            tb.Inlines.Add(new Run() { Text = "\n" });
            tb.Inlines.Add(hyperlink2);

            ContentDialog contentDialog = new()
            {
                Title = "About GGHS Time Table",
                Content = tb,
                PrimaryButtonText = "Open Naver Blog",
                SecondaryButtonText = "Open Tistory",
                CloseButtonText = "Close",
                DefaultButton = ContentDialogButton.Primary,
                RequestedTheme = IsDarkMode ? ElementTheme.Dark : ElementTheme.Light
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
            if (SaveData.IsNotDeveloperOrInsider)
                await TimeTablePage.ActivateAsync("Insider 전용 기능을 사용하기 위해선 Insider 인증키를 입력해야 합니다.");

            if (SaveData.IsNotDeveloperOrInsider)
            {
                await ShowMessageAsync("You need to be a GTT Insider to use this feature", "Limited feature", MainPage.Theme);
                SetDarkToggle(false);
                return;
            }
            
            IsDarkMode = darkToggle.IsOn;
            RequestedTheme = IsDarkMode ? ElementTheme.Dark : ElementTheme.Light;
        }

        void SetDarkToggle(bool value)
        {
            if (darkToggle.IsOn == value)
                return;
            selfToggled = true;
            darkToggle.IsOn = value;
        }

        private async void Button_Click_4(object sender, RoutedEventArgs e)
        {
            await TimeTablePage.ActivateAsync("인증 레벨을 바꾸고 싶으신가요?");
        }

        private void SilentToggle_Toggled(object sender, RoutedEventArgs e)
        {
            SilentMode = SilentToggle.IsOn;
        }

        private async void Button_Click_5(object sender, RoutedEventArgs e)
        {
            ContentMessageDialog dialog = new(
                "GGHS Time Table 4 사용중 문제가 발생했나요?\n\n" +
"오류가 난 경우 대부분 개발자에게 자동으로 보고되며, 별다른 조치를 취하실 필요가 없습니다. " +
"만약 오류 창이 뜨거나 경고 메시지가 뜬 경우 해당 화면을 캡처해서 제게 보내주시기 바랍니다. " +
"혹은, 'Send Feedback' 버튼을 통해 오류를 제보해주세요.\n\n" +
"선택과목 및 ZOOM 링크에 오류가 있는 경우에도 제보 부탁드립니다.\n"+
$"저장된 데이터가 유실된 경우 {ApplicationData.Current.LocalFolder.Path}폴더를 확인하세요.",
                "Troubleshoot");
            await dialog.ShowAsync();
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            colorPicker.Color = SaveData.ColorType = DefaultColor;
        }
    }
}
