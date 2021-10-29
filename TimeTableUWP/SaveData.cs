#nullable enable

using RollingRess.UWP.FileIO;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTableUWP.Pages;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;
using static RollingRess.StaticClass;

namespace TimeTableUWP
{
    public static class SaveData
    {
        public class ComboBoxSave
        {
            public ComboBoxSave() : this("", "", "", "", "") { }
            public ComboBoxSave(string @class, string lang, string special1, string special2, string science)
            {
                Class = @class;
                Lang = lang;
                Special1 = special1;
                Special2 = special2;
                Science = science;
            }

        public string Class { get; set; }
        public string Lang { get; set; }
        public string Special1 { get; set; }
        public string Special2 { get; set; }
        public string Science { get; set; }
        }


        private const string dataFileName = "gttdatxml.sav", keyFileName = "gttactxml.key", settingsFileName = "gttsetxml.sav", versionFileName = "gttverxml.sav";
        
        private static string grade, @class, special, social, lang, science;
        public static bool IsActivated { get; set; } = false;
        public static ActivateLevel ActivateStatus { get; set; } = ActivateLevel.None;
        public static Color ColorType  = Colors.LightSteelBlue;
        public static string GradeComboBoxText { get => "Grade 2"; set => grade = value ?? "NULL"; }
        public static string ClassComboBoxText { get => @class; set => @class = value ?? "NULL"; }
        public static string Special1ComboBoxText { get => special; set => special = value ?? "NULL"; }
        public static string Special2ComboBoxText { get => social; set => social = value ?? "NULL"; }
        public static string LangComboBoxText { get => lang; set => lang = value ?? "NULL"; }
        public static string ScienceComboBoxText { get => science; set => science = value ?? "NULL"; }
        public static bool IsDeveloperOrInsider => ActivateStatus is ActivateLevel.Developer or ActivateLevel.Insider;
        public static bool IsNotDeveloperOrInsider => !IsDeveloperOrInsider;

        private static IEnumerable<string> ComboBoxTexts
        {
            get
            {
                yield return GradeComboBoxText;
                yield return ClassComboBoxText;
                yield return LangComboBoxText;
                yield return Special1ComboBoxText;
                yield return Special2ComboBoxText;
                yield return ScienceComboBoxText;
            }
        }

        public static async Task SaveDataAsync()
        {
            if (IsActivated is true)
            {
                DataWriter<ActivateLevel> writer = new(keyFileName, ActivateStatus);
                await writer.WriteAsync();
            }

            DataWriter<ComboBoxSave> writeComboBox = new(dataFileName, new(
                ClassComboBoxText,
                LangComboBoxText,
                Special1ComboBoxText,
                Special2ComboBoxText,
                ScienceComboBoxText
                ));
            DataWriter<Color> writeSettings = new(settingsFileName, ColorType);
            DataWriter<string> writeVersion = new(versionFileName, MainPage.Version);
            await Task.WhenAll(writeSettings.WriteAsync(), writeVersion.WriteAsync(), writeComboBox.WriteAsync());
        }

        public static async Task<TimeTablePage.LoadStatus> LoadDataAsync()
        { 
            var storageFolder = ApplicationData.Current.LocalFolder;
            if (await storageFolder.TryGetItemAsync(versionFileName) is not StorageFile dataFile)
            {
                return TimeTablePage.LoadStatus.NewUser;
            }

            DataReader<Color> readSettings = new(settingsFileName);
            ColorType = await readSettings.ReadAsync();

            DataReader<ComboBoxSave> readCombo = new(dataFileName);
            var Combo = await readCombo.ReadAsync();
            ClassComboBoxText = Combo.Class;
            LangComboBoxText = Combo.Lang;
            Special1ComboBoxText = Combo.Special1;
            Special2ComboBoxText = Combo.Special2;
            ScienceComboBoxText = Combo.Science;

            if (await storageFolder.TryGetItemAsync(keyFileName) is StorageFile keyFile)
            {
                DataReader<ActivateLevel> reader = new(keyFileName);
                ActivateStatus = await reader.ReadAsync();
                IsActivated = true;
            }

            DataReader<string> readVersion = new(versionFileName);
            if (await readVersion.ReadAsync() != MainPage.Version)
            {
                return TimeTablePage.LoadStatus.Updated;
            }
            return TimeTablePage.LoadStatus.Default;
        }

        public static void SetComboBoxes(IEnumerable<ComboBox> comboBoxes)
        {
            var tupleList = comboBoxes.Zip(ComboBoxTexts, (comboBox, text) => (comboBox, text));
            foreach (var (comboBox, text) in tupleList)
            {
                comboBox.SelectedItem = text is "NULL" ? null : text;
            }
        }

        public static void SetGradeAndClass(ref int grade, ref int @class)
        {
            grade = 2;
            @class = ClassComboBoxText[6] - '0';
        }
    }

    // TODO: Use dynamic APIs
    // Check for C# reference in microsoft.
}
