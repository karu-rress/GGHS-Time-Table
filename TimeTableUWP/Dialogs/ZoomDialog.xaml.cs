#nullable enable

using RollingRess;
using System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Documents;
using GGHS;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace TimeTableUWP
{

    public sealed partial class ZoomDialog : ContentDialog
    {
        private const string None = "(None)";
        private readonly ZoomInfo zoomInfo;

        public ZoomDialog(int @class, string subject, ZoomInfo zoomInfo)
        {
            InitializeComponent();
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
                hyperlink.Inlines.Add(new Run() { Text = zoomInfo.Link });
                tb.Inlines.Add(hyperlink);
                tb.AddText("\n");
                tb.AddTextLine($"ID: {zoomInfo.Id ?? None}");
                tb.AddTextLine($"PW: {zoomInfo.Pw ?? None}");
            }
            else
            {
                tb.AddTextLine("Not available");
                tb.AddTextLine("개발자에게 줌 링크 추가를 요청해보세요.");
                IsPrimaryButtonEnabled = false;
                DefaultButton = ContentDialogButton.Secondary;
            }

            tb.AddText("\nClassroom: ");
            if (zoomInfo.ClassRoom is not null)
            {
                Hyperlink classroom = new();
                // TODO: Remove the ?? operator after the classrooms are all filled.
                classroom.NavigateUri = new(zoomInfo.ClassRoom ?? "https://classroom.google.com/");
                classroom.Inlines.Add(new Run() { Text = "Click here to open classroom" });
                tb.Inlines.Add(classroom);
            }
            else
            {
                tb.AddTextLine("Not available");
                tb.AddTextLine("개발자에게 클래스룸 링크 추가를 요청해보세요.");
                IsSecondaryButtonEnabled = false;
            }
            tb.AddTextLine($"\nTeacher: {zoomInfo.Teacher} 선생님\n");
            tb.AddText(@"Click the buttons below or the links above to navigate.");

            Content = tb;
        }

        private async void ContentDialog_PrimaryButtonClick(ContentDialog _, ContentDialogButtonClickEventArgs args) 
            => await Windows.System.Launcher.LaunchUriAsync(new(zoomInfo.Link));

        private async void ContentDialog_SecondaryButtonClick(ContentDialog _, ContentDialogButtonClickEventArgs args) 
            => await Windows.System.Launcher.LaunchUriAsync(new(zoomInfo.ClassRoom));
    }
}
