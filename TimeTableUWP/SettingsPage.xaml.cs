using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System.Threading;
using Windows.UI;
using System.Threading.Tasks;
using Windows.ApplicationModel.Email;
using Windows.ApplicationModel.Contacts;
using Windows.UI.Xaml.Documents;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace TimeTableUWP
{

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    /// 
    public sealed partial class SettingsPage : Page
    {
        public static bool Use24Hour { get; private set; } = false;
        public static DateType DateFormat { get; private set; } = DateType.YYYYMMDD;
        public static Color ColorType { get; private set; } = Colors.DarkSlateBlue;

        readonly Dictionary<DateType, int> dateFormatDict = new()
        {
            [DateType.YYYYMMDD] = 0,
            [DateType.YYYYMMDD2] = 1,
            [DateType.MMDDYYYY] = 2,
        };
        public SettingsPage()
        {
            this.InitializeComponent();
            use24Toggle.IsOn = Use24Hour;
            dateFormatRadio.SelectedIndex = dateFormatDict[DateFormat];
            colorPicker.Color = ColorType;
        }

        private void Button_Click(object sender, RoutedEventArgs e) => Frame.Navigate(typeof(MainPage));

        private void ToggleSwitch_Toggled(object sender, RoutedEventArgs e) => Use24Hour = use24Toggle.IsOn;

        private void dateFormatRadio_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is RadioButtons rb)
            {
                if (rb.SelectedItem is not string selected)
                    return;
                DateFormat = selected switch
                {
                    "2020/06/03" => DateType.YYYYMMDD,
                    "2020-06-03" => DateType.YYYYMMDD2,
                    "06/03/2020" => DateType.MMDDYYYY,
                    _ => throw new DataAccessException($"dateFormatRadio_SelectionChanged(): SelectedItem is '{selected}'.")
                };
            }
        }

        private void ColorPicker_ColorChanged(Microsoft.UI.Xaml.Controls.ColorPicker sender, Microsoft.UI.Xaml.Controls.ColorChangedEventArgs args) 
            => ColorType = colorPicker.Color;

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Hyperlink hyperlink = new() { NavigateUri = new("https://blog.naver.com/nsun527") };
            hyperlink.Inlines.Add(new Run() { Text = "개발자 블로그 1 (네이버: 카루)" });

            Hyperlink hyperlink2 = new() { NavigateUri = new("https://rress.tistory.com") };
            hyperlink2.Inlines.Add(new Run() { Text = "개발자 블로그 2 (티스토리: Rolling Ress)" });

            TextBlock tb = new();
            tb.Inlines.Add(new Run()
            {
                Text = @"환영합니다, Rolling Ress의 카루입니다.

GGHS Time Table을 설치해주셔서 감사합니다. 가능하다면 가능한 많은 분들께
이 프로그램을 알려주세요.
기능에 문제가 있거나, 줌 링크가 누락이 된 반 혹은 과목이 있다면
설정 창의 'Send Feedback' 버튼을 통해 제보해주시면 감사하겠습니다.

카루 블로그 링크:
"
            });
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
            };

            var selection = await contentDialog.ShowAsync();
            if (selection is ContentDialogResult.Primary)
                await Windows.System.Launcher.LaunchUriAsync(new("https://blog.naver.com/nsun527"));
            if (selection is ContentDialogResult.Secondary)
                await Windows.System.Launcher.LaunchUriAsync(new("https://rress.tistory.com"));
        }

        private async void Button_Click_2(object sender, RoutedEventArgs e)
        {
            FeedbackDialog feedbackDialog = new();
            await feedbackDialog.ShowAsync();
        }
        /*
* 버튼 하나 더 만들고
* About 크레딧 넣기
*/
    }
}
