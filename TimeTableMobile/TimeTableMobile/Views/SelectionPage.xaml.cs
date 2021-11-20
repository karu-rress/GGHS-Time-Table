using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTableMobile.GGHS;
using TimeTableMobile.GGHS.Grade2.Semester2;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TimeTableMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SelectionPage : ContentPage
    {
        public SelectionPage()
        {
            InitializeComponent();
            ReadFromSettings();
            SetClassModifier();
        }

        private void ReadFromSettings()
        {
            if (UserData.Class is 0)
                return;

            (UserData.Language switch
            {
                "중어Ⅰ" => RadioLangChinese,
                "스어Ⅰ" => RadioLangSpanish,
                "일어Ⅰ" => RadioLangJapanese,
            }).IsChecked = true;
            (UserData.Special1 switch
            {
                "국경" => RadioSocialAEconomics,
                "국정" => RadioSocialAPolitics,
                "비문" => RadioSocialACulture,
                "세역문" => RadioSocialAHistory,
                "현정철" => RadioSocialAPhilosophy,
                "세지연" => RadioSocialARegion,
            }).IsChecked = true;
            (UserData.Special2 switch
            {
                "국경" => RadioSocialBEconomics,
                "국정" => RadioSocialBPolitics,
                "비문" => RadioSocialBCulture,
                "동근사" => RadioSocialBEastern,
                "세역문" => RadioSocialBHistory,
                "현정철" => RadioSocialBPhilosophy,
                "GIS" => RadioSocialBGIS,
            }).IsChecked = true;
            (UserData.Science switch
            {
                "과학사" => RadioScienceHistory,
                "생과" => RadioScienceLife
            }).IsChecked = true;
        }

        private void ClassAdjustButtonClicked(object sender, EventArgs e)
        {
            if (sender is Button btn)
            {
                if (btn.Text is "<")
                    UserData.Class--;
                
                else if (btn.Text is ">")
                    UserData.Class++;

                SetClassModifier();
            }
        }

        private void SetClassModifier()
        {
            ClassLabel.Text = UserData.Class.ToString();
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

            string subject = radio.ContentAsString();
            switch (radio.GroupName)
            {
                case "Language":
                    Subjects.Languages.Selected = UserData.Language = subject.RawNameToCellName();
                    break;
                case "SocialA":
                    Subjects.Specials1.Selected = UserData.Special1 = subject.RawNameToCellName();
                    break;
                case "SocialB":
                    Subjects.Specials2.Selected = UserData.Special2 = subject.RawNameToCellName();
                    break;
                case "Science":
                    Subjects.Sciences.Selected = UserData.Science = subject.RawNameToCellName();
                    break;
            }
        }
    }
}