using System;
using System.Collections.Generic;
using System.Linq;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using TimeTableCore;
using System.Collections;
using System.IO;

using TimeTableCore.Grade3.Semester2;

namespace TimeTableMobile.Views;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class TimeTablePage : ContentPage
{
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

    public static void LoadSettings()
    {
        if (File.Exists(User.FileName))
        {
            string read = File.ReadAllText(User.FileName);
            string[] array = read.Split(',');

            SelectionPage.User.Class = Convert.ToInt32(array[0]);

            Subjects.Social.SetAs(array[1]);
            Subjects.Language.SetAs(array[2]);
            Subjects.Global1.SetAs(array[3]);
            Subjects.Global2.SetAs(array[4]);
        }
    }


    private void TableButtons_Click(object sender, EventArgs e)
    {

    }

    private async void SpecialButtons_Click(object sender, EventArgs e)
    {
        if (sender == fri5Button || sender == fri6Button || sender == mon6Button || sender == mon7Button)
            await DisplayAlert("창의적 체험활동", "인문학 울림 또는 창의진로프로젝트 시간입니다.", "OK");

        if (sender == fri7Button)
            await DisplayAlert("Homecoming", "즐거운 홈커밍 데이 :)", "OK");
    }

    private void DrawTimeTable()
    {
        ErrorLabel.IsVisible = false;
        if (SelectionPage.User is null)
            throw new Exception();

        try
        {
            if (SelectionPage.User.Class is 0)
            {
                ErrorLabel.IsVisible=true;
                return;
            }

            MainLabel.Text = $"Class {SelectionPage.User.Class} Timetable";


            TimeTable.ResetClass(SelectionPage.User.Class);
            AssignButtonsByTable(TimeTable.Table!);

            void AssignButtonsByTable(TimeTable subjectTable)
            {
                var subjects = subjectTable.Data.Cast<Subject>();
                var lists = Buttons.Zip(subjects, (Button btn, Subject subject) => (btn, subject));
                foreach (var (btn, subject) in lists)
                    btn.Text = subject.Name switch
                    {
                        "한국지리" => "한지",
                        "사회·문화" => "사문",
                        "일본문화" or "중국문화" => $"{subject.Name[0]}{subject.Name[2]}",
                        var x when x.Contains("Ⅱ") => $"{x[..2]}{x[^1]}",
                        _ => subject.Name,
                    };
            }
        }
        catch
        {
            ErrorLabel.IsVisible = true;
        }
    }


    /*
    public static DataSaver SaveData { get; set; } = new();





    // private ZoomLinks ZoomLink => new();
    /*private async void TableButtons_Click(object sender, EventArgs e)
    {
        if (SaveData.UserData.Class is 0)
        {
            ErrorLabel.IsVisible = true;
            return;
        }
        Dictionary<string, ZoomInfo?> GetClassZoomLink() => SaveData.UserData.Class switch
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
            await DisplayActionSheet($"Class {SaveData.UserData.Class} {subject} Links", "Cancel", null);
            return;
        }
        else 
        {
            action = await DisplayActionSheet($"Class {SaveData.UserData.Class} {subject} Links", "Cancel", null, buttons.ToArray());
        }

        switch (action)
        {
            case "Show ZOOM info":
                await DisplayAlert($"Class {SaveData.UserData.Class} {subject} ZOOM info", $"ID: {zoomInfo.Id}\n" + $"PW: {zoomInfo.Pw}", "OK");
                break;
            case "Open ZOOM meetings":
            case "Open Google classroom":
            default:
                break;
        }
    }*


    private void RefreshButton_Click(object sender, EventArgs e)
    {
        ErrorLabel.IsVisible = false;
        DrawTimeTable();
    }

    private void TableButtons_Click(object sender, EventArgs e)
    {

    }
    */
}
