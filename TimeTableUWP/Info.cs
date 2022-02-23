using System.Runtime.Serialization;
using Windows.ApplicationModel;
using Windows.UI;

namespace TimeTableUWP;

// Base information about GTT
public static class Info
{
    public static User User { get; } = new(); 
    public static Version Version { get; } = new();
    public static Settings Settings { get; set; } = new();
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

    public override string ToString() => Value;
    public static bool operator ==(Version v1, Version v2) => v1.Value == v2.Value;
    public static bool operator !=(Version v1, Version v2) => v1.Value != v2.Value;
    public override bool Equals(object obj) => (obj is Version rhs) && Value == rhs.Value;
    public override int GetHashCode() => Value.GetHashCode();
    public char GetLastNumber() => Value[Value.Length - 1];
}

[DataContract(Name = "Settings")]
public class Settings
{
    public static Color DefaultColor { get; } = Color.FromArgb(160, 251, 193, 212);
    public ElementTheme Theme => IsDarkMode ? ElementTheme.Dark : ElementTheme.Light;

    [DataMember] public Color ColorType { get; set; } = DefaultColor;
    [DataMember] public bool Use24Hour { get; set; } = false;
    [DataMember] public bool IsDarkMode { get; set; } = false;
    [DataMember] public DateType DateFormat { get; set; } = DateType.YYYYMMDD;
    [DataMember] public bool SilentMode { get; set; } = false;
}
