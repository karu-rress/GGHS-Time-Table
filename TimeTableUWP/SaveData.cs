#nullable enable
#define __TESTING__

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

namespace TimeTableUWP
{
    [GTT5]
    public class DataSaver : BaseSaver
    {
        // 이거 Info.Settings로 대체 가능하지 않음?
        // public Settings? Settings { get; set; }

        private string DataFile => SaveFiles.DataFile;
        private string KeyFile => SaveFiles.KeyFile;
        private string SettingsFile => SaveFiles.SettingsFile;
        private string VersionFile => SaveFiles.VersionFile;
        private string ClassFile => SaveFiles.ClassFile;

        [XmlType("SubjectList")]
        [XmlInclude(typeof(Subject))]
        public class SubjectList
        {
            public SubjectList() { }
            public SubjectList(Subject kor, Subject math, Subject soc, Subject lang, Subject glo1, Subject glo2)
            {
                Korean = kor;
                Math = math;
                Social = soc;
                Language = lang;
                Global1 = glo1; 
                Global2 = glo2;
            }
            public Subject Korean { get; set; }
            public Subject Math { get; set; }
            public Subject Social { get; set; }
            public Subject Language { get; set; }
            public Subject Global1 { get; set; }
            public Subject Global2 { get; set; }
            public (Subject kor, Subject math, Subject soc, Subject lang, Subject glo1, Subject glo2) Parse()
            {
                return (Korean, Math, Social, Language, Global1, Global2);
            }
        }

        public async Task SaveAsync()
        {
#if __TESTING__
            if (Info.User is null)
                throw new NullReferenceException("DataSaver: BaseSaver.UserData is null");

            //if (Settings is null)
                //throw new NullReferenceException("DataSaver: Settings is null.");
#endif
            Task writeKey = Task.CompletedTask;
            if (Info.User.IsActivated)
            {
                DataWriter<ActivationLevel> writer = new(KeyFile, Info.User.ActivationLevel);
                writeKey = writer.WriteAsync();
            }

            // SubjectTuple tuple = (Korean, Math, Social, Language, Global1, Global2);
            SubjectList list = new(ttc.Korean.Selected, ttc.Math.Selected, ttc.Social.Selected, ttc.Language.Selected, ttc.Global1.Selected, ttc.Global2.Selected);
            DataWriter<SubjectList> writeSubject = new(DataFile, list);
            DataWriter<Settings> writeSettings = new(SettingsFile, Info.Settings);
            DataWriter<Version> writeVersion = new(VersionFile, Info.Version);
            DataWriter<int> writeClass = new(ClassFile, Info.User.Class);
            await Task.WhenAll(writeKey, writeSubject.WriteAsync(), writeSettings.WriteAsync(), writeVersion.WriteAsync(), writeClass.WriteAsync());
        }

        public async Task LoadAsync()
        {
            var storageFolder = ApplicationData.Current.LocalFolder;

            // Check the Version File exists. If not, the user is using version 3 or newly installed.
            if (await storageFolder.TryGetItemAsync(VersionFile) is not StorageFile)
            {
                Info.User.Status = LoadStatus.NewlyInstalled;
                return;
            }

            DataReader<Settings> readSettings = new(SettingsFile);
            var settings = readSettings.ReadAsync();

            DataReader<SubjectList> readTuple = new(DataFile);
            var subject = readTuple.ReadAsync();

            DataReader<Version> readVersion = new(VersionFile);
            var version = readVersion.ReadAsync();

            DataReader<int> readClass = new(ClassFile);
            var cls = readClass.ReadAsync();

            // Wait all until all files are read
            await Task.WhenAll(settings, subject, version, cls);

            Info.Settings = await settings;
            SubjectList subjects = await subject;
            Info.User.Class = await cls;

            (ttc.Korean.Selected, ttc.Math.Selected, ttc.Social.Selected, ttc.Language.Selected, ttc.Global1.Selected, ttc.Global2.Selected)
                = subjects.Parse();

            // TODO:
            // 이거 이런 식으로 여기서 로드해버릴 수 있음.
            // 세이브도 바로 되면 그냥 BaseData 자체를 삭제해도 될 수도.
            // TimeTableCore.Grade3.Semester1.Subjects.Korean.SetAs(Korean);


            // If activated
            if (await storageFolder.TryGetItemAsync(KeyFile) is not null)
            {
                DataReader<ActivationLevel> reader = new(KeyFile);
                Info.User.ActivationLevel = await reader.ReadAsync();
            }

            // If updated
            if (await version != Info.Version)
            {
                Info.User.Status = LoadStatus.Updated;
                return;
            }
            Info.User.Status = LoadStatus.Normal;
        }
    }
}