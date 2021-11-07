#nullable enable

using System;
using Windows.System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Documents;

using GGHS;
using RollingRess;

namespace TimeTableUWP
{

    public sealed partial class ZoomDialog : ContentDialog
    {
        private const string None = "(None)";
        private readonly ZoomInfo zoomInfo;

        public ZoomDialog(int @class, string subject, ZoomInfo zoomInfo)
        {
            InitializeComponent();
            RequestedTheme = MainPage.Theme;

            Title = $"Class {@class} {subject} Links";
            this.zoomInfo = zoomInfo;
            
            SetContent();
        }

        private void SetContent()
        {
            TextBlock tb = new();
            tb.AddText("Zoom: ");
            if (zoomInfo.Link is not null)
            {
                Hyperlink hyperlink = new();
                hyperlink.NavigateUri = new(zoomInfo.Link);
                hyperlink.Inlines.Add(new Run() { Text = "Click here to open ZOOM meetings" });
                tb.Inlines.Add(hyperlink);
                tb.AddText("\n");
                tb.AddTextLine($"ID: {zoomInfo.Id ?? None}");
                tb.AddTextLine($"PW: {zoomInfo.Pw ?? None}");
            }
            else
            {
                tb.AddTextLine("Not available");
                tb.AddTextLine("카루에게 줌 링크 추가를 요청해보세요.");
                IsPrimaryButtonEnabled = false;
                DefaultButton = ContentDialogButton.Secondary;
            }

            tb.AddText("\nClassroom: ");
            if (zoomInfo.ClassRoom is not null)
            {
                Hyperlink classroom = new();
                classroom.NavigateUri = new(zoomInfo.ClassRoom);
                classroom.Inlines.Add(new Run() { Text = "Click here to open classroom" });
                tb.Inlines.Add(classroom);
            }
            else
            {
                tb.AddTextLine("Not available");
                tb.AddTextLine("카루에게 클래스룸 링크 추가를 요청해보세요.");
                IsSecondaryButtonEnabled = false;
            }
            tb.AddTextLine($"\nTeacher: {zoomInfo.Teacher} 선생님\n");
            tb.AddText(@"Click the buttons below or the links above to navigate.");

            Content = tb;
        }

        private async void ContentDialog_PrimaryButtonClick(ContentDialog _, ContentDialogButtonClickEventArgs args) 
            => await Launcher.LaunchUriAsync(new(zoomInfo.Link));

        private async void ContentDialog_SecondaryButtonClick(ContentDialog _, ContentDialogButtonClickEventArgs args) 
            => await Launcher.LaunchUriAsync(new(zoomInfo.ClassRoom));
    }
}
