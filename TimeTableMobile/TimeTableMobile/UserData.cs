using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TimeTableMobile
{
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

