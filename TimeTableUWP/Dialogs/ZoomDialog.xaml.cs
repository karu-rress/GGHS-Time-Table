#nullable enable

using System;
using Windows.System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Documents;

using GGHS;
using RollingRess;
using GGHS.Grade2.Semester2;

namespace TimeTableUWP
{
    public partial class ZoomDialog : ContentDialog
    {
        private const string None = "(None)";
        protected readonly ZoomInfo zoomInfo;
        protected TextBlock TextBlock;
        public ZoomDialog(int @class, string subject, ZoomInfo zoomInfo)
        {
            InitializeComponent();
            RequestedTheme = MainPage.Theme;

            Title = $"Class {@class} {subject} Links";
            this.zoomInfo = zoomInfo;

            TextBlock = new();
            SetContent();
        }

        protected virtual void SetContent()
        {
            TextBlock.AddText("Zoom: ");
            if (zoomInfo.Link is not null)
            {
                Hyperlink hyperlink = new();
                hyperlink.NavigateUri = new(zoomInfo.Link);
                hyperlink.Inlines.Add(new Run() { Text = "Click here to open ZOOM meetings" });
                TextBlock.Inlines.Add(hyperlink);
                TextBlock.AddText("\n");
                TextBlock.AddTextLine($"ID: {zoomInfo.Id ?? None}");
                TextBlock.AddTextLine($"PW: {zoomInfo.Pw ?? None}");
            }
            else
            {
                TextBlock.AddTextLine("Not available");
                TextBlock.AddTextLine("카루에게 줌 링크 추가를 요청해보세요.");
                IsPrimaryButtonEnabled = false;
                DefaultButton = ContentDialogButton.Secondary;
            }

            TextBlock.AddText("\nClassroom: ");
            if (zoomInfo.ClassRoom is not null)
            {
                Hyperlink classroom = new();
                classroom.NavigateUri = new(zoomInfo.ClassRoom);
                classroom.Inlines.Add(new Run() { Text = "Click here to open classroom" });
                TextBlock.Inlines.Add(classroom);
            }
            else
            {
                TextBlock.AddTextLine("Not available");
                TextBlock.AddTextLine("카루에게 클래스룸 링크 추가를 요청해보세요.");
                IsSecondaryButtonEnabled = false;
            }
            TextBlock.AddTextLine($"\nTeacher: {zoomInfo.Teacher} 선생님\n");
            TextBlock.AddText(@"Click the buttons below or the links above to navigate.");

            Content = TextBlock;
        }

        private async void ContentDialog_PrimaryButtonClick(ContentDialog _, ContentDialogButtonClickEventArgs args) 
            => await Launcher.LaunchUriAsync(new(zoomInfo.Link));

        private async void ContentDialog_SecondaryButtonClick(ContentDialog _, ContentDialogButtonClickEventArgs args) 
            => await Launcher.LaunchUriAsync(new(zoomInfo.ClassRoom));
    }

    public sealed class CompareCultureDialog : ZoomDialog
    {
        public CompareCultureDialog(int @class) : base(@class, "비교문화", new(null, null, null, "https://classroom.google.com/u/0/c/Mzc4NzczNjQ1MDU0", ""))
        {
            IsPrimaryButtonEnabled = false;
            DefaultButton = ContentDialogButton.Secondary;
        }

        protected override void SetContent()
        {
            TextBlock.AddText("홍정민 T: ");
            Hyperlink hyperlink1 = new();
            hyperlink1.NavigateUri = new(ZoomLinks.CompareCultures.Hong.Zoom);
            hyperlink1.Inlines.Add(new Run() { Text = "Click here to open ZOOM meetings (홍정민 T)" });
            TextBlock.Inlines.Add(hyperlink1);
            TextBlock.AddText("\n");
            TextBlock.AddTextLine($"ID: {ZoomLinks.CompareCultures.Hong.Id}");
            TextBlock.AddTextLine($"PW: {ZoomLinks.CompareCultures.Hong.Password}");

            TextBlock.AddText("\n정혜영 T: ");
            Hyperlink hyperlink2 = new();
            hyperlink2.NavigateUri = new(ZoomLinks.CompareCultures.Jung.Zoom);
            hyperlink2.Inlines.Add(new Run() { Text = "Click here to open ZOOM meetings (정혜영 T)" });
            TextBlock.Inlines.Add(hyperlink2);
            TextBlock.AddText("\n");
            TextBlock.AddTextLine($"ID: {ZoomLinks.CompareCultures.Jung.Id}");
            TextBlock.AddTextLine($"PW: {ZoomLinks.CompareCultures.Jung.Password}");

            TextBlock.AddText("\nClassroom: ");

            Hyperlink classroom = new();
            classroom.NavigateUri = new("https://classroom.google.com/u/0/c/Mzc4NzczNjQ1MDU0");
            classroom.Inlines.Add(new Run() { Text = "Click here to open classroom" });
            TextBlock.Inlines.Add(classroom);
            
            TextBlock.AddTextLine($"\nTeacher: 홍정민 / 정혜영 선생님\n");
            TextBlock.AddText(@"Click the buttons below or the links above to navigate.");

            Content = TextBlock;
        }
    }

}
