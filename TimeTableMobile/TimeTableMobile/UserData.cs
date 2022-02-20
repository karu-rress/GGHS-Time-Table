using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TimeTableCore;

namespace TimeTableMobile
{
    public class DataSaver : BaseSaver
    {
        public static string FileName
            => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "gttsav.sav")!;

        public bool IsAllFilled
            => Info.User.Class is not 0 &&
            Korean is not null && Math is not null && Social is not null &&
            Language is not null && Global1 is not null && Global2 is not null;
    }

    internal class User
    {
        public int Class { get; set; } = 0;
    }


    internal static class Info
    {
        public static User User { get; } = new();
    }

    [Obsolete]
    internal static class UserData
    {
        public static string FileName 
            => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "gttsav.sav")!;

        public static int Class { get; set; } = 0;

        public static string? Language { get; set; }
        public static string? Special1 { get; set; }
        public static string? Special2 { get; set; }
        public static string? Science { get; set; }

        public static bool IsAllFilled 
            => Class is not 0 && Language is not null && Special1 is not null && Special2 is not null && Science is not null;
    }
}

