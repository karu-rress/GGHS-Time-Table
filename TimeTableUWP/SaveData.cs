#nullable enable

using RollingRess.UWP.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeTableUWP.Pages;
using Windows.Storage;
using Windows.UI;
using Windows.UI.Xaml.Controls;

namespace TimeTableUWP
{
    public static class SaveData
    {
        public class ComboBoxSave
        {
            public string? Class { get; set; }
            public string? Lang { get; set; }
            public string? Special1 { get; set; }
            public string? Special2 { get; set; }
            public string? Science { get; set; }

            public (string? @class, string? lang, string? special1, string? special2, string? science) Parse()
            => (Class, Lang, Special1, Special2, Science);
        }

        private const string dataFileName = SaveFiles.DataFile;
        private const string keyFileName = SaveFiles.KeyFile;
        private const string settingsFileName = SaveFiles.SettingsFile;
        private const string versionFileName = SaveFiles.VersionFile;

        public static bool IsActivated { get; set; } = false;
        public static ActivateLevel ActivateStatus { get; set; } = ActivateLevel.None;

        public static Color ColorType = Color.FromArgb(0xEE, 0xE3, 0xCA, 0xEB); // pink
        public static string? ClassComboBoxText { get; set; }
        public static string? Special1ComboBoxText { get; set; }
        public static string? Special2ComboBoxText { get; set; }
        public static string? LangComboBoxText { get; set; }
        public static string? ScienceComboBoxText { get; set; }
        public static bool IsDeveloperOrInsider => ActivateStatus is ActivateLevel.Developer or ActivateLevel.Insider or ActivateLevel.ShareTech;
        public static bool IsNotDeveloperOrInsider => !IsDeveloperOrInsider;

        private static IEnumerable<string?> ComboBoxTexts
        {
            get
            {
                yield return ClassComboBoxText;
                yield return LangComboBoxText;
                yield return Special1ComboBoxText;
                yield return Special2ComboBoxText;
                yield return ScienceComboBoxText;
            }
        }

        public static async Task SaveDataAsync()
        {
            // Save activation status only if the user is activated.
            if (IsActivated is true)
            {
                DataWriter<ActivateLevel> writer = new(keyFileName, ActivateStatus);
                await writer.WriteAsync();
            }

            DataWriter<ComboBoxSave> writeComboBox = new(dataFileName, new()
            {
                Class = ClassComboBoxText,
                Lang = LangComboBoxText,
                Special1 = Special1ComboBoxText,
                Special2 = Special2ComboBoxText,
                Science = ScienceComboBoxText
            });
            DataWriter<Color> writeSettings = new(settingsFileName, ColorType);
            DataWriter<string> writeVersion = new(versionFileName, MainPage.Version);
            await Task.WhenAll(writeSettings.WriteAsync(), writeVersion.WriteAsync(), writeComboBox.WriteAsync());
        }

        public static async Task<TimeTablePage.LoadStatus> LoadDataAsync()
        {
            var storageFolder = ApplicationData.Current.LocalFolder;

            // Check the Version File exists. If not, the user is using version 3 or newly installed.
            if (await storageFolder.TryGetItemAsync(versionFileName) is not StorageFile)
            {

                return TimeTablePage.LoadStatus.NewUser;
            }

            DataReader<Color> readSettings = new(settingsFileName);
            var settings = readSettings.ReadAsync();

            DataReader<ComboBoxSave> readCombo = new(dataFileName);
            var combo = readCombo.ReadAsync();

            DataReader<string> readVersion = new(versionFileName);
            var version = readVersion.ReadAsync();

            // Wait all until all files are read
            await Task.WhenAll(settings, combo, version);

            ColorType = await settings;
            ComboBoxSave? Combo = await combo;

            (ClassComboBoxText, LangComboBoxText, Special1ComboBoxText, Special2ComboBoxText, ScienceComboBoxText) = Combo.Parse();

            // If activated
            if (await storageFolder.TryGetItemAsync(keyFileName) is not null)
            {
                DataReader<ActivateLevel> reader = new(keyFileName);
                ActivateStatus = await reader.ReadAsync();
                IsActivated = true;
            }

            // If updated
            if (await version != MainPage.Version)
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
                comboBox.SelectedItem = string.IsNullOrEmpty(text) ? null : text;
            }
        }

        public static void SetClass(ref int @class)
        {
            if (!string.IsNullOrEmpty(ClassComboBoxText))
                @class = ClassComboBoxText![6] - '0';
        }
    }
}