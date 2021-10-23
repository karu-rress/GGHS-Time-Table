#nullable disable

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        private const string dataFileName = "gttdat.sav", keyFileName = "gttact.key", settingsFileName = "gttsets.sav";
        
        private static string grade, @class, special, social, lang, science;
        public static bool IsActivated { get; set; } = false;
        public static ActivateLevel ActivateStatus { get; set; } = ActivateLevel.None;
        public static Color ColorType  = Colors.LightSteelBlue;
        public static string GradeComboBoxText { get => grade; set => grade = value ?? "NULL"; }
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
            StorageFolder storageFolder = ApplicationData.Current.LocalFolder;

            if (GradeComboBoxText is not "NULL")
            {
                var dataFile = await storageFolder.CreateFileAsync(dataFileName, CreationCollisionOption.ReplaceExisting);
                using var stream = await dataFile.OpenAsync(FileAccessMode.ReadWrite);
                using var outputStream = stream.GetOutputStreamAt(0);
                using var dataWriter = new DataWriter(outputStream);
                dataWriter.WriteString(
                    $"{MainPage.Version}\n" + // Prevent conflict with older version's files
                    GradeComboBoxText + "\n" +
                    ClassComboBoxText + "\n" +
                    LangComboBoxText + "\n" +
                    Special1ComboBoxText + "\n" +
                    Special2ComboBoxText + "\n" +
                    ScienceComboBoxText
                    );
                await dataWriter.StoreAsync();
                await outputStream.FlushAsync();
            }

            if (IsActivated is true)
            {
                var keyFile = await storageFolder.CreateFileAsync(keyFileName, CreationCollisionOption.ReplaceExisting);
                using var stream = await keyFile.OpenAsync(FileAccessMode.ReadWrite);
                using var outputStream = stream.GetOutputStreamAt(0);
                using var keyWriter = new DataWriter(outputStream);
                keyWriter.WriteString(ActivateStatus.MakeString());
                await keyWriter.StoreAsync();
                await outputStream.FlushAsync();
            }

            {
                var settingsFile = await storageFolder.CreateFileAsync(settingsFileName, CreationCollisionOption.ReplaceExisting);
                using var stream = await settingsFile.OpenAsync(FileAccessMode.ReadWrite);
                using var outputStream = stream.GetOutputStreamAt(0);
                using var settingsWriter = new DataWriter(outputStream);
                string hexValue = $"#{ColorType.A:X}{ColorType.R:X}{ColorType.G:X}{ColorType.B:X}";
                settingsWriter.WriteString(hexValue);
                await settingsWriter.StoreAsync();
                await outputStream.FlushAsync();
            }
        }

        public static async Task<bool> LoadDataAsync()
        { 
            var storageFolder = ApplicationData.Current.LocalFolder;
            if (await storageFolder.TryGetItemAsync(dataFileName) is not StorageFile dataFile)
            {
                await ShowMessageAsync(@"환영합니다, Rolling Ress의 카루입니다.
GGHS Time Table을 설치해주셔서 감사합니다. 

자신의 선택과목을 선택하고, 시간표를 누르면 해당 시간의 줌 링크와 클래스룸 링크가 띄워집니다.

수시로 최신 버전이 업데이트되니 꼭 주기적으로 업데이트를 해주세요. 다양한 기능이 추가될 예정입니다.", "GGHS Time Table 3", MainPage.Theme);
                return false;
            }

            using (var stream = await dataFile.OpenAsync(FileAccessMode.Read))
            {
                var size = stream.Size;
                using var inputStream = stream.GetInputStreamAt(0);
                using var dataReader = new DataReader(inputStream);
                uint numBytesLoaded = await dataReader.LoadAsync((uint)size);
                string text = dataReader.ReadString(numBytesLoaded);
                string[] lines = text.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

                // 구버전인 경우
                if (lines[0][0] is not '3')
                {
                    var saveFile = await storageFolder.TryGetItemAsync(dataFileName);
                    if (saveFile is not null)
                        await saveFile.DeleteAsync();

                    saveFile = await storageFolder.TryGetItemAsync(keyFileName);
                    if (saveFile is not null)
                        await saveFile.DeleteAsync();

                    saveFile = await storageFolder.TryGetItemAsync(settingsFileName);
                    if (saveFile is not null)
                        await saveFile.DeleteAsync();

                    await ShowMessageAsync(@"환영합니다, Rolling Ress의 카루입니다.

GGHS Time Table을 사용해주셔서 감사합니다. 전면 등교로 인해 버그패치를 제외한 " +
"더 이상의 업데이트는 진행하지 않습니다. 2022년 1월을 기점으로 마이크로소프트 " +
"스토어에서 삭제될 예정이니 참고하시기 바랍니다.", "GGHS Time Table 3", MainPage.Theme);
                    return false;
                }

                GradeComboBoxText = lines[1];
                ClassComboBoxText = lines[2];
                LangComboBoxText = lines[3];
                Special1ComboBoxText = lines[4];
                Special2ComboBoxText = lines[5];
                ScienceComboBoxText = lines[6];

                if (lines[0] != MainPage.Version)
                {
                    await ShowMessageAsync(@$"GGHS Time Table이 V{MainPage.Version}(으)로 업데이트 되었습니다.

전면 등교로 인해 버그패치를 제외한 " +
"더 이상의 업데이트는 진행하지 않습니다. 2022년 1월을 기점으로 마이크로소프트 " +
"스토어에서 삭제될 예정이니 참고하시기 바랍니다.", $"New version installed");
                }
            }

            if (await storageFolder.TryGetItemAsync(keyFileName) is StorageFile keyFile)
            {
                using var stream = await keyFile.OpenAsync(FileAccessMode.Read);
                var size = stream.Size;
                using var inputStream = stream.GetInputStreamAt(0);
                using var keyReader = new DataReader(inputStream);
                uint numBytesLoaded = await keyReader.LoadAsync((uint)size);
                string key = keyReader.ReadString(numBytesLoaded);

                ActivateStatus = key.MakeActivateLevel();
                IsActivated = true;
            }

            if (await storageFolder.TryGetItemAsync(settingsFileName) is StorageFile settingsFile)
            {
                using var stream = await settingsFile.OpenAsync(FileAccessMode.Read);
                var size = stream.Size;
                using var inputStream = stream.GetInputStreamAt(0);
                using var keyReader = new DataReader(inputStream);
                uint numBytesLoaded = await keyReader.LoadAsync((uint)size);
                string hexColor = keyReader.ReadString(numBytesLoaded);

                var (a, r, g, b) = (byte.Parse(hexColor.Substring(1, 2), NumberStyles.HexNumber),
                    byte.Parse(hexColor.Substring(3, 2), NumberStyles.HexNumber),
                    byte.Parse(hexColor.Substring(5, 2), NumberStyles.HexNumber),
                    byte.Parse(hexColor.Substring(7, 2), NumberStyles.HexNumber));

                ColorType = Color.FromArgb(a, r, g, b);
            }


            return true;
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
        => (grade, @class) = (GradeComboBoxText[6] - '0', ClassComboBoxText[6] - '0');
        
        static string MakeString(this ActivateLevel val)
        {
            int converted = (int)val;
            converted = converted * 17 + 19;
            return converted.ToString();
        }

        static ActivateLevel MakeActivateLevel(this string val)
        {
            int converted = Convert.ToInt32(val);
            converted = (converted - 19) / 17;
            return (ActivateLevel)converted;
        }
    }

    // TODO: Use dynamic APIs
    // Check for C# reference in microsoft.
}
