using System;
using System.Diagnostics;
using System.IO;
using System.Xml;
using TimeTableMobile.Views;
using Xamarin.Forms;
using TimeTableCore;

namespace TimeTableMobile
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {

        }

        protected override void OnSleep()
        {
            if (SelectionPage.User.IsAllFilled)
            {
                File.WriteAllText(User.FileName, 
                    $"{SelectionPage.User.Class}," +
                    $"{Korean.Selected}," +
                    $"{TimeTableCore.Math.Selected}," +
                    $"{Social.Selected}," +
                    $"{Language.Selected},"+
                    $"{Global1.Selected},"+
                    $"{Global2.Selected}");
            }
            
        }

        protected override void OnResume()
        {
            TimeTablePage.LoadSettings();            
        }
    }
}