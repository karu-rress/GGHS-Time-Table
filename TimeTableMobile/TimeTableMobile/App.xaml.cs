using System;
using System.Diagnostics;
using System.IO;
using System.Xml;
using TimeTableMobile.GGHS.Grade2.Semester2;
using TimeTableMobile.Views;
using Xamarin.Forms;

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
            if (UserData.IsAllFilled)
            {
                File.WriteAllText(UserData.FileName, $"{UserData.Class},{UserData.Language},{UserData.Special1},{UserData.Special2},{UserData.Science}");
            }
        }

        protected override void OnResume()
        {
            if (File.Exists(UserData.FileName))
            {
                string read = File.ReadAllText(UserData.FileName);
                string[] array = read.Split(',');

                UserData.Class = Convert.ToInt32(array[0]);
                UserData.Language = array[1];
                UserData.Special1 = array[2];
                UserData.Special2 = array[3];
                UserData.Science = array[4];
            }
        }
    }
}