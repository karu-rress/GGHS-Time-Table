using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace TimeTableUWP
{
    public static class SaveData
    {
        static string dataFile = "gttdat.sav", keyFile = "gttact.key";
        public static bool IsActivated { get; set; } = false;
        public static ActivateLevel ActivateStatus { get; set; } = ActivateLevel.None;
        private static string grade, @class, special, social, lang, science;
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
            var storageFolder = ApplicationData.Current.LocalFolder;
            var datFile = await storageFolder.CreateFileAsync(dataFile, CreationCollisionOption.ReplaceExisting);
            StringBuilder sb = new();
            sb.AppendLine(GradeComboBoxText);
            sb.AppendLine(ClassComboBoxText);
            sb.AppendLine(LangComboBoxText);
            sb.AppendLine(SpecialComboBoxText);
            sb.AppendLine(SocialComboBoxText);
            sb.Append(ScienceComboBoxText);
            await FileIO.WriteTextAsync(datFile, sb.ToString());
            if (IsActivated is false)
            {
                return;
            }
            var keFile = await storageFolder.CreateFileAsync(keyFile, CreationCollisionOption.ReplaceExisting);
            sb = new();
            sb.Append((int)ActivateStatus);
            await FileIO.WriteTextAsync(keFile, sb.ToString());
        }

        public static async Task ReadDataAsync()
        {

        }
    }

}
