using RollingRess;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using TimeTableCore;
using TimeTableUWP.Pages;
using Windows.ApplicationModel;
using Windows.UI;
using Windows.UI.Xaml;

namespace TimeTableUWP
{
    // Base information about GTT
    public static class Info
    {
        public static User User { get; } = new();
        public static Settings Settings { get; set; } = new();
        public static Version Version { get; } = new();
    }

    [DataContract(Name = "Version")]
    public class Version
    {
        private static PackageVersion PackageVer { get; } = Package.Current.Id.Version;
        /// <summary>
        /// GGHS Time Table's version: string value with the format "X.X.X"
        /// </summary>
        [DataMember]
        public string Value { get; set; }
#if BETA
            = Datas.Version;
#else
            = PackageVer.ParseString();
#endif
        // public static implicit operator string(Version v) => v.Value;
  
        override public string ToString() => Value;
        public static bool operator==(Version v1, Version v2)
        {
            return v1.Value == v2.Value;
        }
        public static bool operator!=(Version v1, Version v2)
        {
            return v1.Value != v2.Value;
        }
        public override bool Equals(object obj) => (obj is Version rhs) && Value == rhs.Value;
        override public int GetHashCode() => Value.GetHashCode();
        public char GetLastNumber() => Value[Value.Length - 1];
    }

    [DataContract(Name = "Settings")]
    public class Settings
    {
        public static Color DefaultColor { get; } = Color.FromArgb(160, 251, 193, 212);
        [DataMember]
        public Color ColorType { get; set; } = DefaultColor;

        [DataMember]
        public bool Use24Hour { get; set; } = false;
        [DataMember]
        public bool IsDarkMode { get; set; } = false;
        public ElementTheme Theme => IsDarkMode ? ElementTheme.Dark : ElementTheme.Light;
        [DataMember]
        public DateType DateFormat { get; set; } = DateType.YYYYMMDD;
        [DataMember]
        public bool SilentMode { get; set; } = false;
    }
}