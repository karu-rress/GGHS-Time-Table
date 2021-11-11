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
            SetClassModifier();
        }

        private void ClassAdjustButtonClicked(object sender, EventArgs e)
        {
            if (sender is Button btn)
            {
                if (btn.Text is "<")
                {
                    UserData.Class--;
                }
                else if (btn.Text is ">")
                {
                    UserData.Class++;
                }

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
                    Subjects.Languages.Selected = subject.RawNameToCellName();
                    break;
                case "SocialA":
                    Subjects.Specials1.Selected = subject.RawNameToCellName();
                    break;
                case "SocialB":
                    Subjects.Specials2.Selected = subject.RawNameToCellName();
                    break;
                case "Science":
                    Subjects.Sciences.Selected = subject.RawNameToCellName();
                    break;
                default:
                    break;
            }

        }
    }
}