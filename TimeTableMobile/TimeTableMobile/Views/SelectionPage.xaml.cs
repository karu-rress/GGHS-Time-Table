using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTableCore;
using TimeTableCore.Grade3.Semester1;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TimeTableMobile.Views;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class SelectionPage : ContentPage
{
    internal static User User { get; set; } = new();
    public SelectionPage()
    {
        InitializeComponent();
        ReadFromSettings();
        SetClassModifier();
    }

    private void ClassAdjustButtonClicked(object sender, EventArgs e)
    {
        if (sender is Button btn)
        {
            if (btn.Text is "<")
                User.Class--;

            else if (btn.Text is ">")
                User.Class++;

            SetClassModifier();
        }
    }

    private void SetClassModifier()
    {
        ClassLabel.Text = User.Class.ToString();
        switch (ClassLabel.Text)
        {
            case "0" or "1":
                MinusClassButton.IsVisible = false;
                break;
            case "8":
                PlusClassButton.IsVisible = false;
                break;
            default:
                MinusClassButton.IsVisible = true;
                PlusClassButton.IsVisible = true;
                break;
        }
    }

    private void RadioButtonChanged(object sender, CheckedChangedEventArgs e)
    {
        if (sender is not RadioButton radio)
            return;

        string selected = radio.ContentAsString();
        switch (radio.GroupName)
        {
            case "Korean":
                Subjects.Korean.SetAs(selected);
                break;

            case "Math":
                Subjects.Math.SetAs(selected);
                break;

            case "Social":
                Subjects.Social.SetAs(selected);
                break;

            case "Language":
                Subjects.Language.SetAs(selected);
                break;

            case "Global1":
                Subjects.Global1.SetAs(selected);
                break;

            case "Global2":
                Subjects.Global2.SetAs(selected);
                break;
        }
    }
    private void ReadFromSettings()
    {
        if (User.Class is 0)
            return;

#pragma warning disable CS8509
        (Subjects.Korean.FullName switch
        {
            "언어와 매체" => RadioKorLang,
            "화법과 작문" => RadioKorSpeech,
        }).IsChecked = true;
        (Subjects.Math.FullName switch
        { 
            "확률과 통계" => RadioMathProbaility,
            "미적분" => RadioMathDaic,
        }).IsChecked = true;
        (Subjects.Social.FullName switch
        {
            "동아시아사" => RadioSocialEastern,
            "한국지리" => RadioSocialGeo,
            "사회·문화" => RadioSocialCulture,
        }).IsChecked = true;
        (Subjects.Language.FullName switch
        {
            "스페인어권 문화" => RadioLangSpanish,
            "일본문화" => RadioLangJapanese,
            "중국문화" => RadioLangChinese,
        }).IsChecked= true;
        (Subjects.Global1.FullName switch
        {
            "사회 탐구 방법" => RadioGlobalSocial,
            "한국 사회의 이해" => RadioGlobalKorean,
        }).IsChecked = true;
        (Subjects.Global2.FullName switch
        {
            "세계 문제와 미래 사회" => RadioGlobalFuture,
            "윤리학 연습" => RadioGlobalEthics,
        }).IsChecked = true;
#pragma warning restore
    }
}
