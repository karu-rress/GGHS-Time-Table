using RollingRess;

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

    public class Version
    {
        private static PackageVersion PackageVer { get; } = Package.Current.Id.Version;
        /// <summary>
        /// GGHS Time Table's version: string value with the format "X.X.X"
        /// </summary>
        public string Value { get; }
#if DEBUG
            = "5.0.alpha-0220";
#else
            = PackageVer.ParseString();
#endif
        public static implicit operator string(Version v) => v.Value;
  
        override public string ToString() => Value;
        public char GetLastNumber() => Value[Value.Length - 1];
    }

    public class Settings
    {
        public static Color DefaultColor { get; } = Color.FromArgb(0xEE, 0xD9, 0xC0, 0xF9);
        public Color ColorType { get; set; } = DefaultColor;


        public bool Use24Hour { get; set; } = false;
        public bool IsDarkMode { get; set; } = false;
        public ElementTheme Theme => IsDarkMode ? ElementTheme.Dark : ElementTheme.Light;
        public DateType DateFormat { get; set; } = DateType.YYYYMMDD;

        public bool SilentMode { get; set; } = false;
    }
}