using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using TimeTableCore;
using System.Collections;
using System.IO;

namespace TimeTableMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TimeTablePage : ContentPage
    {
        private string[,]? SubjectTable { get; set; } // string[5, 7]
        private TimeTables TimeTable => new();
        public static bool HasReadFile { get; set; }

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

        public TimeTablePage()
        {
            InitializeComponent();
            Appearing += (s, e) => DrawTimeTable();

            LoadSettings();
        }

        private void LoadSettings()
        {
            if (File.Exists(UserData.FileName))
            {
                string read = File.ReadAllText(UserData.FileName);
                string[] array = read.Split(',');

                UserData.Class = Convert.ToInt32(array[0]);
                Subjects.Languages.Selected = UserData.Language = array[1];
                Subjects.Specials1.Selected = UserData.Special1 = array[2];
                Subjects.Specials2.Selected = UserData.Special2 = array[3];
                Subjects.Sciences.Selected = UserData.Science = array[4];
            }
            
        }

        private void DrawTimeTable()
        {
            ErrorLabel.IsVisible = false;
            try
            {
                if (UserData.Class is 0)
                throw new Exception();

                MainLabel.Text = $"Class {UserData.Class} Timetable";

                string[,] SetArrayByClass()
                {
                    var ret = UserData.Class switch
                    {
                        1 => TimeTable.Class1.Clone() as string[,],
                        2 => TimeTable.Class2.Clone() as string[,],
                        3 => TimeTable.Class3.Clone() as string[,],
                        4 => TimeTable.Class4.Clone() as string[,],
                        5 => TimeTable.Class5.Clone() as string[,],
                        6 => TimeTable.Class6.Clone() as string[,],
                        7 => TimeTable.Class7.Clone() as string[,],
                        8 => TimeTable.Class8.Clone() as string[,],
                        _ => throw new Exception($"SetArrayByClass(): @class: 1~8 expected, but given {@class}.")
                    };
                    if (ret is null)
                        throw new NullReferenceException($"SetArrayByClass(): Class{@class}.Clone() got null.");

                    return ret;
                }

                void AssignButtonsByTable(string[,] subjectTable)
                {
                    IEnumerable<string>? subjects = ((IEnumerable)subjectTable).Cast<string>();
                    var lists = Buttons.Zip(subjects, (Button btn, string subject) => (btn, subject));
                    foreach (var (btn, subject) in lists)
                        btn.Text = subject;
                }

                SubjectTable = SetArrayByClass();
                AssignButtonsByTable(SubjectTable);

                mon6Button.Text = mon7Button.Text = fri5Button.Text = fri6Button.Text =
                SubjectTable[0, 5] = SubjectTable[0, 6] = SubjectTable[4, 4] = SubjectTable[4, 5] = Subjects.CellName.Others;
                fri7Button.Text = SubjectTable[4, 6] = Subjects.CellName.HomeComing;
            }
            catch
            {
                ErrorLabel.IsVisible = true;
            }
        }

        private ZoomLinks ZoomLink => new();
        private async void TableButtons_Click(object sender, EventArgs e)
        {
            if (UserData.Class is 0)
            {
                ErrorLabel.IsVisible = true;
                return;
            }
            Dictionary<string, ZoomInfo?> GetClassZoomLink() => UserData.Class switch
            {
                1 => ZoomLink.Class1,
                2 => ZoomLink.Class2,
                3 => ZoomLink.Class3,
                4 => ZoomLink.Class4,
                5 => ZoomLink.Class5,
                6 => ZoomLink.Class6,
                7 => ZoomLink.Class7,
                8 => ZoomLink.Class8,
                _ => throw new Exception(
                    $"GetClassZoomLink(): Class out of range: 1 to 8 expected, but given {@class}")
            };

            string subject = (sender as Button)!.Text;
            
            if (subject is "비문")
            {
                await ShowCompareCulturePopup();
                return;
            }

            if (GetClassZoomLink().TryGetValue(subject, out var zoomInfo) is false || (zoomInfo is null))
            {
                await DisplayAlert("No Data for Zoom Link", $"Zoom Link for {subject} is currently not available.\n"
                    + "카루에게 줌 링크 추가를 요청해보세요.", "OK");
                return;
            }

            List<string> buttons = new();
            if (zoomInfo.Link is not null)
            {
                if (!string.IsNullOrEmpty(zoomInfo.Pw))
                    buttons.Add("Show ZOOM info");
                buttons.Add("Open ZOOM meetings");
            }
            if (zoomInfo.ClassRoom is not null)
                buttons.Add("Open Google classroom");

            string action;
            if (buttons.Count is 0)
            {
                await DisplayActionSheet($"Class {UserData.Class} {subject} Links", "Cancel", null);
                return;
            }
            else 
            {
                action = await DisplayActionSheet($"Class {UserData.Class} {subject} Links", "Cancel", null, buttons.ToArray());
            }

            switch (action)
            {
                case "Show ZOOM info":
                    await DisplayAlert($"Class {UserData.Class} {subject} ZOOM info", $"ID: {zoomInfo.Id}\n" + $"PW: {zoomInfo.Pw}", "OK");
                    break;
                case "Open ZOOM meetings":
                case "Open Google classroom":
                default:
                    break;
            }
        }

        private async Task ShowCompareCulturePopup()
        {
            string action = await DisplayActionSheet($"Class {UserData.Class} 비교문화 Links", "Cancel", null, 
                "Show ZOOM info", "Open ZOOM (홍정민 T)", "Open ZOOM (정혜영 T)", "Open Google classroom");

            switch (action)
            {
                case "Show ZOOM info":
                    await DisplayAlert($"Class {UserData.Class} 비교문화 ZOOM info", 
                        "홍정민 T\n" +
                        $"ID: {ZoomLinks.CompareCultures.Hong.Id}\n" +
                        $"PW: {ZoomLinks.CompareCultures.Hong.Password}\n\n" +
                        "정혜영 T\n" +
                        $"ID: {ZoomLinks.CompareCultures.Jung.Id}\n" +
                        $"PW: {ZoomLinks.CompareCultures.Jung.Password}", "OK");
                    break;
                case "Open ZOOM (홍정민 T)":
                case "Open ZOOM (정혜영 T)":
                case "Open Google classroom":
                    break;
            }
        }

        private async void SpecialButtons_Click(object sender, EventArgs e)
        {
            if (sender == fri5Button || sender == fri6Button || sender == mon6Button || sender == mon7Button)
                await DisplayAlert("창의적 체험활동", "인문학 울림 또는 창의진로프로젝트 시간입니다.", "OK");

            if (sender == fri7Button)
                await DisplayAlert("Homecoming", "즐거운 홈커밍 데이 :)", "OK");
        }

        private void RefreshButton_Click(object sender, EventArgs e)
        {
            ErrorLabel.IsVisible = false;
            DrawTimeTable();
        }
    }
}