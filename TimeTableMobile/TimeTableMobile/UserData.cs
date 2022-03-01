using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TimeTableCore;

namespace TimeTableMobile;


internal class User : BaseUser
{
    public static string FileName
    => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "gtt5sav.sav")!;

    public string? Korean { get; set; }
    public string? Math { get; set; }
    public string? Social { get; set; }
    public string? Language { get; set; }
    public string? Global1 { get; set; }
    public string? Global2 { get; set; }

    public bool IsAllFilled
    => Class is not 0 &&
    Korean is not null && Math is not null && Social is not null &&
    Language is not null && Global1 is not null && Global2 is not null;
}

/*


internal static class Info
{
    public static User User { get; } = new();
}

[Obsolete]
internal static class UserData
{
    public static string FileName 
        => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "gtt5sav.sav")!;

    public static int Class { get; set; } = 0;


    public static bool IsAllFilled 
        => Class is not 0 && Language is not null && Special1 is not null && Special2 is not null && Science is not null;
}

*/