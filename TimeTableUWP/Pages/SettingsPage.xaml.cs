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

namespace TimeTableUWP.Pages
{
    public sealed partial class SettingsPage : Page
    {
        public Dictionary<DateType, int> DateFormatDict { get; } = new()
        {
            [DateType.YYYYMMDD] = 0,
            [DateType.YYYYMMDD2] = 1,
            [DateType.MMDDYYYY] = 2,
        };
        private bool selfToggled = false;

        public SettingsPage()
        {
            InitializeComponent();
            use24Toggle.IsOn = Info.Settings.Use24Hour;
            RequestedTheme = Info.Settings.Theme; // TODO: 이거 Settings로 끌어들이기
            dateFormatRadio.SelectedIndex = DateFormatDict[Info.Settings.DateFormat];
            colorPicker.Color = Info.Settings.ColorType;
            SilentToggle.IsOn = Info.Settings.SilentMode;
            SetDarkToggle(Info.Settings.IsDarkMode);
        }

        private void ToggleSwitch_Toggled(object _, RoutedEventArgs e) => Info.Settings.Use24Hour = use24Toggle.IsOn;

        private void dateFormatRadio_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is muxc.RadioButtons rb && rb.SelectedItem is string selected)
            {
                Info.Settings.DateFormat = selected switch
                {
                    "2020/06/03" => DateType.YYYYMMDD,
                    "2020-06-03" => DateType.YYYYMMDD2,
                    "06/03/2020" => DateType.MMDDYYYY,
                    _ => throw new DataAccessException($"dateFormatRadio_SelectionChanged(): SelectedItem is '{selected}'.")
                };
            }
        }

        private void ColorPicker_ColorChanged(muxc.ColorPicker sender, muxc.ColorChangedEventArgs args) 
            => Info.Settings.ColorType = colorPicker.Color;

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Hyperlink hyperlink = new() { NavigateUri = new("https://blog.naver.com/nsun527") };
            hyperlink.Inlines.Add(new Run() { Text = "개발자 블로그 1 (네이버: 카루)" });

            Hyperlink hyperlink2 = new() { NavigateUri = new("https://rress.tistory.com") };
            hyperlink2.Inlines.Add(new Run() { Text = "개발자 블로그 2 (티스토리: Rolling Ress)" });

            TextBlock tb = new();
            tb.Inlines.Add(new Run() { Text = Messages.About });
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
                RequestedTheme = Info.Settings.IsDarkMode ? ElementTheme.Dark : ElementTheme.Light
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

            if (await TimeTablePage.AuthorAsync() is false)
            {
                SetDarkToggle(false);
                return;
            }
            
            Info.Settings.IsDarkMode = darkToggle.IsOn;
            RequestedTheme = Info.Settings.IsDarkMode ? ElementTheme.Dark : ElementTheme.Light;
        }

        void SetDarkToggle(bool value)
        {
            if (darkToggle.IsOn == value)
                return;
            selfToggled = true;
            darkToggle.IsOn = value;
        }

        private async void Button_Click_4(object sender, RoutedEventArgs e)
        => await TimeTablePage.ActivateAsync("인증 레벨을 바꾸고 싶으신가요?");

        private void SilentToggle_Toggled(object sender, RoutedEventArgs e)
        => Info.Settings.SilentMode = SilentToggle.IsOn;

        private async void Button_Click_5(object sender, RoutedEventArgs e)
        {
            ContentMessageDialog dialog = new(Messages.Troubleshoot, "Troubleshoot");
            await dialog.ShowAsync();
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        => colorPicker.Color = Info.Settings.ColorType = Settings.DefaultColor;
    }
}
