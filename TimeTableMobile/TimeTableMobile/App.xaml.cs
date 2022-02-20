using System;
using System.Diagnostics;
using System.IO;
using System.Xml;
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
            if (TimeTablePage.SaveData.IsAllFilled)
            {
                File.WriteAllText(TimeTablePage.SaveData.FileName, $"{TimeTablePage.SaveData.Class},{TimeTablePage.SaveData.Language},{TimeTablePage.SaveData.Special1},{TimeTablePage.SaveData.Special2},{TimeTablePage.SaveData.Science}");
            }
        }

        protected override void OnResume()
        {
            if (File.Exists(TimeTablePage.SaveData.FileName))
            {
                string read = File.ReadAllText(TimeTablePage.SaveData.FileName);
                string[] array = read.Split(',');

                TimeTablePage.SaveData.Class = Convert.ToInt32(array[0]);
                TimeTablePage.SaveData.Language = array[1];
                TimeTablePage.SaveData.Special1 = array[2];
                TimeTablePage.SaveData.Special2 = array[3];
                TimeTablePage.SaveData.Science = array[4];
            }
        }
    }
}