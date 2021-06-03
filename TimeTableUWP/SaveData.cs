using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Controls;

namespace TimeTableUWP
{
    public static class SaveData
    {
        const string dataFileName = "gttdat.sav", keyFileName = "gttact.key";
        
        private static string grade, @class, special, social, lang, science;
        public static bool IsActivated { get; set; } = false;
        public static ActivateLevel ActivateStatus { get; set; } = ActivateLevel.None;
        public static string GradeComboBoxText { get => grade; set => grade = value ?? "NULL"; }
        public static string ClassComboBoxText { get => @class; set => @class = value ?? "NULL"; }
        public static string SpecialComboBoxText { get => special; set => special = value ?? "NULL"; }
        public static string SocialComboBoxText { get => social; set => social = value ?? "NULL"; }
        public static string LangComboBoxText { get => lang; set => lang = value ?? "NULL"; }
        public static string ScienceComboBoxText { get => science; set => science = value ?? "NULL"; }

        public static async Task SaveDataAsync()
        {
            if (GradeComboBoxText is "NULL")
            {
                return;
            }
            StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
            StorageFile dataFile = await storageFolder.CreateFileAsync(dataFileName, CreationCollisionOption.ReplaceExisting);

            using (var stream = await dataFile.OpenAsync(FileAccessMode.ReadWrite))
            {
                using var outputStream = stream.GetOutputStreamAt(0);
                using var dataWriter = new DataWriter(outputStream);
                dataWriter.WriteString(
                    GradeComboBoxText + "\n" +
                    ClassComboBoxText + "\n" +
                    LangComboBoxText + "\n" +
                    SpecialComboBoxText + "\n" +
                    SocialComboBoxText + "\n" +
                    ScienceComboBoxText
                    );
                await dataWriter.StoreAsync();
                await outputStream.FlushAsync();
            }

            if (IsActivated is false)
            {
                return;
            }
            StorageFile keyFile = await storageFolder.CreateFileAsync(keyFileName, CreationCollisionOption.ReplaceExisting);
            using (var stream = await keyFile.OpenAsync(FileAccessMode.ReadWrite))
            {
                using var outputStream = stream.GetOutputStreamAt(0);
                using var keyWriter = new DataWriter(outputStream);
                keyWriter.WriteString(ActivateStatus.MakeString());
                await keyWriter.StoreAsync();
                await outputStream.FlushAsync();
            }
        }

        public static async Task<bool> LoadDataAsync()
        {
            var storageFolder = ApplicationData.Current.LocalFolder;
            if (await storageFolder.TryGetItemAsync(dataFileName) is not StorageFile dataFile)
            {
                return false;
            }

            var stream = await dataFile.OpenAsync(FileAccessMode.Read);
            ulong size = stream.Size;
            using (var inputStream = stream.GetInputStreamAt(0))
            {
                using var dataReader = new DataReader(inputStream);
                uint numBytesLoaded = await dataReader.LoadAsync((uint)size);
                string text = dataReader.ReadString(numBytesLoaded);

                string[] lines = text.Split(
                    new[] { "\r\n", "\r", "\n" },
                    StringSplitOptions.None
                    );

                GradeComboBoxText = lines[0];
                ClassComboBoxText = lines[1];
                LangComboBoxText = lines[2];
                SpecialComboBoxText = lines[3];
                SocialComboBoxText = lines[4];
                ScienceComboBoxText = lines[5];
            }

            if (await storageFolder.TryGetItemAsync(keyFileName) is not StorageFile keyFile)
            {
                return true;
            }
            stream = await keyFile.OpenAsync(FileAccessMode.Read);
            size = stream.Size;
            using (var inputStream = stream.GetInputStreamAt(0))
            {
                using var keyReader = new DataReader(inputStream);
                uint numBytesLoaded = await keyReader.LoadAsync((uint)size);
                string text = keyReader.ReadString(numBytesLoaded);

                ActivateStatus = text.MakeActivateLevel();
                IsActivated = true;
            }
            return true;
        }

        public static void SetComboBoxes(
            ref ComboBox gradeComboBox,
            ref ComboBox classComboBox,
            ref ComboBox specialComboBox,
            ref ComboBox socialComboBox,
            ref ComboBox langComboBox,
            ref ComboBox scienceComboBox
            )
        {
            gradeComboBox.SelectedItem = GradeComboBoxText is "NULL" ? null : GradeComboBoxText;
            classComboBox.SelectedItem = ClassComboBoxText is "NULL" ? null : ClassComboBoxText;
            specialComboBox.SelectedItem = SpecialComboBoxText is "NULL" ? null : SpecialComboBoxText;
            socialComboBox.SelectedItem = SocialComboBoxText is "NULL" ? null : SocialComboBoxText;
            langComboBox.SelectedItem = LangComboBoxText is "NULL" ? null : LangComboBoxText;
            scienceComboBox.SelectedItem = ScienceComboBoxText is "NULL" ? null : ScienceComboBoxText;
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
