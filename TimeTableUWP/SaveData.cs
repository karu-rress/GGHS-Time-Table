﻿#nullable enable

using RollingRess.UWP.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeTableUWP.Pages;
using Windows.Storage;
using Windows.UI;
using Windows.UI.Xaml.Controls;
using TimeTableCore;
using ttc = TimeTableCore;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.IO;
using System.Xml;
using System.Text;
using static RollingNET.Serializer;

namespace TimeTableUWP
{
    public class DataSaver
    {
        private static ApplicationDataContainer localSettings => ApplicationData.Current.LocalSettings;

        internal static class SettingValues
        {
            public const string Version = "Version";
            public const string Class = "Class"; 
            public const string Subjects = "Subjects";
            public const string Level = "ActivationLevel";
            public const string Settings = "Settings";
            public const string Todo = "TodoList";
        }

        public static void SaveAsync()
        {
            localSettings.Values[SettingValues.Version] = Serialize(Info.Version);
            localSettings.Values[SettingValues.Class] = Info.User.Class;
            localSettings.Values[SettingValues.Subjects] = Serialize(new SubjectList(Korean.Selected, ttc.Math.Selected, Social.Selected, Language.Selected, Global1.Selected, Global2.Selected));
            localSettings.Values[SettingValues.Level] = Serialize(Info.User.ActivationLevel);
            localSettings.Values[SettingValues.Settings] = Serialize(Info.Settings);
            localSettings.Values[SettingValues.Todo] = Serialize(TodoListPage.TaskList.List);
        }

        public static void LoadAsync()
        {
            if (Deserialize<Version?>(localSettings.Values[SettingValues.Version]) is Version version)
                Info.User.Status = version != Info.Version ? LoadStatus.Updated : LoadStatus.Normal;
            else
            {
                Info.User.Status = LoadStatus.NewlyInstalled;
                return;
            }

            if (Deserialize<SubjectList>(localSettings.Values[SettingValues.Subjects]) is SubjectList list)
                (Korean.Selected, ttc.Math.Selected, Social.Selected, Language.Selected, Global1.Selected, Global2.Selected) = list.Parse();

            if (Deserialize<Settings>(localSettings.Values[SettingValues.Settings]) is Settings setting)
                Info.Settings = setting;

            if (localSettings.Values[SettingValues.Class] is int cls)
                Info.User.Class = cls;

            if (Deserialize<List<Todo.TodoTask>>(localSettings.Values[SettingValues.Todo]) is List<Todo.TodoTask> tasklist)
                TodoListPage.TaskList.List = tasklist;

            if (Deserialize<ActivationLevel>(localSettings.Values[SettingValues.Level]) is ActivationLevel level)
                Info.User.ActivationLevel = level;
        }

        [DataContract(Name = "Subjects")]
        public class SubjectList
        {
            public SubjectList(Subject kor, Subject math, Subject soc, Subject lang, Subject glo1, Subject glo2)
            {
                Korean = kor;
                Math = math;
                Social = soc;
                Language = lang;
                Global1 = glo1;
                Global2 = glo2;
            }
            [DataMember]
            public Subject Korean { get; set; }
            [DataMember]
            public Subject Math { get; set; }
            [DataMember]
            public Subject Social { get; set; }
            [DataMember]
            public Subject Language { get; set; }
            [DataMember]
            public Subject Global1 { get; set; }
            [DataMember]
            public Subject Global2 { get; set; }
            public (Subject kor, Subject math, Subject soc, Subject lang, Subject glo1, Subject glo2) Parse()
                => (Korean, Math, Social, Language, Global1, Global2);
        }
    }
}